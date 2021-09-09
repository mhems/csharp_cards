using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Blackjack
{
    public class TableFullException : Exception
    {
        public TableFullException() : base() { }
        public TableFullException(string msg) : base(msg) { }
        public TableFullException(string msg, Exception inner) : base(msg, inner) { }
    }

    public class BlackjackTable
    {
        private readonly BlackjackTableSlot[] slots;
        private readonly Dictionary<BlackjackActionEnum, BlackjackAction> actionMap = new();

        private BlackjackTableSlot DealerSlot { get; } = new() { Player = new BlackjackDealer() };
        private Bank TableBank => DealerSlot.Player.Bank;
        private BlackjackHand DealerHand => DealerSlot.Hand;

        public Shoe Shoe { get; private set; }
        public Card UpCard => DealerSlot.Hand[1];

        public int NumVacancies => NumSlots - NumOccupiedSlots;
        public bool TableFull => NumVacancies == 0;
        public int NumSlots => slots.Length;
        public int NumOccupiedSlots => slots.Where(s => s.Occupied).Count();
        public int NumActiveSlots => ActiveSlots.Count();
        public BlackjackTableSlot[] ActiveSlots => slots.Where(s => s.Active).ToArray();

        public event EventHandler<EventArgs> RoundBegun;
        public event EventHandler<EventArgs> RoundEnded;

        public BlackjackTable(int numSeats)
        {
            Shoe = new Shoe(6);
            slots = new BlackjackTableSlot[numSeats];
            for (int i = 0; i < numSeats; i++)
            {
                slots[i] = new BlackjackTableSlot();
            }

            HitAction hit = new(Shoe);
            StandAction stand = new();
            actionMap.Add(BlackjackActionEnum.Hit, hit);
            actionMap.Add(BlackjackActionEnum.Stand, stand);
            actionMap.Add(BlackjackActionEnum.Double, new DoubleAction(hit, stand));
            actionMap.Add(BlackjackActionEnum.Split, new SplitAction(hit));
            actionMap.Add(BlackjackActionEnum.Surrender, new SurrenderAction());
        }

        public bool SeatPlayer(BlackjackPlayer player, int position = 0)
        {
            if (position >= slots.Length || position < -1)
            {
                throw new ArgumentException($"seat {position} does not exist");
            }

            if (TableFull)
            {
                throw new TableFullException($"Table of {NumSlots} is full");
            }

            for (int i = position; i < position + NumSlots; i++)
            {
                if (!slots[i % NumSlots].Occupied)
                {
                    slots[i % NumSlots].Player = player;
                    return true;
                }
            }

            return false;
        }

        public void PlayRound()
        {
            BeginRound();
            Deal();
            OfferEarlySurrender();
            if (UpCard.IsAce)
            {
                OfferInsurance();
                if (DealerHand.IsNaturalBlackjack)
                {
                    HandleDealerBlackjack();
                    EndRound();
                    return;
                }
                else
                {
                    CollectInsurance();
                }
            }
            DealSlots();
            Settle();
            EndRound();
        }

        private void OfferEarlySurrender()
        {
            foreach (BlackjackTableSlot slot in ActiveSlots)
            {
                if (slot.OfferEarlySurrender(UpCard))
                {
                    actionMap[BlackjackActionEnum.Surrender].Act(slot);
                    slot.Pot.TransactTo(TableBank, slot.Pot.Balance / 2);
                    slot.Settled = true;
                }
            }
        }

        private void OfferInsurance()
        {
            foreach (BlackjackTableSlot slot in ActiveSlots)
            {
                slot.OfferInsurance(UpCard);
            }
        }

        private void HandleDealerBlackjack()
        {
            foreach (BlackjackTableSlot slot in ActiveSlots)
            {
                slot.Pot.TransactTo(TableBank, 10);
                if (slot.Insured)
                {
                    TableBank.TransactTo(slot.Pot, 10);
                }
                slot.Settled = true;
            }
        }

        private void CollectInsurance()
        {
            foreach (BlackjackTableSlot slot in ActiveSlots)
            {
                if (slot.Insured)
                {
                    slot.Player.Bank.TransactTo(TableBank, 5);
                    slot.Pot.TransactTo(TableBank, 5);
                }
            }
        }

        private void DealSlots()
        {
            foreach (BlackjackTableSlot slot in slots)
            {
                int i = 0;
                while (i < slot.NumSplits)
                {
                    slot.Index = i;
                    DealHand(slot);
                    i++;
                }
            }
            DealHand(DealerSlot);
        }

        private void DealHand(BlackjackTableSlot slot)
        {
            while (true)
            {
                BlackjackHand hand = slot.Hand;
                if (hand.IsBlackjack)
                {
                    int payout = (int)Math.Ceiling(1.5 * slot.Pot.Balance);
                    TableBank.TransactTo(slot.Pot, payout);
                    slot.Settled = true;
                    break;
                }
                else if (hand.IsBust)
                {
                    slot.Pot.TransactTo(TableBank, slot.Pot.Balance);
                    slot.Settled = true;
                    break;
                }
                HashSet<BlackjackActionEnum> availableActions = new(actionMap.Keys.Where(a => actionMap[a].Available(slot)));
                BlackjackActionEnum action = slot.Player.DecisionPolicy.Decide(slot.Hand, UpCard, availableActions);
                if (actionMap[action].Act(slot))
                {
                    break;
                }
            }
        }

        private void Settle()
        {
            foreach (BlackjackTableSlot slot in slots.Where(s => !s.Settled))
            {
                slot.Settle(TableBank, DealerHand.Value);
            }
        }

        private void BeginRound()
        {
            RoundBegun?.Invoke(this, new EventArgs());
            foreach (BlackjackTableSlot slot in slots.Where(s => s.Occupied))
            {
                slot.BeginRound();
            }
            DealerSlot.BeginRound();
        }

        private void Deal()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (BlackjackTableSlot slot in ActiveSlots)
                {
                    slot.Hand.Add(Shoe.Deal(1)[0]);
                }
                DealerHand.Add(Shoe.Deal(1)[0]);
            }
        }

        private void EndRound()
        {
            foreach (BlackjackTableSlot slot in ActiveSlots)
            {
                slot.EndRound();
            }
            DealerSlot.EndRound();
            RoundEnded?.Invoke(this, new EventArgs());
        }
    }

    public class BlackjackTableSlot
    {
        private static readonly HashSet<BlackjackActionEnum> SurrenderSet = new() { BlackjackActionEnum.Surrender };
        private readonly List<BlackjackHand> hands = new();
        private readonly List<Bank> pots = new() { new() };
        private BlackjackPlayer player = null;

        #region Properties
        public BlackjackPlayer Player
        {
            get => player;
            set
            {
                Seating?.Invoke(this, new SeatingEventArgs(player, value));
                player = value;
            }
        }

        internal int Index { get; set; }
        public bool Insured { get; private set; }
        public bool Surrendered { get; internal set; }
        public bool Settled { get; set; }
        public int NumSplits => hands.Count - 1;
        public bool Active => pots[0].Balance> 0;
        public bool Occupied => Player != null;
        public BlackjackHand Hand { get => hands[Index]; internal set => hands[Index] = value; }
        public Bank Pot
        {
            get => pots[Index];
            set => pots[Index] = value;
        }
        #endregion

        #region Events
        public EventHandler<EventArgs> Seating;
        public event EventHandler<EventArgs> RoundBegun;
        public event EventHandler<EventArgs> RoundEnded;
        #endregion

        public void BeginRound()
        {
            RoundBegun?.Invoke(this, new EventArgs());
            int amount = player.BettingPolicy.Bet();
            player.Bank.TransactTo(Pot, amount);
        }

        public bool OfferEarlySurrender(Card upCard)
        {
            BlackjackActionEnum decision = player.DecisionPolicy.Decide(Hand, upCard, SurrenderSet);
            return decision == BlackjackActionEnum.Surrender;
        }

        public bool OfferInsurance(Card upCard)
        {
            Insured = player.InsurancePolicy.Insure(Hand, upCard);
            if (Insured)
            {
                player.Bank.TransactTo(Pot, 5);
            }
            return Insured;
        }

        public void Split()
        {
            BlackjackHand newHand = new() { IsSplit = true };
            newHand.Add(Hand[1]);
            Hand.RemoveAt(1);
            Hand.IsSplit = true;
            hands.Add(newHand);

            pots.Add(new Bank());
            player.Bank.TransactTo(pots[1], 10);
        }

        public void Settle(Bank house, int dealerValue)
        {
            for (int i = 0; i < NumSplits; i++)
            {
                if (Surrendered || (hands[i].Value < dealerValue))
                {
                    pots[i].TransactTo(house, pots[i].Balance);
                }
                else if ((dealerValue > 21) || (hands[i].Value > dealerValue))
                {
                    house.TransactTo(pots[i], 10);
                }
            }
            Settled = true;
        }

        public void EndRound()
        {
            Index = 0;

            foreach (Bank pot in pots)
            {
                pot.TransactTo(player.Bank, pot.Balance);
            }
            pots.RemoveRange(1, pots.Count - 1);
            Pot = new();

            hands.RemoveRange(1, hands.Count - 1);
            Hand.Clear();

            Insured = false;
            Settled = false;

            RoundEnded?.Invoke(this, new EventArgs());
        }
    }

    public class SeatingEventArgs : EventArgs
    {
        public BlackjackPlayer PreviousPlayer { get; private set; }
        public BlackjackPlayer NewPlayer { get; private set; }

        public SeatingEventArgs(BlackjackPlayer prevPlayer, BlackjackPlayer newPlayer)
        {
            PreviousPlayer = prevPlayer;
            NewPlayer = newPlayer;
        }
    }
}

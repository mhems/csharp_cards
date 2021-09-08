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
                if (slot.Insured)
                {
                    slot.Player.Bank.Deposit(TableBank.Withdraw(5));
                }
                TableBank.Deposit(slot.Pot);
                slot.Pot = 0;
                slot.Settled = true;
            }
        }

        private void CollectInsurance()
        {
            foreach (BlackjackTableSlot slot in ActiveSlots)
            {
                if (slot.Insured)
                {
                    TableBank.Deposit(5);
                    slot.Pot = 0;
                    slot.Settled = true;
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
                    int payout = (int)Math.Ceiling(1.5 * slot.Pot);
                    TableBank.Withdraw(payout);
                    slot.Player.Payout(payout);
                    slot.Pot = 0;
                    slot.Settled = true;
                    break;
                }
                else if (hand.IsBust)
                {
                    TableBank.Deposit(slot.Pot);
                    slot.Pot = 0;
                    slot.Settled = true;
                    break;
                }
                HashSet<BlackjackActionEnum> availableActions = new(actionMap.Keys.Where(a => actionMap[a].Available(slot)));
                BlackjackActionEnum action = slot.Player.ActionPolicy.Decide(slot.Hand, UpCard, availableActions);
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
        }
    }

    public class BlackjackTableSlot
    {
        private readonly List<BlackjackHand> hands = new();
        private readonly List<int> pots = new() { 0 };
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
        public bool Active => pots[0] > 0;
        public bool Occupied => Player != null;
        public BlackjackHand Hand { get => hands[Index]; internal set => hands[Index] = value; }
        public int Pot
        {
            get => pots[Index];
            set => pots[Index] = value;
        }
        #endregion

        #region Events
        public EventHandler<EventArgs> Seating;
        #endregion

        public void BeginRound()
        {
            Pot = player.BettingPolicy.Bet();
        }

        public bool OfferInsurance(Card upCard)
        {
            Insured = player.InsurancePolicy.Insure(Hand, upCard);
            player.Bank.Withdraw(5);
            return Insured;
        }

        public void Split()
        {
            BlackjackHand newHand = new() { IsSplit = true };
            newHand.Add(Hand[1]);
            Hand.RemoveAt(1);
            Hand.IsSplit = true;
            hands.Add(newHand);

            player.Bank.Withdraw(10);
            pots.Add(10);
        }

        public void Settle(Bank house, int dealerValue)
        {
            for (int i = 0; i < NumSplits; i++)
            {
                if (Surrendered)
                {
                    house.Deposit(10);
                    pots[i] = 0;
                }
                else if (dealerValue > 21)
                {
                    house.Withdraw(10);
                    pots[i] = 10;
                }
                else if (hands[i].Value > dealerValue)
                {
                    house.Withdraw(10);
                    pots[i] = 10;
                }
                else if (hands[i].Value < dealerValue)
                {
                    house.Deposit(10);
                    pots[i] = 0;
                }
            }
            Settled = true;
        }

        public void EndRound()
        {
            Index = 0;

            foreach (int pot in pots)
            {
                player.Bank.Deposit(pot);
            }
            pots.RemoveRange(1, pots.Count - 1);
            Pot = 0;

            hands.RemoveRange(1, hands.Count - 1);
            Hand.Clear();

            Insured = false;
            Settled = false;
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

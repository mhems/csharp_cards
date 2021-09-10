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

    public class IllegalBetException : Exception
    {
        private static string Msg = "Illegal bet of ${0}";
        public IllegalBetException(int amount): base(String.Format(Msg, amount)) { }
        public IllegalBetException(int amount, Exception inner) : base(String.Format(Msg, amount), inner) {  }
    }

    public class BlackjackTable
    {
        private readonly BlackjackTableSlot[] slots;
        private readonly Dictionary<BlackjackActionEnum, BlackjackAction> actionMap = new();

        #region Properties
        public IBlackjackConfig Config { get; set; }
        private BlackjackTableSlot DealerSlot { get; }
        private Bank TableBank => DealerSlot.Player.Bank;
        private BlackjackHand DealerHand => DealerSlot.Hand;
        public Shoe Shoe { get; private set; }
        public BlackjackCount Count { get; private set; }
        public Card UpCard => DealerSlot.Hand[1];
        public int NumVacancies => NumSlots - NumOccupiedSlots;
        public bool TableFull => NumVacancies == 0;
        public int NumSlots => slots.Length;
        public int NumOccupiedSlots => slots.Where(s => s.Occupied).Count();
        public int NumActiveSlots => ActiveSlots.Length;
        public BlackjackTableSlot[] ActiveSlots => slots.Where(s => s.Active).ToArray();
        #endregion

        public event EventHandler<EventArgs> RoundBegun;
        public event EventHandler<EventArgs> RoundEnded;

        public BlackjackTable(int numSeats, IBlackjackConfig config)
        {
            Config = config;
            Shoe = new Shoe(Config.NumDecksInShoe);
            Shoe.CutIndex = Shoe.Count - Config.CutIndex;
            Shoe.NumBurnOnShuffle = Config.NumBurntOnShuffle;
            Count = new(Shoe);

            slots = new BlackjackTableSlot[numSeats];
            for (int i = 0; i < numSeats; i++)
            {
                slots[i] = new BlackjackTableSlot(Config);
            }
            DealerSlot = new(Config);
            DealerSlot.Player = new BlackjackDealer(Config);

            HitAction hit = new(Shoe);
            StandAction stand = new();
            actionMap.Add(BlackjackActionEnum.Hit, hit);
            actionMap.Add(BlackjackActionEnum.Stand, stand);
            actionMap.Add(BlackjackActionEnum.Double, new DoubleAction(Config, hit, stand));
            actionMap.Add(BlackjackActionEnum.Split, new SplitAction(Config, hit));
            actionMap.Add(BlackjackActionEnum.LateSurrender, new LateSurrenderAction(Config));
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
            if (Config.EarlySurrenderOffered)
            {
                foreach (BlackjackTableSlot slot in ActiveSlots)
                {
                    if (slot.OfferEarlySurrender(UpCard))
                    {
                        slot.Pot.TransferFactor(TableBank, Config.EarlySurrenderCost);
                        slot.Settled = true;
                    }
                }
            }
        }

        private void OfferInsurance()
        {
            if (Config.InsuranceOffered)
            {
                foreach (BlackjackTableSlot slot in ActiveSlots)
                {
                    slot.OfferInsurance(UpCard);
                }
            }
        }

        private void HandleDealerBlackjack()
        {
            foreach (BlackjackTableSlot slot in ActiveSlots)
            {
                if (!slot.Hand.IsNaturalBlackjack)
                {
                    slot.Pot.Transfer(TableBank);
                }
                if (slot.Insured)
                {
                    TableBank.Transfer(slot.InsurancePot, Config.InsurancePayoutRatio * slot.InsurancePot.Balance);
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
                    slot.InsurancePot.Transfer(TableBank);
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
                if (slot.Hand.IsBlackjack && !DealerHand.IsBlackjack)
                {
                    TableBank.Transfer(slot.Pot, Config.BlackjackPayoutRatio * slot.Pot.Balance);
                    slot.Settled = true;
                    break;
                }
                else if (slot.Hand.IsBust)
                {
                    slot.Pot.Transfer(TableBank);
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
            foreach (BlackjackTableSlot slot in slots.Where(s => s.Occupied))
            {
                slot.EndRound();
            }
            DealerSlot.EndRound();
            RoundEnded?.Invoke(this, new EventArgs());
        }
    }

    public class BlackjackTableSlot
    {
        private static readonly HashSet<BlackjackActionEnum> SurrenderSet = new() { BlackjackActionEnum.LateSurrender };
        private readonly List<BlackjackHand> hands = new();
        private readonly List<Bank> pots = new() { new() };
        private BlackjackPlayer player = null;

        #region Properties
        public IBlackjackConfig Config { get; set; }
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
        public bool Insured => InsurancePot.Balance > 0;
        public bool Surrendered { get; internal set; }
        public bool Settled { get; set; }
        public int NumSplits => hands.Count - 1;
        public bool Active => pots[0].Balance > 0;
        public bool Occupied => Player != null;
        public BlackjackHand Hand { get => hands[Index]; internal set => hands[Index] = value; }
        public Bank Pot => pots[Index];
        public Bank InsurancePot { get; private set; } = new();
        #endregion

        #region Events
        public EventHandler<EventArgs> Seating;
        public event EventHandler<EventArgs> RoundBegun;
        public event EventHandler<EventArgs> RoundEnded;
        #endregion

        public BlackjackTableSlot(IBlackjackConfig config)
        {
            Config = config;
        }

        public void BeginRound()
        {
            RoundBegun?.Invoke(this, new EventArgs());
            int amount = player.BettingPolicy.Bet();
            if (amount < Config.MinimumBet || amount > Config.MaximumBet)
            {
                throw new IllegalBetException(amount);
            }
            player.Bank.Transfer(Pot, amount);
        }

        public bool OfferEarlySurrender(Card upCard)
        {
            BlackjackActionEnum decision = player.DecisionPolicy.Decide(Hand, upCard, SurrenderSet);
            return decision == BlackjackActionEnum.LateSurrender;
        }

        public bool OfferInsurance(Card upCard)
        {
            bool insured = player.InsurancePolicy.Insure(Hand, upCard);
            if (insured)
            {
                player.Bank.Transfer(InsurancePot, Config.InsuranceCost * Pot.Balance);
            }
            return insured;
        }

        public void Split()
        {
            BlackjackHand newHand = new() { IsSplit = true };
            newHand.Add(Hand[1]);
            Hand.RemoveAt(1);
            Hand.IsSplit = true;
            hands.Add(newHand);

            pots.Add(new Bank());
            player.Bank.Transfer(pots[1], pots[0].Balance * Config.SplitCost);
        }

        public void Settle(Bank house, int dealerValue)
        {
            for (int i = 0; i < NumSplits; i++)
            {
                if (Surrendered)
                {
                    pots[i].TransferFactor(house, Config.LateSurrenderCost);
                }
                else if (hands[i].Value < dealerValue)
                {
                    pots[i].Transfer(house);
                }
                else if ((dealerValue > 21) || (hands[i].Value > dealerValue))
                {
                    house.Transfer(pots[i], Config.PayoutRatio * pots[i].Balance);
                }
            }
            Settled = true;
        }

        public void EndRound()
        {
            Index = 0;

            foreach (Bank pot in pots)
            {
                pot.Transfer(player.Bank);
            }
            InsurancePot.Transfer(player.Bank);

            pots.RemoveRange(1, pots.Count - 1);
            pots[0] = new();
            InsurancePot = new();

            hands.RemoveRange(1, hands.Count - 1);
            Hand.Clear();

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

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

    public class HandDealtEventArgs : EventArgs
    {
        public BlackjackHand Hand { get; }
        public BlackjackPlayer Player { get; }
        public HandDealtEventArgs(BlackjackHand hand, BlackjackPlayer player)
        {
            Hand = hand;
            Player = player;
        }
    }
    public class BlackjackTable
    {
        private static readonly HashSet<BlackjackActionEnum> dealerOptions = new()
        { BlackjackActionEnum.Hit, BlackjackActionEnum.Stand };
        private readonly BlackjackTableSlot[] slots;
        private readonly Dictionary<BlackjackActionEnum, BlackjackAction> actionMap = new();

        #region Properties
        public IBlackjackConfig Config { get; set; }
        public BlackjackTableSlot DealerSlot { get; set; }
        public Bank TableBank { get; } = new HouseBank();
        public BlackjackDealer Dealer => (BlackjackDealer)DealerSlot.Player;
        public BlackjackHand DealerHand => DealerSlot.Hand;
        public Shoe Shoe { get; private set; }
        public BlackjackCount Count { get; private set; }
        public Card UpCard => DealerHand[1];
        public int NumVacancies => NumSlots - NumOccupiedSlots;
        public bool TableFull => NumVacancies == 0;
        public int NumSlots => slots.Length;
        public int NumOccupiedSlots => slots.Where(s => s.Occupied).Count();
        public int NumActiveSlots => ActiveSlots.Length;
        public BlackjackTableSlot[] Slots => slots;
        public BlackjackTableSlot[] ActiveSlots => slots.Where(s => s.Active).ToArray();
        #endregion

        #region Events
        public event EventHandler<EventArgs> RoundBegun;
        public event EventHandler<EventArgs> RoundEnded;
        public event EventHandler<HandDealtEventArgs> HandDealt;
        public event EventHandler<SeatingEventArgs> SeatChanged;
        #endregion

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

            DealerSlot = new(Config)
            {
                IsDealer = true
            };
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
                    BlackjackPlayer previousPlayer = slots[i % NumSlots].Player;
                    slots[i % NumSlots].Player = player;
                    SeatChanged?.Invoke(this, new SeatingEventArgs(previousPlayer, player, i));
                    return true;
                }
            }

            return false;
        }

        public void AddLogger(BlackjackEventLogger logger)
        {
            RoundBegun += logger.OnTableRoundBegan;
            RoundEnded += logger.OnTableRoundEnded;
            HandDealt += logger.OnTableHandDealt;
            SeatChanged += logger.OnTableSeatChanged;

            foreach (BlackjackAction action in actionMap.Values)
            {
                action.Acted += logger.OnActionExecuted;
            }
            ((HitAction)actionMap[BlackjackActionEnum.Hit]).Hit += logger.OnHitExecuted;

            Count.Changed += logger.OnCountChanged;

            Shoe.Shuffling += logger.OnShoeShuffling;
            Shoe.Dealt += logger.OnShoeDealt;
            Shoe.Burnt += logger.OnShoeBurnt;
            Shoe.Exhausted += logger.OnShoeExhausted;

            TableBank.Deposited += logger.OnHouseEarned;
            TableBank.Withdrawn += logger.OnHouseSpent;

            foreach (BlackjackTableSlot slot in slots)
            {
                if (slot != null)
                {
                    slot.RoundBegun += logger.OnTableSlotRoundBegan;
                    slot.RoundEnded += logger.OnTableSlotRoundEnded;
                    slot.ActingHand += logger.OnTableSlotActingHand;
                    slot.Acting += logger.OnTableSlotActing;

                    if (slot.Occupied)
                    {
                        if (slot.Player.DecisionPolicy != null)
                        {
                            slot.Player.DecisionPolicy.Decided += logger.OnDecisionMade;
                        }
                        if (slot.Player.BettingPolicy != null)
                        {
                            slot.Player.BettingPolicy.Betting += logger.OnBetMade;
                        }
                        if (slot.Player.EarlySurrenderPolicy != null)
                        {
                            slot.Player.EarlySurrenderPolicy.Surrendered += logger.OnEarlySurrenderDecision;
                        }
                        if (slot.Player.InsurancePolicy != null)
                        {
                            slot.Player.InsurancePolicy.Insured += logger.OnInsuranceDecision;
                        }
                        if (slot.Player.Bank != null)
                        {
                            slot.Player.Bank.Withdrawn += logger.OnSpent;
                            slot.Player.Bank.Deposited += logger.OnEarned;
                        }
                    }
                }
            }

            if (DealerSlot != null)
            {
                DealerSlot.RoundBegun += logger.OnTableSlotRoundBegan;
                DealerSlot.RoundEnded += logger.OnTableSlotRoundEnded;
                DealerSlot.ActingHand += logger.OnTableSlotActingHand;
                DealerSlot.Acting += logger.OnTableSlotActing;
                DealerHand.HoleCardRevealed += logger.OnHoleCardRevealed;
                DealerHand.HoleCardRevealed += Count.OnHoleCardRevealed;

                if (DealerSlot.Occupied && DealerSlot.Player.DecisionPolicy != null)
                {
                    DealerSlot.Player.DecisionPolicy.Decided += logger.OnDecisionMade;
                }
            }
        }

        public void PlayRound()
        {
            BeginRound();
            Bet();
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
            DealPlayers();
            DealDealer();
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

        private void DealPlayers()
        {
            foreach (BlackjackTableSlot slot in slots)
            {
                slot.NotifyAction();
                int i = 0;
                while (i < slot.NumHands)
                {
                    slot.Index = i;
                    DealHand(slot);
                    i++;
                }
            }
        }

        private void DealDealer()
        {
            DealerHand.RevealHoleCard();
            DealerSlot.NotifyAction();
            DealerSlot.NotifyHand();
            while (true)
            {
                if (DealerHand.IsBlackjack || DealerHand.IsBust)
                {
                    break;
                }
                BlackjackActionEnum action = Dealer.DecisionPolicy.Decide(DealerHand, UpCard, dealerOptions);
                if (actionMap[action].Act(DealerSlot))
                {
                    break;
                }
            }
        }

        private void DealHand(BlackjackTableSlot slot)
        {
            slot.NotifyHand();
            while (true)
            {
                if (slot.Hand.IsBlackjack)
                {
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

        private void Bet()
        {
            foreach(BlackjackTableSlot slot in slots.Where(s => s.Occupied))
            {
                slot.Bet();
            }
        }

        private void Deal()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (BlackjackTableSlot slot in ActiveSlots)
                {
                    slot.Hand.Add(Shoe.Deal(1)[0]);
                }
                DealerHand.Add(Shoe.Deal(1, i != 0)[0], i != 0);
            }

            foreach (BlackjackTableSlot slot in ActiveSlots)
            {
                HandDealt?.Invoke(this, new HandDealtEventArgs(slot.Hand, slot.Player));
            }
            // TODO: how to have event for dealer?
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
        private readonly List<BlackjackHand> hands = new() { new() };
        private readonly List<Bank> pots = new() { new() };

        #region Properties
        public IBlackjackConfig Config { get; set; }
        public BlackjackPlayer Player { get; set; }
        public bool IsDealer { get; set; }
        public int Index { get; set; }
        public bool Insured => InsurancePot.Balance > 0;
        public bool Surrendered { get; internal set; }
        public bool Settled { get; set; }
        public int NumHands => hands.Count;
        public int NumSplits => hands.Count - 1;
        public bool Active => pots[0].Balance > 0;
        public bool Occupied => Player != null;
        public BlackjackHand Hand { get => hands[Index]; internal set => hands[Index] = value; }
        public Bank Pot => pots[Index];
        public Bank InsurancePot { get; private set; } = new();
        #endregion

        #region Events
        public event EventHandler<EventArgs> RoundBegun;
        public event EventHandler<EventArgs> RoundEnded;
        public event EventHandler<EventArgs> Acting;
        public event EventHandler<EventArgs> ActingHand;
        #endregion

        public BlackjackTableSlot(IBlackjackConfig config)
        {
            Config = config;
        }

        public void BeginRound()
        {
            Index = 0;
            hands.RemoveRange(1, hands.Count - 1);
            Hand.Clear();

            RoundBegun?.Invoke(this, new EventArgs());
        }

        public void Bet()
        {
            int amount = Player.BettingPolicy.Bet(Config.MinimumBet);
            if (amount < Config.MinimumBet || amount > Config.MaximumBet)
            {
                throw new IllegalBetException(amount);
            }
            Player.Bank.Transfer(Pot, amount);
        }

        public bool OfferEarlySurrender(Card upCard)
        {
            return Player.EarlySurrenderPolicy.Surrender(Hand, upCard);
        }

        public bool OfferInsurance(Card upCard)
        {
            bool insured = Player.InsurancePolicy.Insure(Hand, upCard);
            if (insured)
            {
                Player.Bank.Transfer(InsurancePot, Config.InsuranceCost * Pot.Balance);
            }
            return insured;
        }

        public void NotifyAction()
        {
            Acting?.Invoke(this, new EventArgs());
        }

        public void NotifyHand()
        {
            ActingHand?.Invoke(this, new EventArgs());
        }

        public void Split()
        {
            BlackjackHand newHand = new() { IsSplit = true };
            newHand.Add(Hand[1]);
            Hand.RemoveAt(1);
            Hand.IsSplit = true;

            hands.Insert(Index + 1, newHand);

            pots.Insert(Index + 1, new Bank());
            Player.Bank.Transfer(pots[Index + 1], pots[Index].Balance * Config.SplitCost);
        }

        public void Settle(Bank house, int dealerValue)
        {
            for (int i = 0; i < NumHands; i++)
            {
                if (Surrendered)
                {
                    pots[i].TransferFactor(house, Config.LateSurrenderCost);
                }
                else if (hands[i].IsBlackjack && dealerValue != 21)
                {
                    house.Transfer(pots[i], Config.BlackjackPayoutRatio * pots[i].Balance);
                }
                else if ((dealerValue > 21) || (hands[i].Value > dealerValue))
                {
                    house.Transfer(pots[i], Config.PayoutRatio * pots[i].Balance);
                }
                else if (hands[i].Value < dealerValue || hands[i].IsBust)
                {
                    pots[i].Transfer(house);
                }
            }
            Settled = true;
        }

        public void EndRound()
        {
            Index = 0;

            foreach (Bank pot in pots)
            {
                pot.Transfer(Player.Bank);
            }
            InsurancePot.Transfer(Player.Bank);

            pots.RemoveRange(1, pots.Count - 1);
            pots[0].Empty();
            InsurancePot.Empty();

            Settled = false;

            RoundEnded?.Invoke(this, new EventArgs());
        }
    }

    public class SeatingEventArgs : EventArgs
    {
        public BlackjackPlayer PreviousPlayer { get; private set; }
        public BlackjackPlayer NewPlayer { get; private set; }
        public int SeatIndex { get; }

        public SeatingEventArgs(BlackjackPlayer prevPlayer, BlackjackPlayer newPlayer, int seatIndex)
        {
            PreviousPlayer = prevPlayer;
            NewPlayer = newPlayer;
            SeatIndex = seatIndex;
        }
    }
}

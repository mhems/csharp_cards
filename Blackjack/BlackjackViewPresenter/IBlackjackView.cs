using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using Blackjack;

namespace BlackjackViewPresenter
{
    public interface ICardView
    {
        public Card.RankEnum Rank { get; set; }
        public Card.SuitEnum Suit { get; set; }
    }

    public interface IBlackjackHandView
    {
        public int Value { get; set; }
        public bool Bust { get; set; }
        public bool Blackjack { get; set; }
        public void ClearHand();
        public void AddCard(Card card, bool visible=true);
    }

    public interface IBankView
    {
        public int Balance { get; set; }
    }

    public interface IBlackjackPlayerView
    {
        public IBankView Bank { get; }
        public bool DisplayBalance { get; set; }
        public string PlayerName { get; set; }
    }

    public interface IBlackjackCountView
    {
        public void SetCount(BlackjackCountEnum system, float count);
    }

    public interface IShoeView
    {
        public int CutIndex { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }

        public void Burn(int n);
        public void Shuffle();
    }

    public interface IBlackjackTableSlotView
    {
        public IBlackjackPlayerView Player { get;}
        public IBankView[] Pots { get; }
        public IBlackjackHandView[] Hands { get; }
        public IBankView InsurancePot { get; }
        public int Index { get; }
    }

    public interface IBlackjackConfigView
    {
        public Dictionary<string, string> Config { get; set; }
        public BlackjackConfigPresenter Presenter { get; set; }
    }

    public interface IBlackjackDecisionView
    {
        public BlackjackActionEnum Action { get; }
        public event EventHandler<EventArgs> DecisionMade;
        public void Prompt(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions);
    }

    public interface IBlackjackBetView
    {
        public int Bet { get; }
        public event EventHandler<EventArgs> BetMade;
        public void Prompt(int minimumBet);
    }

    public interface IBlackjackInsuranceView
    {
        public bool Insured { get; }
        public event EventHandler<EventArgs> DecisionMade;
        public void Prompt(BlackjackHand hand, Card upCard);
    }

    public interface IBlackjackEarlySurrenderView
    {
        public bool Surrendered { get; }
        public event EventHandler<EventArgs> DecisionMade;
        public void Prompt(BlackjackHand hand, Card upCard);
    }

    public interface ILogView
    {
        public void Log(string entry);
        public void Clear();
    }

    public interface IBlackjackTableView
    {
        public ILogView Log { get; }
        public IBlackjackConfigView Config { get; }
        public IBlackjackTableSlotView DealerSlot { get; }
        public IBlackjackTableSlotView[] PlayerSlots { get; }
        public IBlackjackDecisionView Decision { get; }
        public IBlackjackBetView Bet { get; }
        public IBlackjackInsuranceView Insurance { get; }
        public IBlackjackEarlySurrenderView Surrender { get; }
        public IBankView Bank { get; }
        public IShoeView Shoe { get; }
        public IBlackjackCountView Count { get; }

        public event EventHandler<EventArgs> RoundStarted;

        public void EndRound();
    }
}

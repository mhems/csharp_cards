using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using Blackjack;
using System.Threading;

namespace BlackjackViewPresenter
{
    public interface ICardView
    {
        public Card.RankEnum Rank { get; set; }
        public Card.SuitEnum Suit { get; set; }
    }

    public interface IBlackjackHandView
    {
        public bool IsDealer { get; set; }
        public int Value { get; set; }
        public bool Bust { get; set; }
        public bool Blackjack { get; set; }
        public void ClearHand();
        public void AddCard(Card card, bool visible=true);
        public void RevealHoleCard();
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
        public bool IsDealer { get; set; }
    }

    public interface IBlackjackConfigView
    {
        public Dictionary<string, string> Config { get; set; }
        public BlackjackConfigPresenter Presenter { get; set; }
    }

    public interface IBlackjackDecisionView
    {
        public AutoResetEvent Signal { get; set; }
        public BlackjackActionEnum Action { get; set; }
        public void Prompt(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions);
    }

    public interface IBlackjackBetView
    {
        public AutoResetEvent Signal { get; set; }
        public int Bet { get; set; }
        public void Prompt(int minimumBet, int maximumBet);
    }

    public interface IBlackjackInsuranceView
    {
        public AutoResetEvent Signal { get; set; }
        public bool Insure { get; set; }
        public void Prompt(BlackjackHand hand, Card upCard);
    }

    public interface IBlackjackEarlySurrenderView
    {
        public AutoResetEvent Signal { get; set; }
        public bool Surrender { get; set; }
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

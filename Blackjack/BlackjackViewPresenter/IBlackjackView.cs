﻿using System;
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
        public void ClearHand();
        public void AddCard(ICardView cardView);
    }

    public interface IBankView
    {
        public int Balance { get; set; }
    }

    public interface IPlayerView
    {
        public IBankView Bank { get; set; }
        public string Name { get; set; }
    }

    public interface IBlackjackCountView
    {
        public void SetCount(BlackjackCountEnum system, int count);
        public void Clear();
    }

    public interface IShoeView
    {
        public int CutIndex { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
    }

    public interface IBlackjackTableSlotView
    {
        public IPlayerView Player { get; set; }
        public IBankView[] Pots { get; set; }
        public IBlackjackHandView[] Hands { get; set; }
        public IBankView InsurancePot { get; set; }
        public int Index { get; set; }
    }

    public interface IBlackjackConfigView
    {
        public void Edit();
        public void Save();
    }

    public interface IBlackjackActionView
    {

    }

    public interface IBlackjackTableView
    {
        public IBlackjackConfigView Config { get; set; }
        public IBlackjackTableSlotView[] Slots { get; set; }
        public IBlackjackTableSlotView DealerSlot { get; set; }
        public IBlackjackActionView Action { get; set; }
        public IBankView Bank { get; set; }
        public IShoeView Shoe { get; set; }
        public IBlackjackCountView Count { get; set; }
    }
}
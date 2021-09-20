using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using Blackjack;

namespace TestBlackjack
{
    public class TestShoe : Shoe
    {
        public TestShoe(IEnumerable<Card> cards)
        {
            base.cards.Clear();
            base.cards.AddRange(cards);
        }
    }

    public static class TestUtilities
    {
        public static TestShoe MakeTestShoe(IEnumerable<Card> cards)
        {
            return new TestShoe(cards);
        }

        public static TestShoe MakeTestShoe(IEnumerable<Card.RankEnum> ranks)
        {
            return new TestShoe(ranks.Select(r => CardFactory.GetCard(r, Card.SuitEnum.Clubs)));
        }

        public static BlackjackPlayer MakeTestPlayer(int bankroll=1000)
        {
            BlackjackPlayer p = new ("test player");
            p.Bank.Deposit(bankroll);
            return p;
        }

        public static BlackjackTableSlot MakeTestSlot()
        {
            IBlackjackConfig config = new StandardBlackjackConfig();
            BlackjackTableSlot slot = new(config);
            slot.Player = MakeTestPlayer();
            slot.Player.Bank.Transfer(slot.Pot, config.MinimumBet);
            return slot;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;
using Cards;

namespace Driver
{
    public class InputDecisionPolicy : BlackjackDecisionPolicy
    {
        protected override BlackjackActionEnum DecideInner(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions)
        {
            BlackjackActionEnum choice;

            Console.WriteLine($"you have hand {hand} (Value={hand.Value}) vs {upCard.Rank}");
            Console.WriteLine($"choose one of [{string.Join(", ", availableActions)}]");

            while (true)
            {
                string input = Console.ReadLine();
                try
                {
                    choice = availableActions.Where(c => c.ToString().ToLower().StartsWith(input.ToLower())).Single();
                    break;
                }
                catch (InvalidOperationException _)
                {
                    Console.WriteLine($"unable to determine what you meant by '{input}', please enter full command");
                }
            }

            Console.WriteLine($"you have decided to {choice}");
            return choice;
        }
    }
}

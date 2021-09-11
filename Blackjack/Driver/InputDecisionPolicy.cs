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
        private readonly IEnumerable<BlackjackActionEnum> choices = Enum.GetValues(typeof(BlackjackActionEnum)).OfType<BlackjackActionEnum>();
        protected override BlackjackActionEnum DecideInner(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions)
        {
            BlackjackActionEnum choice;

            Console.WriteLine($"you have hand {hand} vs {upCard.Rank}");
            Console.WriteLine($"choose one of [{string.Join(", ", availableActions)}]");
            string input = Console.ReadLine();

            while (true)
            {
                try
                {
                    choice = choices.Where(c => c.ToString().ToLower().StartsWith(input.ToLower())).Single();
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

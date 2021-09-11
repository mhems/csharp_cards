using System;
using Cards;
using Blackjack;

namespace Driver
{
    public class Program
    {
        static void Main(string[] args)
        {
            BlackjackPlayer human = new("me");
            human.Bank.Deposit(1000);

            IBlackjackConfig config = new StandardBlackjackConfig();

            human.BettingPolicy = new MinimumBettingPolicy(config);
            human.DecisionPolicy = new InputDecisionPolicy();
            human.InsurancePolicy = new DeclineInsurancePolicy();

            BlackjackTable table = new(1, config);
            BlackjackEventLogger eventLogger = new();
            table.AddLogger(eventLogger);
            FileLogger fileLogger = new("log.txt");
            eventLogger.Logging += fileLogger.OnEventMessage;

            int numRounds = 0;
            while(true)
            {
                table.PlayRound();

                Console.WriteLine($"{numRounds} rounds played, you have ${human.Bank}");
                Console.WriteLine("continue (y/n)? > ");
                if (Console.ReadLine().ToLower().StartsWith("n"))
                {
                    break;
                }
                numRounds++;
            }
        }
    }
}

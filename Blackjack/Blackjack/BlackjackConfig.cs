using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{    
    public static class BlackjackConfig
    {
        public static IBlackjackConfig Config { get;} = new StaticBlackjackConfig();
    }

    public interface IBlackjackConfig
    {
        public bool DealerHitsSoft17 { get; }

        public int NumDecksInShoe { get; }
        public int CutIndex { get; }
        public int NumBurntOnShuffle { get; }

        public int MinimumBet { get; }
        public int MaximumBet { get; }

        public double PayoutRatio { get; }
        public double BlackjackPayoutRatio { get; }

        public bool DoubleOffered { get; }
        public bool DoubleAfterSplit { get; }
        public uint DoubleTotalsAllowed { get; }
        public double DoubleCost { get; }

        public bool SplitOffered { get; }
        public bool SplitByValue { get; }
        public int NumSplitsAllowed { get; }
        public bool ReSplitAces { get; }
        public bool HitSplitAces { get; }
        public double SplitCost { get; }

        public bool EarlySurrenderOffered { get; }
        public double EarlySurrenderCost { get; }

        public bool LateSurrenderOffered { get; }
        public double LateSurrenderCost { get; }

        public bool InsuranceOffered { get; }
        public double InsuranceCost { get; }
        public double InsurancePayoutRatio { get; }
    }

    public class StaticBlackjackConfig : IBlackjackConfig
    {
        public bool DealerHitsSoft17 { get; } = true;

        public int NumDecksInShoe { get; } = 6;
        public int CutIndex { get; } = 26;
        public int NumBurntOnShuffle { get; } = 1;

        public int MinimumBet { get; } = 10;
        public int MaximumBet { get; } = 250;

        public double PayoutRatio { get; } = 1;
        public double BlackjackPayoutRatio { get; } = 1.5;

        public bool DoubleOffered { get; } = true;
        public bool DoubleAfterSplit { get; } = true;
        public uint DoubleTotalsAllowed { get; } = 0x3F_FFFF;
        public double DoubleCost { get; } = 1;

        public bool SplitOffered { get; } = true;
        public bool SplitByValue { get; } = false;
        public int NumSplitsAllowed { get; } = 4;
        public bool ReSplitAces { get; } = true;
        public bool HitSplitAces { get; } = false;
        public double SplitCost { get; } = 1;

        public bool EarlySurrenderOffered { get; } = false;
        public double EarlySurrenderCost { get; } = 0.5;

        public bool LateSurrenderOffered { get; } = true;
        public double LateSurrenderCost { get; } = 0.5;

        public bool InsuranceOffered { get; } = true;
        public double InsuranceCost { get; } = 0.5;
        public double InsurancePayoutRatio { get; } = 2;
    }
}

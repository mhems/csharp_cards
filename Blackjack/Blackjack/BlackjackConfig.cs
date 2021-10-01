using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public interface IBlackjackConfig
    {
        public bool DealerHitsSoft17 { get; set; }

        public int NumDecksInShoe { get; set; }
        public int CutIndex { get; set; }
        public int NumBurntOnShuffle { get; set; }

        public int MinimumBet { get; set; }
        public int MaximumBet { get; set; }

        public double PayoutRatio { get; set; }
        public double BlackjackPayoutRatio { get; set; }

        public bool DoubleOffered { get; set; }
        public bool DoubleAfterSplit { get; set; }
        public uint DoubleTotalsAllowed { get; set; }
        public double DoubleCost { get; set; }

        public bool SplitOffered { get; set; }
        public bool SplitByValue { get; set; }
        public int NumSplitsAllowed { get; set; }
        public bool ReSplitAces { get; set; }
        public bool HitSplitAces { get; set; }
        public double SplitCost { get; set; }

        public bool EarlySurrenderOffered { get; set; }
        public double EarlySurrenderCost { get; set; }

        public bool LateSurrenderOffered { get; set; }
        public double LateSurrenderCost { get; set; }

        public bool InsuranceOffered { get; set; }
        public double InsuranceCost { get; set; }
        public double InsurancePayoutRatio { get; set; }
    }

    public class StandardBlackjackConfig : IBlackjackConfig
    {
        public bool DealerHitsSoft17 { get; set; } = true;

        public int NumDecksInShoe { get; set; } = 6;
        public int CutIndex { get; set; } = 26;
        public int NumBurntOnShuffle { get; set; } = 1;

        public int MinimumBet { get; set; } = 10;
        public int MaximumBet { get; set; } = 250;

        public double PayoutRatio { get; set; } = 1;
        public double BlackjackPayoutRatio { get; set; } = 1.5;

        public bool DoubleOffered { get; set; } = true;
        public bool DoubleAfterSplit { get; set; } = true;
        public uint DoubleTotalsAllowed { get; set; } = 0x3F_FFFF;
        public double DoubleCost { get; set; } = 1;

        public bool SplitOffered { get; set; } = true;
        public bool SplitByValue { get; set; } = false;
        public int NumSplitsAllowed { get; set; } = 4;
        public bool ReSplitAces { get; set; } = true;
        public bool HitSplitAces { get; set; } = false;
        public double SplitCost { get; set; } = 1;

        public bool EarlySurrenderOffered { get; set; } = false;
        public double EarlySurrenderCost { get; set; } = 0.5;

        public bool LateSurrenderOffered { get; set; } = true;
        public double LateSurrenderCost { get; set; } = 0.5;

        public bool InsuranceOffered { get; set; } = true;
        public double InsuranceCost { get; set; } = 0.5;
        public double InsurancePayoutRatio { get; set; } = 2;
    }
}

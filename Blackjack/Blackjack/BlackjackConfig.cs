using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class BlackjackConfig
    {
        public const bool DealerHitsSoft17 = true;

        public const int NumDecksInShoe = 6;
        public const int CutIndex = 26;
        public const int NumBurntOnShuffle = 1;

        public const int MinimumBet = 10;
        public const int MaximumBet = 250;

        public const double PayoutRatio = 1;
        public const double BlackjackPayoutRatio = 1.5;

        public const bool DoubleOffered = true;
        public const bool DoubleAfterSplit = true;
        public const uint DoubleTotalsAllowed = 0x3F_FFFF;
        public const double DoubleCost = 1;

        public const bool SplitOffered = true;
        public const bool SplitByValue = false;
        public const int NumSplitsAllowed = 4;
        public const bool ReSplitAces = true; // TODO
        public const bool HitSplitAces = false; // TODO
        public const double SplitCost = 1;

        public const bool EarlySurrenderOffered = false;
        public const double EarlySurrenderCost = 0.5;

        public const bool LateSurrenderOffered = true;
        public const double LateSurrenderCost = 0.5;

        public const bool InsuranceOffered = true;
        public const double InsuranceCost = 0.5;
        public const double InsurancePayoutRatio = 2;
    }
}

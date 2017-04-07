using System;
using System.Collections.Generic;

namespace PEngine
{
    public sealed class SimResults
    {
        public Int32 Players { get; set; }

        public Int32 Games { get; set; }

        public Dictionary<CardsAnalyser.HandRank, Int32> HandsStats { get; set; }

        public Dictionary<CardsAnalyser.HandRank, Int32> WinningHandsStats { get; set; }

        public Dictionary<String, Int32> PocketCardsWinnersStats { get; set; }

        public Dictionary<String, Int32> PocketCardsLoserStats { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PEngine
{
    public static class WinnersStats
    {
        public static void PrintResults(Dictionary<Int32, SimResults> allResults, Func<SimResults, Dictionary<CardsAnalyser.HandRank, Int32>> resultsSelector)
        {
            Array items = PrintHeaders();

            StringBuilder sb = new StringBuilder();

            for (Int32 q = 2; q < 10; q++)
            {
                Dictionary<CardsAnalyser.HandRank, Int32> winnersCounters = resultsSelector(allResults[q]);

                sb.Append(q).Append(";");

                for (Int32 w = 0; w < items.Length; w++)
                {
                    CardsAnalyser.HandRank rank = (CardsAnalyser.HandRank)items.GetValue(w);

                    Int32 value = 0;

                    winnersCounters.TryGetValue(rank, out value);

                    sb.Append(value).Append(";");
                }

                Console.WriteLine(sb.ToString());

                sb.Length = 0;
            }
        }

        public static Array PrintHeaders()
        {
            Array items = Enum.GetValues(typeof(CardsAnalyser.HandRank));

            Console.WriteLine();

            Console.Write("Players;");

            for (Int32 w = 0; w < items.Length; w++)
            {
                Console.Write("{0};", items.GetValue(w));
            }

            Console.WriteLine();

            return items;
        }
    }
}

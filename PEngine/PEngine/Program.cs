﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PEngine
{
    public static class Program
    {
        private static void TryUpdateCounterOrAdd<T>(this Dictionary<T, Int32> countersMap, T item)
        {
            Int32 counter;

            if (countersMap.TryGetValue(item, out counter))
            {
                countersMap[item] = counter + 1;
            }
            else
            {
                countersMap.Add(item, 1);
            }
        }

        private static Dictionary<CardsAnalyser.HandRank, Int32> Sim1(Int32 players, Int32 maxGames)
        {
            List<Card[]> playerCards = new List<Card[]>(players);

            Dictionary<CardsAnalyser.HandRank, Int32> counters = new Dictionary<CardsAnalyser.HandRank, Int32>();

            IRandom random = new MersenneTwister((UInt64)Environment.TickCount);

            CardsAnalyser.Result[] singleMatchResults = new CardsAnalyser.Result[players];

            Dictionary<CardsAnalyser.HandRank, Int32> winnersCounters = new Dictionary<CardsAnalyser.HandRank, Int32>();

            for (Int32 g = 0; g < maxGames; g++)
            {
                DeckOfCards deckOfCards = new DeckOfCards();

                deckOfCards.Shuffle(random, 2000);

                playerCards.Clear();

                for (Int32 q = 0; q < players; q++)
                {
                    Card[] cards = deckOfCards.Draw(2);

                    playerCards.Add(cards);

                    //Console.WriteLine("{0} {1}", cards[0], cards[1]);
                }

                Card[] cards2 = deckOfCards.Draw(5);

                //Console.WriteLine("{0} {1} {2} {3} {4}", cards2[0], cards2[1], cards2[2], cards2[3], cards2[4]);

                for (Int32 q = 0; q < players; q++)
                {
                    List<Card> cc = new List<Card>(cards2);

                    cc.AddRange(playerCards[q]);

                    CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(cc.ToArray());

                    counters.TryUpdateCounterOrAdd(result.Rank);

                    singleMatchResults[q] = result;
                }

                CardsAnalyser.Result winner = CardsAnalyser.GetWinner(singleMatchResults);

                winnersCounters.TryUpdateCounterOrAdd(winner.Rank);
            }

            return winnersCounters;
        }

        private static void SimWrapper(Int32 maxGames)
        {
            Dictionary<Int32, Dictionary<CardsAnalyser.HandRank, Int32>> allResults = new Dictionary<Int32, Dictionary<CardsAnalyser.HandRank, Int32>>();

            for (Int32 q = 2; q < 10; q++)
            {
                Console.WriteLine(q);

                Dictionary<CardsAnalyser.HandRank, Int32> winnersCounters = Sim1(q, maxGames);

                allResults.Add(q, winnersCounters);
            }

            PrintResults(allResults);
        }

        private static void PrintResults(Dictionary<Int32, Dictionary<CardsAnalyser.HandRank, Int32>> allResults)
        {
            Array items = PrintHeaders();

            StringBuilder sb = new StringBuilder();

            for (Int32 q = 2; q < 9; q++)
            {
                Dictionary<CardsAnalyser.HandRank, Int32> winnersCounters = allResults[q];

                for (Int32 w = 0; w < items.Length; w++)
                {
                    CardsAnalyser.HandRank rank = (CardsAnalyser.HandRank)items.GetValue(w);

                    Int32 value = 0;

                    winnersCounters.TryGetValue(rank, out value);

                    sb.Append(value).Append("\t");
                }

                Console.WriteLine(sb.ToString());

                sb.Length = 0;
            }
        }

        private static Array PrintHeaders()
        {
            Array items = Enum.GetValues(typeof(CardsAnalyser.HandRank));

            Console.WriteLine();

            Console.Write("Players\t");

            for (Int32 w = 0; w < items.Length; w++)
            {
                Console.Write("{0}\t", items.GetValue(w));
            }

            Console.WriteLine();

            return items;
        }

        static void Main(string[] args)
        {
            //SimWrapper(100);
            SimWrapper(1000000);
        }
    }
}

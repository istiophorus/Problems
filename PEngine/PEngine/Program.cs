using System;
using System.Collections.Generic;

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

        static void Main(string[] args)
        {
            Int32 players = 2;

            List<Card[]> playerCards = new List<Card[]>(players);

            Dictionary<CardsAnalyser.HandRank, Int32> counters = new Dictionary<CardsAnalyser.HandRank, Int32>();

            IRandom random = new MersenneTwister((UInt64)Environment.TickCount);

            CardsAnalyser.Result[] singleMatchResults = new CardsAnalyser.Result[players];

            Dictionary<CardsAnalyser.HandRank, Int32> winnersCounters = new Dictionary<CardsAnalyser.HandRank, Int32>();

            for (Int32 g = 0; g < 10000000; g++)
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
        }
    }
}

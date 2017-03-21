using System;
using System.Collections.Generic;

namespace PEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 players = 6;

            List<Card[]> playerCards = new List<Card[]>(players);

            Dictionary<CardsAnalyser.HandRank, Int32> counters = new Dictionary<CardsAnalyser.HandRank, Int32>();

            IRandom random = new MersenneTwister((UInt64)Environment.TickCount);

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

                    //Console.WriteLine(result.Rank);

                    Int32 counter;

                    if (counters.TryGetValue(result.Rank, out counter))
                    {
                        counters[result.Rank] = counter + 1;
                    }
                    else
                    {
                        counters.Add(result.Rank, 1);
                    }
                }
            }
        }
    }
}

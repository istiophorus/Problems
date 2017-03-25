using System;
using System.Collections.Generic;
using System.Text;

namespace PEngine
{
    public static class Program
    {
        private static Char ShortSymbol(CardSymbol symbol)
        {
            Char result;

            switch (symbol)
            {
                case CardSymbol.Two:
                    result = '2';
                    break;

                case CardSymbol.Three:
                    result = '3';
                    break;

                case CardSymbol.Four:
                    result = '4';
                    break;

                case CardSymbol.Five:
                    result = '5';
                    break;

                case CardSymbol.Six:
                    result = '6';
                    break;

                case CardSymbol.Seven:
                    result = '7';
                    break;

                case CardSymbol.Eight:
                    result = '8';
                    break;

                case CardSymbol.Nine:
                    result = '9';
                    break;

                case CardSymbol.Ten:
                    result = 'T';
                    break;

                case CardSymbol.Jack:
                    result = 'J';
                    break;

                case CardSymbol.Queen:
                    result = 'Q';
                    break;

                case CardSymbol.King:
                    result = 'K';
                    break;

                case CardSymbol.Ace:
                    result = 'A';
                    break;

                default:
                    throw new ArgumentException(symbol.ToString());
            }

            return result;
        }

        private static CardSymbol MinValue(CardSymbol a, CardSymbol b)
        {
            if (a.CompareTo(b) >= 0)
            {
                return b;
            }
            else
            {
                return a;
            }
        }

        private static CardSymbol MaxValue(CardSymbol a, CardSymbol b)
        {
            if (a.CompareTo(b) >= 0)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        private static String GetCardsState(Card cardA, Card cardB)
        {
            Char s1 = ShortSymbol(MaxValue(cardA.Symbol, cardB.Symbol));

            Char s2 = ShortSymbol(MinValue(cardA.Symbol, cardB.Symbol));

            String result = String.Format("{0}{1}", s1, s2);

            if (cardA.Suit != cardB.Suit)
            {
                result = result.ToLowerInvariant() + "-u";
            }
            else
            {
                result = result.ToLowerInvariant() + "-s";
            }

            return result;
        }

        private static Dictionary<String, Int32> WinnerPocketCardsStats(Int32 players, Int32 maxGames)
        {
            List<Card[]> playerCards = new List<Card[]>(players);

            IRandom random = new MersenneTwister((UInt64)Environment.TickCount);

            CardsAnalyser.Result[] singleMatchResults = new CardsAnalyser.Result[players];

            Dictionary<String, Int32> winnersCounters = new Dictionary<String, Int32>();

            for (Int32 g = 0; g < maxGames; g++)
            {
                DeckOfCards deckOfCards = new DeckOfCards();

                deckOfCards.Shuffle(random, 2000);

                playerCards.Clear();

                for (Int32 q = 0; q < players; q++)
                {
                    Card[] cards = deckOfCards.Draw(2);

                    playerCards.Add(cards);
                }

                Card[] cards2 = deckOfCards.Draw(5);

                for (Int32 q = 0; q < players; q++)
                {
                    List<Card> cc = new List<Card>(cards2);

                    cc.AddRange(playerCards[q]);

                    CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(cc.ToArray());

                    singleMatchResults[q] = result;
                }

                Tuple<CardsAnalyser.Result, Int32> winner = CardsAnalyser.GetWinner(singleMatchResults);

                Card[] initialCards = playerCards[winner.Item2];

                String state = GetCardsState(initialCards[0], initialCards[1]);

                winnersCounters.TryUpdateCounterOrAdd(state);
            }

            return winnersCounters;
        }

        private static void Sim2Wrapper(Int32 maxGames)
        {
            //Dictionary<Int32, Dictionary<CardsAnalyser.HandRank, Int32>> allResults = new Dictionary<Int32, Dictionary<CardsAnalyser.HandRank, Int32>>();

            //for (Int32 q = 2; q < 10; q++)
            {
                Int32 q = 2;

                Console.WriteLine(q);

                Dictionary<String, Int32> winnersCounters = WinnerPocketCardsStats(q, maxGames);

              //  allResults.Add(q, winnersCounters);
            }

            //rintResults(allResults);
        }

        static void Main(string[] args)
        {
            Sim2Wrapper(100000);

            //SimWrapper(100);
            WinnersStats.WinnersStatsSimWrapper(1000000);

            Sim2Wrapper(1000000);
        }
    }
}

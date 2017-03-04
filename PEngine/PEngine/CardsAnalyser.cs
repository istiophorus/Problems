using System;
using System.Collections.Generic;

namespace PEngine
{
    public static class CardsAnalyser
    {
        public enum HandRank
        {
            HighCard = 0,
            Pair,
            TwoPairs,
            ThreeOfkind,
            Straight,
            Flush,
            FullHouse,
            FourOfKind,
            StraightFlush,
            RoyalFLush
        }

        public sealed class Result
        {
            public HandRank Rank { get; set; }
        }

        public static Result Analyse(Card[] cards)
        {
            Int32[][] cardsMap = new Int32[4][] { new Int32[13], new Int32[13], new Int32[13], new Int32[13] };

            Int32[] counters = new Int32[13];

            Int32[] suitsCounters = new Int32[4];

            Boolean isFlush = false;

            for (Int32 q = 0; q < cards.Length; q++)
            {
                Card card = cards[q];

                Int32 index = (Int32)card.Symbol - 2;

                counters[index]++;

                Int32 suitIndex = (Int32)card.Suit - 1;

                cardsMap[suitIndex][index] = 1;

                suitsCounters[suitIndex]++;

                if (suitsCounters[suitIndex] >= 5)
                {
                    isFlush = true;
                }
            }

            HandRank currentHand = HandRank.HighCard;

            Int32 pairs = 0;

            Int32 threes = 0;

            Int32 longestSequence = 0;

            Int32 currentSequence = 0;

            Int32[] currentSuitedSequence = new Int32[4];

            Int32[] longestSuitedSequence = new Int32[4];

            Int32 fours = 0;

            if (isFlush)
            {
                currentHand = HandRank.Flush;
            }

            for (Int32 q = 0; q < counters.Length; q++)
            {
                Int32 current = counters[q];

                if (current == 0)
                {
                    currentSequence = 0;

                    currentSuitedSequence[0] = 0;
                    currentSuitedSequence[1] = 0;
                    currentSuitedSequence[2] = 0;
                    currentSuitedSequence[3] = 0;
                }
                else
                {
                    currentSequence++;

                    if (currentSequence > longestSequence)
                    {
                        longestSequence = currentSequence;
                    }

                    for (Int32 s = 0; s < 4; s++)
                    {
                        currentSuitedSequence[s] += cardsMap[s][q];

                        if (currentSuitedSequence[s] > longestSuitedSequence[s])
                        {
                            longestSuitedSequence[s] = currentSuitedSequence[s];
                        }

                        if (longestSuitedSequence[s] >= 5)
                        {
                            if (currentHand < HandRank.StraightFlush)
                            {
                                currentHand = HandRank.StraightFlush;
                            }
                        }

                        if (q == counters.Length - 1 /* last card */)
                        {
                            if (currentSuitedSequence[s] >= 5)
                            {
                                if (currentHand < HandRank.RoyalFLush)
                                {
                                    currentHand = HandRank.RoyalFLush;
                                }
                            }
                        }
                    }

                    if (current == 4)
                    {
                        fours++;
                    }
                    else if (current == 3)
                    {
                        threes++;
                    }
                    else if (current == 2)
                    {
                        pairs++;
                    }
                }
            }

            if (fours > 0)
            {
                if (currentHand < HandRank.FourOfKind)
                {
                    currentHand = HandRank.FourOfKind;
                }
            }
            else
            {
                if (threes >= 2 ||
                    (threes == 1 && pairs >= 1))
                {
                    if (currentHand < HandRank.FullHouse)
                    {
                        currentHand = HandRank.FullHouse;
                    }
                }
            }

            if (threes >= 1)
            {
                if (currentHand < HandRank.ThreeOfkind)
                {
                    currentHand = HandRank.ThreeOfkind;
                }
            }

            if (pairs >= 2)
            {
                if (currentHand < HandRank.TwoPairs)
                {
                    currentHand = HandRank.TwoPairs;
                }
            }

            if (pairs == 1)
            {
                if (currentHand < HandRank.Pair)
                {
                    currentHand = HandRank.Pair;
                }
            }

            if (longestSequence >= 5)
            {
                if (currentHand < HandRank.Straight)
                {
                    currentHand = HandRank.Straight;
                }
            }

            return new Result
                {
                    Rank = currentHand
                };
        }
    }
}

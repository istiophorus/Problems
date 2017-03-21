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

            public Card[] MainHandCards { get; set; }

            public Card[] ResidualCards { get; set; }
        }

        private static readonly CardSuit[] SuitsIndexed = new CardSuit[]
        {
            CardSuit.Spades,
            CardSuit.Clubs,
            CardSuit.Diamond,
            CardSuit.Heart
        };

        private static readonly CardSymbol[] SymbolsIndexed = new CardSymbol[]
        {
            CardSymbol.Two,
            CardSymbol.Three,
            CardSymbol.Four,
            CardSymbol.Five,
            CardSymbol.Six,
            CardSymbol.Seven,
            CardSymbol.Eight,
            CardSymbol.Nine,
            CardSymbol.Ten,
            CardSymbol.Jack,
            CardSymbol.Queen,
            CardSymbol.King,
            CardSymbol.Ace
        };

        private static Card GetCard(Int32 symbolIndex, Int32 suitIndex)
        {
            return new Card(SymbolsIndexed[symbolIndex], SuitsIndexed[suitIndex]);
        }

        public static Result Analyse(Card[] cards)
        {
            Int32[][] cardsBitMap = new Int32[4][] { new Int32[13], new Int32[13], new Int32[13], new Int32[13] };

            Int32[] counters = new Int32[13];

            CardsMap cardsMap = new CardsMap();

            List<Card> flushCards = CountCardsAndPrepareMaps(cards, cardsBitMap, counters, cardsMap);

            HandRank currentHand = HandRank.HighCard;

            if (flushCards != null && flushCards.Count >= HandCardsCount)
            {
                currentHand = HandRank.Flush;
            }

            Int32 pairs = 0;

            Int32 threes = 0;

            List<Int32> currentCardsSequence = new List<Int32>(7);

            List<Int32> longestCardsSequence = new List<Int32>(7);

            List<Card>[] currentSuitedSequence = new List<Card>[]
                {
                    new List<Card>(7),
                    new List<Card>(7),
                    new List<Card>(7),
                    new List<Card>(7),
                };

            List<Card>[] longestSuitedSequence = new List<Card>[]
                {
                    new List<Card>(7),
                    new List<Card>(7),
                    new List<Card>(7),
                    new List<Card>(7),
                };

            Int32 fours = 0;
            Int32 highestThree = -1;
            Int32 highestFour = -1;
            Int32 highestTwo = -1;

            List<Int32> pairsList = new List<Int32>(4);

            List<Int32> threesList = new List<Int32>(4);

            List<Card> selectedLongestSuitedSequence = null;

            for (Int32 q = 0; q < counters.Length; q++)
            {
                Int32 current = counters[q];

                if (current == 0)
                {
                    currentCardsSequence = new List<Int32>(7);

                    currentSuitedSequence[0] = new List<Card>(7);
                    currentSuitedSequence[1] = new List<Card>(7);
                    currentSuitedSequence[2] = new List<Card>(7);
                    currentSuitedSequence[3] = new List<Card>(7);
                }
                else
                {
                    currentCardsSequence.Add(q);

                    if (currentCardsSequence.Count > longestCardsSequence.Count)
                    {
                        longestCardsSequence = currentCardsSequence;
                    }

                    for (Int32 suitIndex = 0; suitIndex < 4; suitIndex++)
                    {
                        if (cardsBitMap[suitIndex][q] != 0)
                        {
                            currentSuitedSequence[suitIndex].Add(GetCard(q, suitIndex));
                        }

                        if (currentSuitedSequence[suitIndex].Count > longestSuitedSequence[suitIndex].Count)
                        {
                            longestSuitedSequence[suitIndex] = currentSuitedSequence[suitIndex];
                        }

                        if (longestSuitedSequence[suitIndex].Count >= 5)
                        {
                            if (currentHand < HandRank.StraightFlush)
                            {
                                currentHand = HandRank.StraightFlush;

                                selectedLongestSuitedSequence = longestSuitedSequence[suitIndex];
                            }
                        }

                        if (q == counters.Length - 1 /* last card */)
                        {
                            if (currentSuitedSequence[suitIndex].Count >= 5)
                            {
                                if (currentHand < HandRank.RoyalFLush)
                                {
                                    currentHand = HandRank.RoyalFLush;

                                    selectedLongestSuitedSequence = currentSuitedSequence[suitIndex];
                                }
                            }
                        }
                    }

                    if (current == 4)
                    {
                        fours++;

                        if (q > highestFour)
                        {
                            highestFour = q;
                        }
                    }
                    else if (current == 3)
                    {
                        threes++;

                        threesList.Add(q);

                        if (q > highestThree)
                        {
                            highestThree = q;
                        }
                    }
                    else if (current == 2)
                    {
                        pairs++;

                        pairsList.Add(q);

                        if (q > highestTwo)
                        {
                            highestTwo = q;
                        }
                    }
                }
            }

            List<Card> selectedHandCards = new List<Card>();

            if (fours > 0)
            {
                if (currentHand < HandRank.FourOfKind)
                {
                    currentHand = HandRank.FourOfKind;

                    selectedHandCards.Clear();

                    selectedHandCards.AddRange(cardsMap.Get(highestFour));
                }
            }
            else
            {
                if (threes == 1 && pairs >= 1)
                {
                    if (currentHand < HandRank.FullHouse)
                    {
                        currentHand = HandRank.FullHouse;

                        selectedHandCards.Clear();

                        selectedHandCards.AddRange(cardsMap.Get(highestThree));

                        selectedHandCards.AddRange(cardsMap.Get(highestTwo));
                    }
                }
                else if (threes >= 2)
                {
                    if (currentHand < HandRank.FullHouse)
                    {
                        currentHand = HandRank.FullHouse;

                        selectedHandCards.Clear();

                        threesList.Sort();

                        selectedHandCards.AddRange(cardsMap.Get(threesList[threesList.Count - 1]));

                        selectedHandCards.AddRange(cardsMap.Get(threesList[threesList.Count - 2], 2));
                    }
                }
            }

            if (threes >= 1)
            {
                if (currentHand < HandRank.ThreeOfkind)
                {
                    currentHand = HandRank.ThreeOfkind;

                    selectedHandCards.Clear();

                    selectedHandCards.AddRange(cardsMap.Get(highestThree));
                }
            }

            if (pairs >= 2)
            {
                if (currentHand < HandRank.TwoPairs)
                {
                    pairsList.Sort();

                    currentHand = HandRank.TwoPairs;

                    selectedHandCards.Clear();

                    selectedHandCards.AddRange(cardsMap.Get(pairsList[pairsList.Count - 1]));

                    selectedHandCards.AddRange(cardsMap.Get(pairsList[pairsList.Count - 2]));
                }
            }

            if (pairs == 1)
            {
                if (currentHand < HandRank.Pair)
                {
                    currentHand = HandRank.Pair;

                    selectedHandCards.Clear();

                    selectedHandCards.AddRange(cardsMap.Get(highestTwo));
                }
            }

            if (longestCardsSequence.Count >= 5)
            {
                if (currentHand < HandRank.Straight)
                {
                    currentHand = HandRank.Straight;

                    EnsureNotMoreThen(longestCardsSequence, HandCardsCount);

                    selectedHandCards.Clear();

                    longestCardsSequence.ForEach(x => selectedHandCards.Add(cardsMap.Get(x, 1)[0]));
                }
            }

            if (currentHand == HandRank.RoyalFLush ||
                currentHand == HandRank.StraightFlush)
            {
                if (selectedLongestSuitedSequence.Count >= HandCardsCount)
                {
                    selectedHandCards.Clear();

                    EnsureNotMoreThen(selectedLongestSuitedSequence, HandCardsCount);

                    selectedHandCards.AddRange(selectedLongestSuitedSequence);
                }
            }

            if (currentHand == HandRank.Flush)
            {
                selectedHandCards.Clear();

                selectedHandCards.AddRange(flushCards);
            }

            return new Result
                {
                    Rank = currentHand,
                    MainHandCards = selectedHandCards.ToArray(),
                    ResidualCards = PrepareResidualCards(cards, selectedHandCards)
            };
        }

        private static List<T> EnsureNotMoreThen<T>(List<T> input, Int32 count)
        {
            input.Sort();

            while (input.Count > count)
            {
                input.RemoveAt(0);
            }

            return input;
        }

        public static readonly Int32 HandCardsCount = 5;

        private static readonly Card[] EmptyCardsArray = new Card[0];

        private static Card[] PrepareResidualCards(Card[] inputCards, List<Card> mainHandCards)
        {
            Int32 cardsToChoose = HandCardsCount - mainHandCards.Count;

            Int32 cardsLeft = inputCards.Length - mainHandCards.Count;

            if (cardsToChoose < 0)
            {
                throw new ApplicationException(String.Format("Invalid main hand cards count {0}", mainHandCards.Count));
            }

            if (cardsLeft <= 0)
            {
                return EmptyCardsArray;
            }

            if (cardsToChoose == 0)
            {
                return EmptyCardsArray;
            }

            List<Card> result = new List<Card>(inputCards);

            mainHandCards.ForEach(card => result.Remove(card));

            EnsureNotMoreThen(result, cardsToChoose);

            //// it should be sorted ascending tehrefore we have to change the order

            result.Reverse();

            return result.ToArray();
        }

        private static List<Card> CountCardsAndPrepareMaps(Card[] cards, Int32[][] cardsBitMap, Int32[] counters, CardsMap cardsMap)
        {
            List<Card>[] suitedLists = new[]
            {
                new List<Card>(7),
                new List<Card>(7),
                new List<Card>(7),
                new List<Card>(7)
            };

            List<Card> result = null;

            for (Int32 q = 0; q < cards.Length; q++)
            {
                Card card = cards[q];

                Int32 index = (Int32)card.Symbol - 2;

                counters[index]++;

                Int32 suitIndex = (Int32)card.Suit - 1;

                cardsBitMap[suitIndex][index] = 1;

                suitedLists[suitIndex].Add(card);

                if (suitedLists[suitIndex].Count >= HandCardsCount)
                {
                    result = suitedLists[suitIndex];
                }

                cardsMap.Add(index, card);
            }

            if (null != result)
            {
                EnsureNotMoreThen(result, HandCardsCount);
            }

            return result;
        }
    }
}

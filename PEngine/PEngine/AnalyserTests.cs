using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PEngine
{
    [TestClass]
    public sealed class AnalyserTests
    {
        [TestMethod]
        public void TestPair1()
        {
            CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
                {
                    new Card
                    {
                        Suit = CardSuit.Clubs,
                        Symbol = CardSymbol.Jack
                    },
                    new Card
                    {
                        Suit = CardSuit.Diamond,
                        Symbol = CardSymbol.Jack
                    }
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Pair, result.Rank);
        }

        [TestMethod]
        public void TestPair2()
        {
            CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
                {
                    new Card
                    {
                        Suit = CardSuit.Clubs,
                        Symbol = CardSymbol.Jack
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.Ace
                    },
                    new Card
                    {
                        Suit = CardSuit.Clubs,
                        Symbol = CardSymbol.Three
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.King
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.Queen
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.Nine
                    },
                    new Card
                    {
                        Suit = CardSuit.Spades,
                        Symbol = CardSymbol.Jack
                    }
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Pair, result.Rank);
        }

        [TestMethod]
        public void TestStraight2()
        {
            CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
                {
                    new Card
                    {
                        Suit = CardSuit.Clubs,
                        Symbol = CardSymbol.Jack
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.Ace
                    },
                    new Card
                    {
                        Suit = CardSuit.Clubs,
                        Symbol = CardSymbol.Three
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.King
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.Queen
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.Ten
                    },
                    new Card
                    {
                        Suit = CardSuit.Spades,
                        Symbol = CardSymbol.Jack
                    }
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Straight, result.Rank);
        }

        [TestMethod]
        public void TestStraightVsThree()
        {
            CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
                {
                    new Card
                    {
                        Suit = CardSuit.Clubs,
                        Symbol = CardSymbol.Jack
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.Ace
                    },
                    new Card
                    {
                        Suit = CardSuit.Diamond,
                        Symbol = CardSymbol.Jack
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.King
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.Queen
                    },
                    new Card
                    {
                        Suit = CardSuit.Heart,
                        Symbol = CardSymbol.Ten
                    },
                    new Card
                    {
                        Suit = CardSuit.Spades,
                        Symbol = CardSymbol.Jack
                    }
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Straight, result.Rank);
        }
    }
}

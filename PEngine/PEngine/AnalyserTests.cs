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
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card (CardSymbol.Jack, CardSuit.Diamond)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Pair, result.Rank);
        }

        [TestMethod]
        public void TestPair2()
        {
            CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Ace, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.King, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Nine, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Spades)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Pair, result.Rank);
        }

        [TestMethod]
        public void TestTwoPairs1()
        {
            CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Ace, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.Three, CardSuit.Heart),
                    new Card(CardSymbol.Nine, CardSuit.Clubs),
                    new Card(CardSymbol.Nine, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Spades)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.TwoPairs, result.Rank);
        }

        [TestMethod]
        public void TestStraight2()
        {
            CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Ace, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.King, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Ten, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Spades)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Straight, result.Rank);
        }

        [TestMethod]
        public void TestStraightVsThree()
        {
            CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Ace, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Diamond),
                    new Card(CardSymbol.King, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Ten, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Spades)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Straight, result.Rank);
        }

        //[TestMethod]
        //public void TestFlushVsTwoPairs()
        //{
        //    CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
        //        {
        //            new Card
        //            {
        //                Suit = CardSuit.Clubs,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Clubs,
        //                Symbol = CardSymbol.Three
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Three
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Queen
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Ten
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Jack
        //            }
        //        });

        //    Assert.AreEqual(CardsAnalyser.HandRank.Flush, result.Rank);
        //}

        //[TestMethod]
        //public void TestFlushVsThree()
        //{
        //    CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
        //        {
        //            new Card
        //            {
        //                Suit = CardSuit.Clubs,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Diamond,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Three
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Queen
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Ten
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Jack
        //            }
        //        });

        //    Assert.AreEqual(CardsAnalyser.HandRank.Flush, result.Rank);
        //}

        //[TestMethod]
        //public void TestFourVsFull()
        //{
        //    CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
        //        {
        //            new Card
        //            {
        //                Suit = CardSuit.Clubs,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Diamond,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Spades,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Queen
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Diamond,
        //                Symbol = CardSymbol.Queen
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Spades,
        //                Symbol = CardSymbol.Queen
        //            }
        //        });

        //    Assert.AreEqual(CardsAnalyser.HandRank.FourOfKind, result.Rank);
        //}

        //[TestMethod]
        //public void TestFull()
        //{
        //    CardsAnalyser.Result result = CardsAnalyser.Analyse(new[]
        //        {
        //            new Card
        //            {
        //                Suit = CardSuit.Clubs,
        //                Symbol = CardSymbol.Ace
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Diamond,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Spades,
        //                Symbol = CardSymbol.Two
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Heart,
        //                Symbol = CardSymbol.Queen
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Diamond,
        //                Symbol = CardSymbol.Queen
        //            },
        //            new Card
        //            {
        //                Suit = CardSuit.Spades,
        //                Symbol = CardSymbol.Queen
        //            }
        //        });

        //    Assert.AreEqual(CardsAnalyser.HandRank.FullHouse, result.Rank);
        //}
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PEngine
{
    [TestClass]
    public sealed class AnalyserTests
    {
        [TestMethod]
        public void ComparePairs()
        {
            CardsAnalyser.Result resultA = CardsAnalyser.AnalyseCards(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Eight, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.King, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Nine, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Spades)
                });

            CardsAnalyser.Result resultB = CardsAnalyser.AnalyseCards(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Eight, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.King, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Nine, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Spades)
                });

            Int32 comparisonResult = resultA.CompareTo(resultB);

            Assert.AreEqual(-1, comparisonResult);
        }

        [TestMethod]
        public void CompareSamePairs()
        {
            CommonMatchTest(new[]
               {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Eight, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.King, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Six, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Spades)
                },
                CardsAnalyser.HandRank.Pair,
                2,
                3,
                new[]
                 {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Eight, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.King, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Nine, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Spades)
                },
                CardsAnalyser.HandRank.Pair,
                2,
                3,
                -1);
        }

        [TestMethod]
        public void HighCard()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Eight, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.Five, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Nine, CardSuit.Heart),
                    new Card(CardSymbol.Two, CardSuit.Spades)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.HighCard, result.Rank);

            CheckResult(result, 0, 5);
        }

        private static void CommonMatchTest(
            Card[] cardsA,
            CardsAnalyser.HandRank expectedRankA,
            Int32 expectedMainA,
            Int32 expectedResidualA,
            Card[] cardsB,
            CardsAnalyser.HandRank expectedRankB,
            Int32 expectedMainB,
            Int32 expectedResidualB,
            Int32 expectedComparisonResult)
        {
            CardsAnalyser.Result resultA = CardsAnalyser.AnalyseCards(cardsA);

            CardsAnalyser.Result resultB = CardsAnalyser.AnalyseCards(cardsB);

            Assert.AreEqual(expectedRankA, resultA.Rank);

            Assert.AreEqual(expectedRankB, resultB.Rank);

            Int32 comparisonResult = resultA.CompareTo(resultB);

            Assert.AreEqual(expectedComparisonResult, comparisonResult);

            CheckResult(resultA, expectedMainA, expectedResidualA);

            CheckResult(resultB, expectedMainB, expectedResidualB);
        }

        [TestMethod]
        public void CompareHighCards()
        {
            CommonMatchTest(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Eight, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.Five, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Nine, CardSuit.Heart),
                    new Card(CardSymbol.Two, CardSuit.Spades)
                },
                CardsAnalyser.HandRank.HighCard,
                0,
                5,
                new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Eight, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.Four, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Nine, CardSuit.Heart),
                    new Card(CardSymbol.Two, CardSuit.Spades)
                },
                CardsAnalyser.HandRank.HighCard,
                0,
                5,
                1);
        }

        [TestMethod]
        public void TestPair1()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card (CardSymbol.Jack, CardSuit.Diamond)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Pair, result.Rank);
        }

        [TestMethod]
        public void TestPair2()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
                {
                    new Card(CardSymbol.Jack, CardSuit.Clubs),
                    new Card(CardSymbol.Eight, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.King, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Nine, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Spades)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Pair, result.Rank);

            CheckResult(result, 2, 3);

            Assert.AreEqual(Card.CreateInstance(CardSymbol.King, CardSuit.Heart), result.ResidualCards[0]);

            Assert.AreEqual(Card.CreateInstance(CardSymbol.Queen, CardSuit.Heart), result.ResidualCards[1]);

            Assert.AreEqual(Card.CreateInstance(CardSymbol.Nine, CardSuit.Heart), result.ResidualCards[2]);
        }

        private static void CheckResult(CardsAnalyser.Result result, Int32 expectedMain, Int32 expectedResidual)
        {
            Assert.IsNotNull(result.MainHandCards);

            Assert.IsNotNull(result.ResidualCards);

            Assert.AreEqual(CardsAnalyser.HandCardsCount, result.MainHandCards.Length + result.ResidualCards.Length);

            Assert.AreEqual(expectedMain, result.MainHandCards.Length);

            Assert.AreEqual(expectedResidual, result.ResidualCards.Length);
        }

        [TestMethod]
        public void TestTwoPairs1()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
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

            CheckResult(result, 4, 1);

            Assert.AreEqual(Card.CreateInstance(CardSymbol.Ace, CardSuit.Heart), result.ResidualCards[0]);
        }

        [TestMethod]
        public void TestStraight2()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
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

            CheckResult(result, 5, 0);
        }

        [TestMethod]
        public void TestStraightVsThree()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
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

            CheckResult(result, 5, 0);
        }

        [TestMethod]
        public void TestFlushVsTwoPairs()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
                {
                    new Card(CardSymbol.Two, CardSuit.Clubs),
                    new Card(CardSymbol.Two, CardSuit.Heart),
                    new Card(CardSymbol.Three, CardSuit.Clubs),
                    new Card(CardSymbol.Three, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Ten, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Heart)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Flush, result.Rank);

            CheckResult(result, 5, 0);
        }

        [TestMethod]
        public void TestFlushVsThree()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
                {
                    new Card(CardSymbol.Two, CardSuit.Clubs),
                    new Card(CardSymbol.Two, CardSuit.Heart),
                    new Card(CardSymbol.Two, CardSuit.Diamond),
                    new Card(CardSymbol.Three, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Ten, CardSuit.Heart),
                    new Card(CardSymbol.Jack, CardSuit.Heart)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.Flush, result.Rank);

            CheckResult(result, 5, 0);
        }

        [TestMethod]
        public void TestFourVsFull()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
                {
                    new Card(CardSymbol.Two, CardSuit.Clubs),
                    new Card(CardSymbol.Two, CardSuit.Heart),
                    new Card(CardSymbol.Two, CardSuit.Diamond),
                    new Card(CardSymbol.Two, CardSuit.Spades),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Diamond),
                    new Card(CardSymbol.Queen, CardSuit.Spades)
                });

            Assert.AreEqual(CardsAnalyser.HandRank.FourOfKind, result.Rank);

            CheckResult(result, 4, 1);
        }

        [TestMethod]
        public void TestFull()
        {
            CardsAnalyser.Result result = CardsAnalyser.AnalyseCards(new[]
                {
                    new Card(CardSymbol.Ace, CardSuit.Clubs),
                    new Card(CardSymbol.Two, CardSuit.Heart),
                    new Card(CardSymbol.Two, CardSuit.Diamond),
                    new Card(CardSymbol.Two, CardSuit.Spades),
                    new Card(CardSymbol.Queen, CardSuit.Heart),
                    new Card(CardSymbol.Queen, CardSuit.Diamond),
                    new Card(CardSymbol.Queen, CardSuit.Spades)

                });

            Assert.AreEqual(CardsAnalyser.HandRank.FullHouse, result.Rank);

            CheckResult(result, 5, 0);
        }
    }
}

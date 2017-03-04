using System;

namespace PEngine
{
    public enum CardSymbol
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14,
    }

    public enum CardSuit
    {
        Spades = 1,
        Clubs = 2,
        Diamond = 3,
        Heart = 4
    }

    public struct Card
    {
        public CardSymbol Symbol { get; set; }

        public CardSuit Suit { get; set; }

        private static String SymbolToString(CardSymbol symbol)
        {
            Int32 value = (Int32)symbol;

            if (value >= 2 && value <= 10)
            {
                return value.ToString();
            }

            switch (symbol)
            {
                case CardSymbol.Ace:
                    return "A";

                case CardSymbol.Queen:
                    return "Q";

                case CardSymbol.King:
                    return "K";

                case CardSymbol.Jack:
                    return "J";

                default:
                    throw new NotSupportedException(symbol.ToString());
            }
        }

        public override String ToString()
        {
            return String.Format("{0}-{1}", SymbolToString(Symbol), Suit);
        }
    }
}

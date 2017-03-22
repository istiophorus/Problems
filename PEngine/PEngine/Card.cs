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

    public struct Card : IComparable<Card>
    {
        public static Card CreateInstance(CardSymbol symbol, CardSuit suit)
        {
            return new Card(symbol, suit);
        }

        public Card(CardSymbol symbol, CardSuit suit)
        {
            _symbol = symbol;

            _suit = suit;

            _toString = String.Format("{0}-{1}", SymbolToString(_symbol), _suit);

            _hashCode = typeof(Card).GetHashCode() * 1000 + (Int32)_symbol * 10 + (Int32)_suit;
        }

        private readonly String _toString;

        private readonly CardSymbol _symbol;

        private readonly CardSuit _suit;

        private readonly Int32 _hashCode;

        public CardSymbol Symbol
        {
            get
            {
                return _symbol;
            }
        }

        public CardSuit Suit
        {
            get
            {
                return _suit;
            }
        }

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
            return _toString;
        }

        public override Boolean Equals(Object obj)
        {
            if (null == obj)
            {
                return false;
            }

            if (obj is Card)
            {
                Card other = (Card)obj;

                return other._symbol == _symbol && other._suit == _suit;
            }
            else
            {
                return false;
            }
        }

        public override Int32 GetHashCode()
        {
            return _hashCode;
        }

        public Int32 CompareTo(Card other)
        {
            return Symbol.CompareTo(other.Symbol);
        }
    }
}

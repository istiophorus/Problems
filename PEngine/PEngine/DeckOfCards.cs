using System;
using System.Collections.Generic;
using System.Linq;

namespace PEngine
{
    public sealed class DeckOfCards
    {
        private readonly List<Card> _cards;

        public DeckOfCards()
        {
            _cards = InitializeCardsDeck();
        }

        private static List<Card> InitializeCardsDeck()
        {
            String[] suits = Enum.GetNames(typeof(CardSuit));

            String[] symbols = Enum.GetNames(typeof(CardSymbol));

            var r = from s in suits
                    from v in symbols
                    select new Card((CardSymbol)Enum.Parse(typeof(CardSymbol), v), (CardSuit)Enum.Parse(typeof(CardSuit), s));

            return r.ToList();
        }

        public void Shuffle(IRandom random, UInt16 moves = 16000)
        {
            if (null == random)
            {
                throw new ArgumentNullException("random");
            }

            for (Int32 q = 0; q < moves; q++)
            {
                Int32 index1 = random.Next(_cards.Count);

                Int32 index2 = random.Next(_cards.Count);

                Card temp = _cards[index1];

                _cards[index1] = _cards[index2];

                _cards[index2] = temp;
            }
        }

        public Int32 Count
        {
            get
            {
                return _cards.Count;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return _cards.Count <= 0;
            }
        }

        public Card[] Draw(Int32 count)
        {
            if (count < 0 || count > _cards.Count)
            {
                throw new InvalidOperationException();
            }

            List<Card> cards = new List<Card>();

            while (count > 0)
            {
                cards.Add(Draw());

                count--;
            }

            return cards.ToArray();
        }

        public Card Draw()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException();
            }

            Card result = _cards[0];

            _cards.RemoveAt(0);

            return result;
        }
    }
}

using System;
using System.Collections.Generic;

namespace PEngine
{
    public sealed class CardsMap
    {
        private readonly Dictionary<Int32, List<Card>> cardsContainer = new Dictionary<Int32, List<Card>>();

        public void Add(Int32 index, Card card)
        {
            if (index < 0 || index > 13)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            List<Card> list;

            if (!cardsContainer.TryGetValue(index, out list))
            {
                list = new List<Card>();

                cardsContainer.Add(index, list);
            }

            list.Add(card);
        }

        private static readonly Card[] Empty = new Card[0];

        public Card[] Get(Int32 index, Int32 count = Int32.MaxValue)
        {
            if (index < 0 || index > 13)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            List<Card> list;

            if (cardsContainer.TryGetValue(index, out list))
            {
                if (count >= list.Count)
                {
                    return list.ToArray();
                }
                else
                {
                    Card[] result = new Card[count];

                    for (Int32 q = 0; q < result.Length; q++)
                    {
                        result[q] = list[q];
                    }

                    return result;
                }
            }

            return Empty;
        }
    }
}

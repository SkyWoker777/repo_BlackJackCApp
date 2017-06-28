using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            OnCreate();
        }
       
        public void OnCreate()
        {
            cards = new List<Card>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    cards.Add(new Card() { 
                        Name = (Name)j,
                        Suit = (Suit)i
                    });

                    if (j <= 7)
                        cards[cards.Count - 1].Dignity = j + 2;
                    else if(j == 12)
                    {
                        cards[cards.Count - 1].Dignity = 11;
                    }
                    else
                        cards[cards.Count - 1].Dignity = 10;
                }
            }
            Shuffle();
        }

        public Card GetCard()
        {
            Card card = cards.First();
            cards.Remove(card);
            return card;
        }

        public int CountCardsInDeck()
        {
            return cards.Count;
        }

        private void Shuffle()
        {
            Random rnd = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Card card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }
        }


    }
}

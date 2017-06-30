using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class Deck
    {
        private List<Card> _cards;

        public Deck()
        {
            CreateDeck();
        }
       
        public void CreateDeck()
        {
            _cards = new List<Card>();
            var numOfSuit = Enum.GetNames(typeof(Suit)).Length;
            var cardsSameSuit = Enum.GetNames(typeof(Name)).Length;
            int dignityAce = 11;
            int dignityPicture = 10;

            for (int i = 0; i < numOfSuit; i++)
            {
                for (int j = 0; j < cardsSameSuit; j++)
                {
                    _cards.Add(new Card() { 
                        Name = (Name)j,
                        Suit = (Suit)i
                    });

                    if (j <= 7)
                    {
                        _cards[_cards.Count - 1].Dignity = j + 2;
                    }
                    if (j == 12)
                    {

                        _cards[_cards.Count - 1].Dignity = dignityAce;
                    }
                    if (j >= 8 && j != 12)
                    {
                        _cards[_cards.Count - 1].Dignity = dignityPicture;
                    }
                }
            }
            Shuffle();
        }

        public Card GetCard()
        {
            Card card = _cards.First();
            _cards.Remove(card);
            return card;
        }

        public int CountCardsInDeck()
        {
            return _cards.Count;
        }

        private void Shuffle()
        {
            Random rnd = new Random();
            int n = _cards.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Card card = _cards[k];
                _cards[k] = _cards[n];
                _cards[n] = card;
            }
        }

    }
}

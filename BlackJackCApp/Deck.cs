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

            for (int i = 0; i < Enum.GetNames(typeof(Suit)).Length; i++)
            {
                for (int j = 0, value = 2; j < Enum.GetNames(typeof(Name)).Length; j++)
                {
                    var card = new Card();
                    card.Name = (Name)j;
                    card.Suit = (Suit)i;
                    _cards.Add(card);

                    if (j < (int)Name.Ten)
                    {
                        _cards.Last().Dignity = value++;
                    }
                    if (j == (int)Name.Ace)
                    {
                        _cards.Last().Dignity = ++value;
                    }
                    if (j >= (int)Name.Ten && j != (int)Name.Ace)
                    {
                        _cards.Last().Dignity = value;
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

        public void DisplayDeck()
        {
            if (_cards.Count == 0)
            {
                CreateDeck();
            }
            foreach (var c in _cards)
            {
                Console.WriteLine($"{c.Name} of {c.Suit} : {c.Dignity}");
            }
        }

        public int Count
        {
            get { return _cards.Count; }
        }

        private void Shuffle()
        {
            Random rnd = new Random();
            int n = _cards.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n+1);
                Card card = _cards[k];
                _cards[k] = _cards[n];
                _cards[n] = card;
            }
        }

    }
}

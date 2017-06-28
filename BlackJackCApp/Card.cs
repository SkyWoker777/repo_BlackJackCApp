using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public enum Suit
    {
        Heart,
        Bubba,
        Cross,
        Lance
    }

    public enum Name
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class Card
    {
        public Name Name { get; set; }
        public int Dignity { get; set; }
        public Suit Suit { get; set; }
    }
}

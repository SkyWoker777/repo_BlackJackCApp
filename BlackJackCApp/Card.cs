using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public enum Suit
    {
        Heart = 0,
        Bubba = 1,
        Cross = 2,
        Lance = 3
    }
    public enum Name
    {
        Two = 0,
        Three = 1,
        Four = 2,
        Five = 3,
        Six = 4,
        Seven = 5,
        Eight = 6,
        Nine = 7,
        Ten = 8,
        Jack = 9,
        Queen = 10,
        King = 11,
        Ace = 12
    }

    public class Card
    {
        public Name Name { get; set; }
        public int Dignity { get; set; }
        public Suit Suit { get; set; }
    }
}

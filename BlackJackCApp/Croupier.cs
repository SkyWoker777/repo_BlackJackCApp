using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class Croupier
    {
        public List<Card> Hand { get; set; }

        public Croupier()
        {
            Hand = new List<Card>();
        }
    }
}

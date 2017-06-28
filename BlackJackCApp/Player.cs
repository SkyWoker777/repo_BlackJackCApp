using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }

        public Player()
        {
            Hand = new List<Card>();
        }
        //add-on card
        public void HitMe(Card card)
        {
            Hand.Add(card);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class User
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }

        public User()
        {
            Hand = new List<Card>();
        }
    }
}

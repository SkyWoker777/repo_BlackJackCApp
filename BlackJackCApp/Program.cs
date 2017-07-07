using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameProcess();
            game.NewGame();
            game.Play();
        }
    }
}

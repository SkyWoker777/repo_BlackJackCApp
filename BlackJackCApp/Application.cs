using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class Application
    {
        public void Run()
        {
            var game = new GameViewModel();
            game.NewGame();
            game.Play();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class Application
    {
        public event ContentEventHandler Launched;

        public void Run()
        {
            Launched("Welcome to BlackJack!\n");

            Game game = new Game();
            game.MessageShowed += ContentMessage.Show_Message;
            game.NewGame();
        }
    }
}

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
            Console.WriteLine("Welcome to BlackJack!\n");

            Game game = new Game();
            game.NewGame();
            Console.WriteLine("----------------Let's Play! GL HF---------------\n");
            Console.WriteLine("Croupier gives cards...");
            System.Threading.Thread.Sleep(2000);
            game.OnPlay();
            Console.WriteLine("\nGood Bay! See you next time...");
            Console.ReadLine();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJackCApp
{
    public class Game
    {
        Deck deck;
        Player player;
        Croupier croupier;

        public Game(){ }

        public void NewGame()
        {
            Console.Write("Enter your Name: ");
            player = new Player()
            {
                Name = Console.ReadLine()
            };
            croupier = new Croupier();
            deck = new Deck();
            Console.Clear();
            Console.WriteLine($"The game is created. Hi! {player.Name}\n");
        }

        public void OnPlay()
        {
            if(deck.CountCardsInDeck() < 25)
            {
                deck.OnCreate();
            }

            int num = 0;
            bool insurance = false;
            int playerTotal = 0;
            int croupierTotal = 0;
            bool play = true;

            player.Hand.Add(deck.GetCard());
            croupier.Hand.Add(deck.GetCard());
            player.Hand.Add(deck.GetCard());
            croupier.Hand.Add(deck.GetCard());

            Console.WriteLine($"{player.Name}'s hand:");
            foreach (var c in player.Hand)
            {
                Console.WriteLine($"Card {++num}: {c.Name} of {c.Suit}");
            }
            Console.WriteLine("Total: {0}\n", CalcPlayerHand());

            Console.WriteLine("Croupier's hand:");
            Console.WriteLine("Card 1: {0} of {1}", croupier.Hand[0].Name, croupier.Hand[0].Suit);
            Console.WriteLine("Card 2: [Hidden Card]");
            Console.WriteLine("Total: {0}\n", croupier.Hand[0].Dignity);

            //Check Ace in Croupier's hand. Insurance stage!
            if (croupier.Hand[0].Name == Name.Ace)
            {
                Console.WriteLine("Insurance? ([Y]es / [N]o)");
                ConsoleKeyInfo command = Console.ReadKey(true);
                while (command.Key != ConsoleKey.Y && command.Key != ConsoleKey.N)
                {
                    Console.WriteLine("Croupier has an Ace. Insurance from Croupier's Black Jack? [Y] / [N]");
                    command = Console.ReadKey(true);
                }
                if (command.Key == ConsoleKey.Y)
                {
                    insurance = true;
                    Console.WriteLine("\nInsurance Accepted!\n");
                }
                else
                {
                    insurance = false;
                    Console.WriteLine("\nInsurance Rejected.\n");
                }
            }

            if(croupier.Hand[0].Name == Name.Ace || croupier.Hand[0].Dignity == 10)
            {
                Console.WriteLine("Croupier is checking Hand for the BlackJack...\n");
                System.Threading.Thread.Sleep(2000);
                int total = croupier.Hand[0].Dignity + croupier.Hand[1].Dignity;

                if (total == 21)
                {
                    Console.WriteLine("Croupier's hand:");
                    Console.WriteLine("Card 1: {0} of {1}", croupier.Hand[0].Name, croupier.Hand[0].Suit);
                    Console.WriteLine("Card 2: {0} of {1}", croupier.Hand[1].Name, croupier.Hand[1].Suit);
                    Console.WriteLine("Total: {0} - BlackJack!\n", total);

                    if (player.Hand[0].Dignity + player.Hand[1].Dignity == 21 && insurance)
                    {
                        Console.WriteLine($"{player.Name}, You have - BlackJack! Prize pool: 1 to 1");
                    }
                    else if (player.Hand[0].Dignity + player.Hand[1].Dignity != 21 && !insurance)
                    {
                        Console.WriteLine($"{player.Name} Lost! The casino takes your bet. \n");
                    }
                    Continue();
                }
            }

            if (CalcPlayerHand() == 21)
            {
                Console.WriteLine("BlackJack, You Won! Prize pool: 3 to 2");
                Console.WriteLine($"Croupier has: {CalcCroupierHand()} points");
                Continue();
                play = false;
            }

            // [Hit] the card if player says "Hit Me"
            while(play)
            {
                Console.WriteLine("Do you want add-on card or enough? : [E]nough or [H]it\n");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                while (keyInfo.Key != ConsoleKey.H && keyInfo.Key != ConsoleKey.E)
                {
                    Console.WriteLine("Illegal key. Need to tap [S] or [H]\n");
                    keyInfo = Console.ReadKey(true);
                }

                switch (keyInfo.Key)
                {
                    case ConsoleKey.H:
                        Console.WriteLine("-----[Hit]-----");
                        player.HitMe(deck.GetCard());
                        Console.WriteLine($"{player.Name}'s hand:");
                        Console.WriteLine("Hitted card: {0} of {1}", player.Hand.Last().Name, player.Hand.Last().Suit);

                        if (player.Hand.Last().Name == Name.Ace)
                        {
                            playerTotal = CalcPlayerHand();
                            if (playerTotal > 21)
                            {
                                // Now an Ace = 1
                                playerTotal -= 10;
                            }
                        }
                        else
                        {
                            playerTotal = CalcPlayerHand();
                        }
                        Console.WriteLine("Total now: {0}\n", playerTotal);
                        if(playerTotal > 21)
                        {
                            Console.WriteLine("A lot of! You Lose. The casino takes your bet.");
                            play = false;
                        }
                        else if(playerTotal == 21)
                        {
                            Console.WriteLine("BlackJack! I assume you want to stand from now on...\n");
                            System.Threading.Thread.Sleep(1000);
                            continue;
                        }
                        else { continue; }
                        break;

                    case ConsoleKey.E:
                        Console.WriteLine("---[Enough]---, Croupier cheks his hand...");
                        Console.WriteLine("Croupier's hand:");
                        Console.WriteLine("Card 1: {0} of {1}", croupier.Hand[0].Name, croupier.Hand[0].Suit);
                        Console.WriteLine("Card 2: {0} of {1}", croupier.Hand[1].Name, croupier.Hand[1].Suit);

                        croupierTotal = CalcCroupierHand();

                        while (croupierTotal < 17)
                        {
                            croupier.Hand.Add(deck.GetCard());
                            Console.WriteLine("Hitted card: {0} of {1}", croupier.Hand.Last().Name, croupier.Hand.Last().Suit);
                            croupierTotal = CalcCroupierHand();
                        }
                        Console.WriteLine("Total now: {0}\n", croupierTotal);

                        if (croupierTotal > 21)
                        {
                            Console.WriteLine("Croupier bust:( You win! Prize pool = 3 to 2");
                            play = false;
                        }
                        else
                        {
                            playerTotal = CalcPlayerHand();
                            if (croupierTotal > playerTotal)
                            {
                                Console.WriteLine("Croupier wins! {0} vs {1}", croupierTotal, playerTotal);
                            }
                            else
                            {
                                Console.WriteLine($"{player.Name} wins! Croupier: {croupierTotal} vs You: {playerTotal}");
                            }
                            play = false;
                        }
                        break;
                }
            }
            Continue();
        }

        public void Continue()
        {
            Console.WriteLine("\nWant to play [A]gain or [Q]uit?");
            ConsoleKeyInfo command = Console.ReadKey(true);
            while (command.Key != ConsoleKey.A && command.Key != ConsoleKey.Q)
            {
                Console.WriteLine("Continue? [A] / [Q]");
                command = Console.ReadKey(true);
            }
            if (command.Key == ConsoleKey.A)
            {
                player.Hand.Clear();
                croupier.Hand.Clear();
                Console.WriteLine("\nPlease, wait! The croupier gives new cards...\n");
                System.Threading.Thread.Sleep(3000);
                OnPlay();
            }
            else
            {
                Console.WriteLine("\n---------------Game Over---------------\n");
            }
        }

        private int CalcPlayerHand()
        {
            int sum = 0;
            foreach(var card in player.Hand)
            {
                sum += card.Dignity;
            }
            return sum;
        }
        private int CalcCroupierHand()
        {
            int sum = 0;
            foreach (var card in croupier.Hand)
            {
                sum += card.Dignity;
            }
            return sum;
        }
    }
}

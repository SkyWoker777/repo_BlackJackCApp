using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJackCApp
{
    public class Game
    {
        private Deck _deck;
        private Player _player;
        private Croupier _croupier;

        public event ContentEventHandler MessageShowed;

        public Game(){ }

        public void NewGame()
        {
            MessageShowed?.Invoke("Enter your name:");
            _player = new Player()
            {
                Name = Console.ReadLine()
            };
            _croupier = new Croupier();
            _deck = new Deck();
            Console.Clear();
            MessageShowed?.Invoke($"The game is created. Hi! {_player.Name}\n");
            Play();
        }

        private void Play()
        {
            if(_deck.CountCardsInDeck() < 25)
            {
                _deck.CreateDeck();
            }

            _player.Hand.Add(_deck.GetCard());
            _croupier.Hand.Add(_deck.GetCard());
            _player.Hand.Add(_deck.GetCard());
            _croupier.Hand.Add(_deck.GetCard());

            int num = 0;
            MessageShowed?.Invoke($"{_player.Name}'s hand:");
            foreach (var c in _player.Hand)
            {
                MessageShowed?.Invoke($"Card {++num}: {c.Name} of {c.Suit}");
            }
            MessageShowed?.Invoke($"Total: {CalcPlayerHand()}\n");

            MessageShowed?.Invoke("Croupier's hand:");
            MessageShowed?.Invoke($"Card 1: {_croupier.Hand[0].Name} of {_croupier.Hand[0].Suit}");
            MessageShowed?.Invoke("Card 2: [Hidden Card]");
            MessageShowed?.Invoke($"Total: {_croupier.Hand[0].Dignity}\n");

            bool insurance = CanInsure();
            bool add_on = HaveBlackJack(insurance);

            AddonCard(add_on);
        }

        public void Continue()
        {
            MessageShowed?.Invoke("\nDo you want to continue or quit? [A]ccept / [Q]uit");
            ConsoleKeyInfo command = Console.ReadKey(true);
            while (command.Key != ConsoleKey.A && command.Key != ConsoleKey.Q)
            {
                MessageShowed?.Invoke("Continue? [A] / [Q]");
                command = Console.ReadKey(true);
            }
            if (command.Key == ConsoleKey.A)
            {
                _player.Hand.Clear();
                _croupier.Hand.Clear();
                MessageShowed?.Invoke("\nPlease, wait! The croupier gives new cards...\n");
                System.Threading.Thread.Sleep(3000);
                Play();
            }
            else { MessageShowed?.Invoke("\n---------------Game Over---------------\n"); }
        }

        private bool HaveBlackJack(bool insurance)
        {
            bool add_on = true;
            if (_croupier.Hand[0].Name == Name.Ace || _croupier.Hand[0].Dignity == 10)
            {
                MessageShowed?.Invoke("Croupier is checking Hand for the BlackJack...\n");
                System.Threading.Thread.Sleep(2000);
                int total = _croupier.Hand[0].Dignity + _croupier.Hand[1].Dignity;

                if (total == 21)
                {
                    MessageShowed?.Invoke("Croupier's hand:");
                    MessageShowed?.Invoke($"Card 1: {_croupier.Hand[0].Name} of {_croupier.Hand[0].Suit}");
                    MessageShowed?.Invoke($"Card 2: {_croupier.Hand[1].Name} of {_croupier.Hand[1].Suit}");
                    MessageShowed?.Invoke($"Total: {total} - BlackJack!\n");

                    if (_player.Hand[0].Dignity + _player.Hand[1].Dignity == 21 && insurance)
                    {
                        MessageShowed?.Invoke($"{_player.Name}, You have - BlackJack! Prize pool: 1 to 1");
                    }
                    if (_player.Hand[0].Dignity + _player.Hand[1].Dignity != 21 && !insurance)
                    {
                        MessageShowed?.Invoke($"{_player.Name} Lost! The casino takes your bet. \n");
                    }
                    Continue();
                }
            }
            if (CalcPlayerHand() == 21)
            {
                MessageShowed?.Invoke($"BlackJack, You Won! Croupier has: {CalcCroupierHand()} points. Prize pool: 3 to 2");
                add_on = false;
                Continue();
            }
            return add_on;
        }

        /// <summary>
        /// [Hit] the card if player says "Hit Me"
        /// </summary>
        /// <param name="add_on">true, if need add-on</param>
        private void AddonCard(bool add_on)
        {
            int playerTotal = 0;
            int croupierTotal = 0;
            while (add_on)
            {
                MessageShowed?.Invoke("Do you want add-on card or enough? : [E]nough or [H]it\n");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                while (keyInfo.Key != ConsoleKey.H && keyInfo.Key != ConsoleKey.E)
                {
                    MessageShowed?.Invoke("Illegal key. Need to tap [S] or [H]\n");
                    keyInfo = Console.ReadKey(true);
                }

                switch (keyInfo.Key)
                {
                    case ConsoleKey.H:
                        _player.HitMe(_deck.GetCard());

                        MessageShowed?.Invoke($"[---Hit---]\n{_player.Name}'s hand:");
                        MessageShowed?.Invoke($"Hitted card: {_player.Hand.Last().Name} of {_player.Hand.Last().Suit}");

                        if (_player.Hand.Last().Name == Name.Ace)
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
                        MessageShowed?.Invoke($"Total now: {playerTotal}\n");
                        if (playerTotal > 21)
                        {
                            MessageShowed?.Invoke("A lot of! You Lose. The casino takes your bet.");
                            add_on = false;
                        }
                        if(playerTotal == 21)
                        {
                            MessageShowed?.Invoke("BlackJack! I assume you want to stand from now on...\n");
                            System.Threading.Thread.Sleep(1000);
                            continue;
                        }
                        continue;

                    case ConsoleKey.E:
                        MessageShowed?.Invoke("[---Enough---]\nCroupier cheks his hand...");
                        MessageShowed?.Invoke($"Card 1: {_croupier.Hand[0].Name} of {_croupier.Hand[0].Suit}");
                        MessageShowed?.Invoke($"Card 2: {_croupier.Hand[1].Name} of {_croupier.Hand[1].Suit}");

                        croupierTotal = CalcCroupierHand();

                        while (croupierTotal < 17)
                        {
                            _croupier.Hand.Add(_deck.GetCard());
                            MessageShowed?.Invoke($"Hitted card: {_croupier.Hand.Last().Name} of {_croupier.Hand.Last().Suit}");
                            croupierTotal = CalcCroupierHand();
                        }
                        MessageShowed?.Invoke($"Total now: {croupierTotal}\n");

                        if (croupierTotal > 21)
                        {
                            MessageShowed?.Invoke("Croupier bust. You win! Prize pool = 3 to 2");
                            add_on = false;
                        }
                        else
                        {
                            playerTotal = CalcPlayerHand();
                            if (croupierTotal > playerTotal)
                            {
                                MessageShowed?.Invoke($"Croupier wins! {croupierTotal} vs {playerTotal}");
                            }
                            else
                            {
                                MessageShowed?.Invoke($"{_player.Name} wins! Croupier: {croupierTotal} vs You: {playerTotal}");
                            }
                            add_on = false;
                        }
                        break;
                }
            }
            Continue();
        }

        private bool CanInsure()
        {
            //Check Ace in Croupier's hand. Insurance stage!
            bool insurance = false;

            if (_croupier.Hand[0].Name == Name.Ace)
            {
                MessageShowed?.Invoke("Insurance? ([Y]es / [N]o)");
                ConsoleKeyInfo command = Console.ReadKey(true);
                while (command.Key != ConsoleKey.Y && command.Key != ConsoleKey.N)
                {
                    MessageShowed?.Invoke("Croupier has an Ace. Insurance from Croupier's Black Jack? [Y] / [N]");
                    command = Console.ReadKey(true);
                }
                if (command.Key == ConsoleKey.Y)
                {
                    insurance = true;
                    MessageShowed?.Invoke("\nInsurance Accepted!\n");
                }
                else { MessageShowed?.Invoke("\nInsurance Rejected.\n"); }
            }
            return insurance;
        }

        private int CalcPlayerHand()
        {
            int sum = 0;
            foreach(var card in _player.Hand)
            {
                sum += card.Dignity;
            }
            return sum;
        }
        private int CalcCroupierHand()
        {
            int sum = 0;
            foreach (var card in _croupier.Hand)
            {
                sum += card.Dignity;
            }
            return sum;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class GameViewModel
    {
        private Game _game;
        private string _userName;

        public void NewGame()
        {
            ShowMessage("Enter your name:");
            _userName = ContentControl.InputText();

            _game = new Game(_userName);
        }

        public void Play()
        {
            _game.Initialize();
            var user_hand = _game.GetHand(true);
            var croupier_hand = _game.GetHand(false);

            ShowMessage($"{_userName}'s hand:");
            ShowHand(user_hand, false);
            ShowMessage("\nCroupie's hand:");
            ShowHand(croupier_hand, true);

            bool insurance = CanInsure();
            bool add_on = true;
            if (croupier_hand[0].Name == Name.Ace || croupier_hand[0].Dignity == 10)
            {
                ShowMessage("The croupier is checking Hand for the BlackJack...");
                System.Threading.Thread.Sleep(2000);
                bool have_croupier = _game.HaveBlackJack(croupier_hand);

                if (have_croupier)
                {
                    ShowMessage("\nCroupie's hand:");
                    ShowHand(croupier_hand, false);

                    bool have_user = _game.HaveBlackJack(user_hand);
                    if (have_user && insurance || !have_user && insurance)
                    {
                        ShowMessage($"{_userName}, You have - BlackJack! Prize pool: 1 to 1\n");
                    }
                    if (!have_user && !insurance)
                    {
                        ShowMessage($"{_userName} Lost! The casino takes your bet.\n");
                    }
                    Continue();
                }
            }
            if (_game.HaveBlackJack(user_hand))
            {
                ShowMessage($"BlackJack, You Won! Croupier has: {_game.CalcHand(croupier_hand)} points. Prize pool: 3 to 2");
                add_on = false;
                Continue();
            }

            AddonCard(add_on, user_hand, croupier_hand);
        }

        private void Continue()
        {
            ShowMessage("\nDo you want to continue or quit? [A]ccept / [Q]uit");
            var command = ContentControl.InputKey();
            while (command.Key != ConsoleKey.A && command.Key != ConsoleKey.Q)
            {
                ShowMessage("Continue? [A] / [Q]");
                command = ContentControl.InputKey();
            }
            if (command.Key == ConsoleKey.A)
            {
                ShowMessage("\nPlease, wait! The croupier is giving new cards...\n");
                System.Threading.Thread.Sleep(2000);
                Play();
            }
        }

        private bool CanInsure()
        {
            bool insurance = false;

            if (_game.HaveAnAce())
            {
                ShowMessage("Insurance? ([Y]es / [N]o)");
                var command = ContentControl.InputKey();
                while (command.Key != ConsoleKey.Y && command.Key != ConsoleKey.N)
                {
                    ShowMessage("Croupier has an Ace. Insurance from Croupier's Black Jack? [Y] / [N]");
                    command = ContentControl.InputKey();
                }
                if (command.Key == ConsoleKey.Y)
                {
                    insurance = true;
                    ShowMessage("\nInsurance Accepted!\n");
                }
                else { ShowMessage("\nInsurance Rejected.\n"); }
            }
            return insurance;
        }

        private void AddonCard(bool add_on, List<Card> userHand, List<Card> croupierHand)
        {
            int userTotal = 0;
            int croupierTotal = 0;
            while (add_on)
            {
                ShowMessage("Do you want add-on card or enough? : [E]nough or [H]it\n");
                var keyInfo = ContentControl.InputKey();
                while (keyInfo.Key != ConsoleKey.H && keyInfo.Key != ConsoleKey.E)
                {
                    ShowMessage("Illegal key. Need to press [E] or [H]");
                    keyInfo = ContentControl.InputKey();
                }

                switch (keyInfo.Key)
                {
                    case ConsoleKey.H:
                        _game.TakeACard(userHand);
                        ShowMessage($"[---Hit---]\n{_userName}'s hand:");
                        ShowMessage($"Hitted card: {userHand.Last().Name} of {userHand.Last().Suit}");
                        userTotal = _game.CalcHand(userHand);
                        ShowMessage($"Total now: {userTotal}\n");
                        if (userTotal > Game.BLACKJACK)
                        {
                            ShowMessage("A lot of! You Lose. The casino takes your bet.");
                            add_on = false;
                        }
                        if (userTotal == Game.BLACKJACK)
                        {
                            ShowMessage("BlackJack! I assume you want to stand from now on...\n");
                            System.Threading.Thread.Sleep(1000);
                            continue;
                        }
                        continue;

                    case ConsoleKey.E:
                        ShowMessage("[---Enough---]\nCroupier cheks his hand...");
                        ShowHand(croupierHand, false);

                        croupierTotal = _game.CalcHand(croupierHand);
                        while (croupierTotal < Game.MINVALUE)
                        {
                            _game.TakeACard(croupierHand);
                            ShowMessage($"Hitted card: {croupierHand.Last().Name} of {croupierHand.Last().Suit}");
                            croupierTotal += croupierHand.Last().Dignity;
                        }
                        ShowMessage($"Total now: {croupierTotal}\n");

                        if (croupierTotal > Game.BLACKJACK)
                        {
                            ShowMessage("Croupier bust. You win! Prize pool = 3 to 2");
                            add_on = false;
                        }
                        else
                        {
                            userTotal = _game.CalcHand(userHand);
                            string message = (croupierTotal > userTotal) ?
                                $"Croupier wins! {croupierTotal} vs {userTotal}" :
                                $"{_userName} wins! Croupier: {croupierTotal} vs You: {userTotal}";
                            ShowMessage(message);
                            add_on = false;
                        }
                        break;
                }
            }
            Continue();
        }

        private void ShowHand(List<Card> hand, bool hidden)
        {
            if (!hidden)
            {
                int num = 0;
                int total = _game.CalcHand(hand);
                foreach (var card in hand)
                {
                    ShowMessage($"Card {++num}: {card.Name} of {card.Suit}");
                }
                ShowMessage($"Total: {total}\n");
            }
            else
            {
                //early game of the croupier
                ShowMessage($"Card 1: {hand[0].Name} of {hand[0].Suit}");
                ShowMessage("Card 2: [Hidden Card]");
                ShowMessage($"Total: {hand[0].Dignity}\n");
            }
        }

        private void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class ContentControl
    {
        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static string SetName()
        {
            ShowMessage("Enter your name:");
            return Console.ReadLine();
        }

        public static void DeadHeat_Message()
        {
            ShowMessage("Dead heat! Prize pool: 1 to 1\n");
        }
        public static void ReportLoss(string userName, int userTotal, int croupierTotal)
        {
            ShowMessage($"{userName} Lost! Croupier: {croupierTotal} vs  You: {userTotal}.\n");
        }
        public static void ReportVictory()
        {
            ShowMessage($"You Won! Prize pool: 3 to 2");
        }
        public static void ReportALotOf()
        {
            ShowMessage("A lot of! You Lose. The casino takes your bet."); 
        }
        public static void Report_CroupierHasALot()
        {
            ShowMessage("Croupier bust. You win! Prize pool = 3 to 2");
        }

        public static void ShowView_Cheking()
        {
            ShowMessage("The croupier is checking Hand for the BlackJack...");
        }
        public static void ShowView_Offering()
        {
            ShowMessage("BlackJack! I assume you want to stand from now on...\n");
        }

        public static void ShowHittedCard(string playerName, List<Card> hand, int total)
        {
            ShowMessage($"[---Hit---]\n{playerName}'s hand:");
            ShowMessage($"Hitted card: {hand.Last().Name} of {hand.Last().Suit}");
            ShowMessage($"Total now: {total}\n");
        }

        public static void ShowHand(string playerName, List<Card> hand, int total , bool hidden = false)
        {
            if (!hidden)
            {
                int num = 0;
                ShowMessage($"\n{playerName}'s hand:");
                foreach (var card in hand)
                {
                    ShowMessage($"Card {++num}: {card.Name} of {card.Suit}");
                }
                ShowMessage($"Total: {total}\n");
            }
            else
            {
                ShowMessage($"{playerName}'s hand:");
                ShowMessage($"Card 1: {hand[0].Name} of {hand[0].Suit}");
                ShowMessage("Card 2: [Hidden Card]");
                ShowMessage($"Total: {hand[0].Dignity}\n");
            }
        }

        public static bool WantToContinue()
        {
            ShowMessage("\nDo you want to continue or quit? [A]ccept / [Q]uit");
            var command = Console.ReadKey(true);
            while (command.Key != ConsoleKey.A && command.Key != ConsoleKey.Q)
            {
                ShowMessage("Illegal key! Need to press: [A] / [Q]");
                command = Console.ReadKey(true);
            }
            if (command.Key == ConsoleKey.A)
            {
                ShowMessage("\nPlease, wait! The croupier is giving new cards...\n");
                return true;
            }
            return false;
        }
        public static bool WantToInsurance()
        {
            ShowMessage("Insurance? ([Y]es / [N]o)");
            var command = Console.ReadKey(true);
            while (command.Key != ConsoleKey.Y && command.Key != ConsoleKey.N)
            {
                ShowMessage("Croupier has an Ace. Insurance from Croupier's Black Jack? [Y] / [N]");
                command = Console.ReadKey(true);
            }
            if (command.Key == ConsoleKey.Y)
            {
                ShowMessage("\nInsurance Accepted!\n");
                return true;
            }
            ShowMessage("\nInsurance Rejected.\n");
            return false;
        }
        public static bool SuggestTakeACard()
        {
            ShowMessage("Do you want add-on card or enough? : [E]nough or [H]it\n");
            var command = Console.ReadKey(true);
            while (command.Key != ConsoleKey.H && command.Key != ConsoleKey.E)
            {
                ShowMessage("Illegal key. Need to press [E] or [H]");
                command = Console.ReadKey(true);
            }
            if (command.Key != ConsoleKey.H)
            {
                ShowMessage("[---Enough---]\nCroupier cheks his hand...");
                return false;
            }
            return true;
        }
    }
}

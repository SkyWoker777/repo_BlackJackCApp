using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class GameProcess
    {
        private Game _game;
        private string _userName;
        private string _croupierName = "Dealer";

        public void NewGame()
        {
            _userName = ContentControl.SetName();
            _game = new Game(_userName);
        }

        public void Play()
        {
            _game.Initialize();
            var user_hand = _game.GetUserHand();
            int userTotal = _game.CalcHand(user_hand);
            ContentControl.ShowHand(_userName, user_hand, userTotal);
            System.Threading.Thread.Sleep(1000);

            var croupier_hand = _game.GetCroupierHand();
            int croupierTotal = _game.CalcHand(croupier_hand);
            ContentControl.ShowHand(_croupierName ,croupier_hand, croupierTotal, true);

            bool insurance = CanInsure();
            bool add_on = true;
            if (croupier_hand[0].Name == Name.Ace || croupier_hand[0].Dignity == 10)
            {
                ContentControl.ShowView_Cheking();
                bool have_croupier = _game.HaveBlackJack(croupier_hand);
                System.Threading.Thread.Sleep(2000);

                if (have_croupier)
                {
                    ContentControl.ShowHand(_croupierName, croupier_hand, croupierTotal);

                    bool have_user = _game.HaveBlackJack(user_hand);
                    if (have_user && insurance || !have_user && insurance)
                    {
                        ContentControl.DeadHeat_Message();
                    }
                    if (!have_user && !insurance)
                    {
                        ContentControl.ReportLoss(_userName, userTotal, croupierTotal);
                    }
                    Continue();
                }
            }
            if (_game.HaveBlackJack(user_hand))
            {
                ContentControl.ReportVictory();
                add_on = false;
                Continue();
            }

            AddonCard(add_on, user_hand, croupier_hand, userTotal, croupierTotal);
        }

        private void Continue()
        {
            bool accept = ContentControl.WantToContinue();
            if (accept)
            {
                System.Threading.Thread.Sleep(2000);
                Play();
            }
        }

        private bool CanInsure()
        {
            bool insurance = false;

            if (_game.HaveAnAce())
            {
                insurance = ContentControl.WantToInsurance();
            }
            return insurance;
        }

        private void AddonCard(bool add_on, List<Card> userHand, List<Card> croupierHand,
                                int userTotal, int croupierTotal)
        {
            while (add_on)
            {
                bool take = ContentControl.SuggestTakeACard();

                if (!take)
                {
                    add_on = false;
                    ContentControl.ShowHand(_croupierName, croupierHand, croupierTotal);

                    while (croupierTotal < Game.MINVALUE)
                    {
                        _game.TakeACard(croupierHand);
                        croupierTotal += croupierHand.Last().Dignity;
                        ContentControl.ShowHittedCard(_croupierName, croupierHand, croupierTotal);
                        System.Threading.Thread.Sleep(1000);
                    }
                    if (_game.HaveBlackJack(croupierHand))
                    {
                        ContentControl.ReportLoss(_userName, userTotal, croupierTotal);
                    }
                    if (croupierTotal > Game.BLACKJACK)
                    {
                        ContentControl.Report_CroupierHasALot();
                    }
                    if(croupierTotal < Game.BLACKJACK)
                    {
                        userTotal = _game.CalcHand(userHand);
                        if(croupierTotal > userTotal)
                        {
                            ContentControl.ReportLoss(_userName, userTotal, croupierTotal);
                            break;
                        }
                        ContentControl.ReportVictory();
                    }
                    break;
                }
                _game.TakeACard(userHand);
                userTotal = _game.CalcHand(userHand);
                ContentControl.ShowHittedCard(_userName, userHand, userTotal);

                if (userTotal > Game.BLACKJACK)
                {
                    ContentControl.ReportALotOf();
                    add_on = false;
                }
                if (userTotal == Game.BLACKJACK)
                {
                    ContentControl.ShowView_Offering();
                    continue;
                }
                continue;
            }
            Continue();
        }

    }
}

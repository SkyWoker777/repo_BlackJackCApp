using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJackCApp
{
    public class Game
    {
        private Deck _deck;
        private User _user;
        private Croupier _croupier;
        private int _remaining = 25;
        public const int BLACKJACK = 21;
        public const int MINVALUE = 17;

        public Game(string userName)
        {
            _user = new User();
            _user.Name = userName;
            _croupier = new Croupier();
            _deck = new Deck();
        }

        /// <summary>
        /// Fills hands for early game
        /// </summary>
        public void Initialize()
        {
            _user.Hand.Clear();
            _croupier.Hand.Clear();
            if (_deck.Count < _remaining)
            {
                _deck.CreateDeck();
            }

            _user.Hand.Add(_deck.GetCard());
            _croupier.Hand.Add(_deck.GetCard());
            _user.Hand.Add(_deck.GetCard());
            _croupier.Hand.Add(_deck.GetCard());
        }

        public List<Card> GetUserHand()
        {
            return _user.Hand;
        }
        public List<Card> GetCroupierHand()
        {
            return _croupier.Hand;
        }

        /// <summary>
        /// Returns false if player has no BLACKJACK
        /// </summary>
        /// <param name="hand"></param>
        public bool HaveBlackJack(List<Card> hand)
        {
            int total = CalcHand(hand);
            return (total == BLACKJACK) ? true : false;
        }

        public int CalcHand(List<Card> hand)
        {
            int sum = 0;
            foreach (var card in hand)
            {
                sum += card.Dignity;
            }
            return sum;
        }

        /// <summary>
        /// Checks an ace in dealer's hands
        /// </summary>
        /// <returns>Returns false if dealer has no an Ace</returns>
        public bool HaveAnAce()
        {
            return (_croupier.Hand[0].Name == Name.Ace) ? true : false;
        }

        /// <summary>
        /// [Hit] the card in player's hand
        /// </summary>
        public void TakeACard(List<Card> hand)
        {
            int total = 0;
            hand.Add(_deck.GetCard());

            if (hand.Last().Name == Name.Ace)
            {
                total = CalcHand(hand);
                if (total > BLACKJACK)
                {
                    // Now the dignity of Ace is 1
                    hand.Last().Dignity = 1;
                }
            }
        }

    }
}

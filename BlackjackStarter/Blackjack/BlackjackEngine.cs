using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackStarter_WPF_Application.Blackjack
{
    internal class BlackjackEngine
    {
        public List<Card> Deck { get; private set; }
        public List<Card> PlayerHand { get; private set; }
        public List<Card> DealerHand { get; private set; }
        private Random random = new Random();

        public BlackjackEngine()
        {
            Deck = new List<Card>();
            PlayerHand = new List<Card>();
            DealerHand = new List<Card>();
        }

        public void Init()
        {
            Deck.Clear();
            PlayerHand.Clear();
            DealerHand.Clear();

            string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
            for (int i = 1; i <= 13; i++)
            {
                foreach (string suit in suits)
                {
                    Deck.Add(new Card(i, suit));
                }
            }
            ShuffleDeck();
            DealInitialCards();
        }

        private void ShuffleDeck()
        {
            Deck = Deck.OrderBy(x => random.Next()).ToList();
        }

        private void DealInitialCards()
        {
            DealToPlayer();
            DealToPlayer();
            DealToDealer();
            DealToDealer();
        }

        public void DealToPlayer()
        {
            if (Deck.Count > 0)
            {
                Card c = Deck[0];
                Deck.RemoveAt(0);
                PlayerHand.Add(c);
            }
        }

        public void DealToDealer()
        {
            if (Deck.Count > 0)
            {
                Card c = Deck[0];
                Deck.RemoveAt(0);
                DealerHand.Add(c);
            }
        }

        public bool IsPlayerBusted()
        {
            return GetHandValue(PlayerHand) > 21;
        }

        public bool IsDealerBusted()
        {
            return GetHandValue(DealerHand) > 21;
        }

        public int GetHandValue(List<Card> hand)
        {
            int sum = 0;
            bool hasAce = false;
            foreach (var card in hand)
            {
                if (card.Rank == 1)
                {
                    hasAce = true;
                    sum += 11;
                }
                else
                {
                    sum += card.Value;
                }
            }
            if (hasAce && sum > 21)
            {
                sum -= 10;
            }
            return sum;
        }

        public void PlayDealerTurn()
        {
            while (GetHandValue(DealerHand) < 17)
            {
                DealToDealer();
            }
        }

        public Card GetDealerFaceUpCard()
        {
            return DealerHand[0];
        }
    }
}

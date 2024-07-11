namespace BlackJackStarter_WPF_Application.Blackjack
{
    internal class Card
    {
        public int Rank { get; set; }
        public string Suit { get; set; }
        public int Value { get; set; }

        public Card(int rank, string suit)
        {
            Rank = rank;
            Suit = suit;
            SetCardValue();
        }

        public string GetFilename()
        {
            string rankText = Rank.ToString();
            if (Rank == 1) rankText = "A";
            else if (Rank == 11) rankText = "J";
            else if (Rank == 12) rankText = "Q";
            else if (Rank == 13) rankText = "K";
            return rankText + Suit.Substring(0, 1) + ".jpg";
        }

        public void SetCardValue()
        {
            if (Rank >= 2 && Rank <= 10)
            {
                Value = Rank;
            }
            else if (Rank == 1)
            {
                Value = 11;
            }
            else if (Rank >= 11 && Rank <= 13)
            {
                Value = 10;
            }
            else
            {
                throw new ArgumentException("Invalid card rank");
            }
        }
    }
}

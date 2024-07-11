using BlackJackStarter_WPF_Application.Blackjack;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BlackjackStarter
{
    public partial class MainWindow : Window
    {
        BlackjackEngine game = new BlackjackEngine();
        string Path = "C:/Users/aswad/OneDrive/Documents/Z_Semester_4/.net/BlackjackStarter/BlackjackStarter/JPEG/";
        string BackCardPath = "C:/Users/aswad/OneDrive/Documents/Z_Semester_4/.net/BlackjackStarter/BlackjackStarter/JPEG/back.jpg";

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            game.DealToPlayer();
            if (game.IsPlayerBusted())
            {
                MessageBox.Show("Player Busted! Dealer Won");
                EndGame();
            }
            RefreshPlayerScreen();
        }

        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            game.PlayDealerTurn();
            RefreshDealerScreen(true);
            if (game.IsDealerBusted())
            {
                MessageBox.Show("Dealer Busted! Player Won");
            }
            else
            {
                int playerHandValue = game.GetHandValue(game.PlayerHand);
                int dealerHandValue = game.GetHandValue(game.DealerHand);
                if (playerHandValue > dealerHandValue)
                {
                    MessageBox.Show("Player Won");
                }
                else if (playerHandValue < dealerHandValue)
                {
                    MessageBox.Show("Dealer Won");
                }
                else
                {
                    MessageBox.Show("It's a Push");
                }
            }
            EndGame();
        }

        private void StartNewGame()
        {
            game.Init();
            RefreshPlayerScreen();
            RefreshDealerScreen();
            HitButton.IsEnabled = true;
            StandButton.IsEnabled = true;
            NewGameButton.Visibility = Visibility.Hidden;
        }

        private void EndGame()
        {
            HitButton.IsEnabled = false;
            StandButton.IsEnabled = false;
            NewGameButton.Visibility = Visibility.Visible;
        }

        public void RefreshPlayerScreen()
        {
            PlayerPanel.Children.Clear();
            foreach (Card c in game.PlayerHand)
            {
                ShowPlayerCard(Path + c.GetFilename());
            }
        }

        public void RefreshDealerScreen(bool revealHiddenCard = false)
        {
            DealerPanel.Children.Clear();
            ShowDealerCard(Path + game.GetDealerFaceUpCard().GetFilename());
            for (int i = 1; i < game.DealerHand.Count; i++)
            {
                if (revealHiddenCard)
                {
                    ShowDealerCard(Path + game.DealerHand[i].GetFilename());
                }
                else
                {
                    ShowDealerCard(BackCardPath);
                }
            }
        }

        private void ShowPlayerCard(string fileName)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(fileName));
            Image image = new Image();
            image.Source = bitmap;
            PlayerPanel.Children.Add(image);
        }

        private void ShowDealerCard(string fileName)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(fileName));
            Image image = new Image();
            image.Source = bitmap;
            DealerPanel.Children.Add(image);
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
    }
}

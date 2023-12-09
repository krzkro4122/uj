using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using BootlegEngine;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System;

namespace BootlegWarThunder
{
    public partial class MainWindow : Window
    {
        readonly List<string> Letters = new() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        private const int BoardSize = 10;
        private const int Dimension = 50;

        private int currentPlayerIndex = 0;

        private readonly List<Board> boards = new();
        private readonly List<Player> players = new();
        private readonly Button[] buttons = new Button[BoardSize * BoardSize];

        public MainWindow()
        {
            InitializeComponent();            
            InitializeBoards();
            InitializeVisualBoards();
            InitializePlayers();            
        }

        private void InitializeBoards()
        {
            boards.Add(new());
            boards.Add(new());
        }

        private void InitializeVisualBoards()
        {
            CreateBoardForPlayer1();
            CreateBoardForPlayer2();

            switchBoards();            
        }

        private void InitializePlayers()
        {
            for (int i = 0; i < 2; i++)
            {
                players.Add(new Player());
            }
            this.DataContext = new MyViewModel(players);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            AttackAndChangeButtonState(button);

            UpdateScoreLabels();

            bool result = CheckForWin();

            if (!result)
            {
                currentPlayerIndex = (currentPlayerIndex + 1) % 2;
                switchBoards();
            }
        }        

        private void CreateBoardForPlayer1()
        {
            Board board = boards[1];
            for (uint i = 0; i < BoardSize; i++)
            {
                for (uint j = 0; j < BoardSize; j++)
                {
                    uint accessIndex = i * 10 + j;
                    Button button = CreateButton(i, j, true, board.Tiles[(int)accessIndex]);
                    buttons[accessIndex] = button;

                    player1Board.Opacity = 0.8;
                    player1Board.Children.Add(button);
                    Grid.SetRow(button, (int)i);
                    Grid.SetColumn(button, (int)j);
                }
            }
        }

        private void CreateBoardForPlayer2()
        {       
            Board board = boards[0];
            for (uint i = 0; i < BoardSize; i++)
            {
                for (uint j = 0; j < BoardSize; j++)
                {
                    uint accessIndex = i * 10 + j;
                    Button button = CreateButton(i, j, false, board.Tiles[(int)accessIndex]);
                    buttons[accessIndex] = button;
                    
                    player2Board.Children.Add(button);
                    Grid.SetRow(button, (int)i);
                    Grid.SetColumn(button, (int)j);
                }
            }
        }

        private Button CreateButton(uint i, uint j, bool showShips, Tile currentTile)
        {
            Label tileLabel = new()
            {
                Foreground = Brushes.White,
                Content = CreateLabelContent(showShips, i, j, currentTile.reward)
            };

            Button button = new()
            {
                Width = Dimension,
                Height = Dimension,
                Tag = currentTile
            };
            button.Click += Button_Click;
            button.Content = tileLabel;
            button.Background = new BrushConverter().ConvertFrom("#FF122C7D") as SolidColorBrush;
            button.FontSize = 20;
            return button;
        }

        private string CreateLabelContent(bool showShips, uint i, uint j, uint reward)
        {
            if (showShips && reward > 0) return "🚢";

            uint associatedDigit = i;
            string associatedLetter = Letters[(int)j];
            return $"{associatedDigit}{associatedLetter}";           
        }

        private void AttackAndChangeButtonState(Button button)
        {
            Player attackingPlayer = players[currentPlayerIndex];
            Board currentBoard = boards[currentPlayerIndex];

            Tile tile = (Tile)button.Tag;

            uint previousScore = attackingPlayer.Score;
            attackingPlayer.Score += currentBoard.attackTile(tile.index);

            uint scoreDifference = attackingPlayer.Score - previousScore;
            if (scoreDifference == 0)
            {
                button.Content = "❌";
                button.Background = Brushes.SlateGray;
                button.Foreground = Brushes.White;
                button.Opacity = 0.50;
            }
            else
            {
                button.Content = "🚢";
                button.Background = new BrushConverter().ConvertFrom("#FFA20000") as SolidColorBrush;
                button.Foreground = Brushes.White;
                button.Opacity = 0.50;
            }

            button.IsHitTestVisible = false;
        }

        private void UpdateScoreLabels()
        {
            Player currentPlayer = players[currentPlayerIndex];
            string newText;

            if (currentPlayerIndex == 0)
            {
                newText = $"Player 1 (You) - score: {currentPlayer.Score}";
            }
            else
            {
                newText = $"Player 2 (AI) - score: {currentPlayer.Score}";
            }

            switch (currentPlayerIndex)
            {
                case 0:
                    ((MyViewModel)DataContext).Player1 = newText;
                    break;
                case 1:
                    ((MyViewModel)DataContext).Player2 = newText;
                    break;
                default:
                    break;
            }
        }

        private bool CheckForWin()
        {
            Player currentPlayer = players[currentPlayerIndex];

            if (currentPlayer.Score == 20)
            {
                MessageBox.Show($"Player {currentPlayerIndex + 1} won!");

                if (currentPlayerIndex == 0)
                {
                    p1.Background = new SolidColorBrush(Colors.DarkGreen);
                    p1.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    p2.Background = new SolidColorBrush(Colors.DarkGreen);
                    p2.Foreground = new SolidColorBrush(Colors.White);
                }
                Winda.IsEnabled = false;
                return true;
            }
            return false;
        }

        private void switchBoards()
        {
            switch (currentPlayerIndex)
            {
                case 0:
                    player1Board.IsHitTestVisible = false;
                    player2Board.IsHitTestVisible = true;
                    break;

                case 1:
                    player1Board.IsHitTestVisible = true;
                    player2Board.IsHitTestVisible = false;

                    Random random = new();
                    ButtonAutomationPeer peer = new
                    (
                        player1Board.Children[random.Next(BoardSize * BoardSize)] as Button
                    );
                    IInvokeProvider? invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    invokeProv?.Invoke();
                    break;
            }
        }
    }
}

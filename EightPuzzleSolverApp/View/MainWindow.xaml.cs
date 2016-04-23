using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using EightPuzzleSolver.EightPuzzle;
using EightPuzzleSolverApp.ViewModel;

namespace EightPuzzleSolverApp.View
{
    public partial class MainWindow : Window
    {
        private class Tile
        {
            private readonly TextBlock _textBlock;

            public Tile(Decorator element)
            {
                Element = element;
                _textBlock = (TextBlock) element.Child;
            }

            private Decorator Element { get; }

            public void SetText(string text)
            {
                _textBlock.Text = text;
            }

            public void SetVisibility(Visibility visibility)
            {
                Element.Visibility = visibility;
            }

            public void Move(MoveDirection direction, Action callback, int durationMs = 900)
            {
                var duration = new Duration(TimeSpan.FromMilliseconds(durationMs));

                bool horizontal = direction.ColumnChange != 0;
                int diff = horizontal ? direction.ColumnChange : direction.RowChange;

                var transform = new TranslateTransform();
                Element.RenderTransform = transform;

                var anim = new DoubleAnimation(diff * TileSize, duration);
                anim.Completed += (s, e) =>
                {
                    Element.RenderTransform = null;

                    callback();
                };
                transform.BeginAnimation(horizontal ? TranslateTransform.XProperty : TranslateTransform.YProperty, anim);
            }
        }

        private MainViewModel _viewModel;

        private const int TileSize = 70;

        private Tile[,] _tiles;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = (MainViewModel) DataContext;

            _viewModel.CreateBoard += VmOnCreateBoard;
            _viewModel.ShowMoves += VmShowMoves;

            _viewModel.FillBoardCommand.Execute(null);
        }

        private void VmOnCreateBoard(object sender, CreateBoardEventArgs args)
        {
            var board = args.Board;

            grdBoard.Children.Clear();

            grdBoard.Rows = board.RowCount;
            grdBoard.Columns = board.ColumnCount;

            grdBoard.Height = board.RowCount * TileSize;
            grdBoard.Width = board.ColumnCount * TileSize;

            _tiles = new Tile[board.RowCount, board.ColumnCount];

            for (int i = 0; i < board.RowCount; i++)
            {
                for (int j = 0; j < board.ColumnCount; j++)
                {
                    var textBlock = new TextBlock
                    {
                        FontSize = 20,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    var border = new Border
                    {
                        Background = new SolidColorBrush(Colors.WhiteSmoke),
                        CornerRadius = new CornerRadius(3),
                        Margin = new Thickness(3),
                        Child = textBlock
                    };

                    _tiles[i, j] = new Tile(border);

                    grdBoard.Children.Add(border);
                }
            }

            SetTileValues(board);
        }

        private void VmShowMoves(object sender, EventArgs eventArgs)
        {
            ShowNextMove();
        }

        private void ShowNextMove()
        {
            var state = _viewModel.NextMoveState();

            if (state == null)
                return;

            var tilePos = state.Board.BlankTilePosition;

            Debug.Assert(state.Direction != null, "state.Direction != null");

            var direction = state.Direction.Value.Opposite();

            _tiles[tilePos.Row, tilePos.Column].Move(direction, () =>
            {
                SetTileValues(state.Board);

                ShowNextMove();
            });
        }

        private void SetTileValues(Board board)
        {
            for (int i = 0; i < board.RowCount; i++)
            {
                for (int j = 0; j < board.ColumnCount; j++)
                {
                    int val = board[i, j];

                    var tile = _tiles[i, j];

                    tile.SetVisibility(val == 0 ? Visibility.Hidden : Visibility.Visible);

                    tile.SetText(val.ToString());
                }
            }
        }


        private void lstMoves_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ((ListBox)sender).ScrollIntoView(e.AddedItems[0]);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using EightPuzzleSolver.EightPuzzle;
using Xunit;

namespace EightPuzzleSolver.Tests.EightPuzzle
{
    public class BoardTests
    {
        [Fact]
        public void EqualsTest()
        {
            var board1 = CreateBoard(new byte[,]
            {
                { 0, 1, 2 },
                { 5, 4, 3 }
            });

            var board2 = CreateBoard(new byte[,]
            {
                { 0, 1, 2 },
                { 5, 4, 3 }
            });

            var board3 = CreateBoard(new byte[,]
            {
                { 0, 2, 1 },
                { 5, 4, 3 }
            });

            Assert.Equal(board1, board2);
            Assert.Equal(board2, board1);
            Assert.True(board1 == board2);
            Assert.False(board1 != board2);

            Assert.NotEqual(board1, board3);
            Assert.NotEqual(board3, board1);
            Assert.True(board3 != board1);
            Assert.False(board3 == board1);
        }

        [Fact]
        public void BlankTilePositionTest()
        {
            var board = CreateBoard(new byte[,]
            {
                { 5, 1, 2 },
                { 4, 3, 0 }
            });

            Assert.Equal(1, board.BlankTilePosition.Row);
            Assert.Equal(2, board.BlankTilePosition.Column);
        }

        [Fact]
        public void MoveTest()
        {
            var board = CreateBoard(new byte[,]
            {
                { 5, 1, 2 },
                { 4, 3, 0 }
            });

            Assert.True(board.CanMove(MoveDirection.Left));
            Assert.False(board.CanMove(MoveDirection.Right));
            Assert.True(board.CanMove(MoveDirection.Top));
            Assert.False(board.CanMove(MoveDirection.Bottom));

            Assert.Throws<ArgumentException>(() => board.Move(MoveDirection.Right));

            var newBoard = board.Move(MoveDirection.Left);

            Assert.Equal(CreateBoard(new byte[,]
            {
                { 5, 1, 2 },
                { 4, 0, 3 }
            }), newBoard);
            Assert.True(newBoard.CanMove(MoveDirection.Left));
            Assert.True(newBoard.CanMove(MoveDirection.Right));
            Assert.True(newBoard.CanMove(MoveDirection.Top));
            Assert.False(newBoard.CanMove(MoveDirection.Bottom));

            newBoard = newBoard.Move(MoveDirection.Top);

            Assert.Equal(CreateBoard(new byte[,]
            {
                { 5, 0, 2 },
                { 4, 1, 3 }
            }), newBoard);
            Assert.True(newBoard.CanMove(MoveDirection.Left));
            Assert.True(newBoard.CanMove(MoveDirection.Right));
            Assert.False(newBoard.CanMove(MoveDirection.Top));
            Assert.True(newBoard.CanMove(MoveDirection.Bottom));

            newBoard = newBoard.Move(MoveDirection.Right);

            Assert.Equal(CreateBoard(new byte[,]
            {
                { 5, 2, 0 },
                { 4, 1, 3 }
            }), newBoard);
            Assert.True(newBoard.CanMove(MoveDirection.Left));
            Assert.False(newBoard.CanMove(MoveDirection.Right));
            Assert.False(newBoard.CanMove(MoveDirection.Top));
            Assert.True(newBoard.CanMove(MoveDirection.Bottom));

            newBoard = newBoard.Move(MoveDirection.Bottom);

            Assert.Equal(CreateBoard(new byte[,]
            {
                { 5, 2, 3 },
                { 4, 1, 0 }
            }), newBoard);
            Assert.True(newBoard.CanMove(MoveDirection.Left));
            Assert.False(newBoard.CanMove(MoveDirection.Right));
            Assert.True(newBoard.CanMove(MoveDirection.Top));
            Assert.False(newBoard.CanMove(MoveDirection.Bottom));

            newBoard = newBoard.Move(MoveDirection.Left)
                .Move(MoveDirection.Left)
                .Move(MoveDirection.Top);

            Assert.Equal(CreateBoard(new byte[,]
            {
                { 0, 2, 3 },
                { 5, 4, 1 }
            }), newBoard);
            Assert.False(newBoard.CanMove(MoveDirection.Left));
            Assert.True(newBoard.CanMove(MoveDirection.Right));
            Assert.False(newBoard.CanMove(MoveDirection.Top));
            Assert.True(newBoard.CanMove(MoveDirection.Bottom));

            Assert.Equal(CreateBoard(new byte[,]
            {
                { 5, 1, 2 },
                { 4, 3, 0 }
            }), board);
        }

        [Fact]
        public void ShouldNotHaveDuplicates()
        {
            var board = CreateBoard(new byte[,]
            {
                { 5, 1, 2 },
                { 4, 3, 0 }
            });

            var set = new HashSet<Board>();
            set.Add(board);
            set.Add(board);
            set.Add(board.Move(MoveDirection.Left));
            set.Add(board.Move(MoveDirection.Left));
            set.Add(board.Move(MoveDirection.Top));
            set.Add(board.Move(MoveDirection.Top));

            Assert.Equal(new HashSet<Board> { board, board.Move(MoveDirection.Left), board.Move(MoveDirection.Top) }, set);
        }

        [Fact]
        public void IsSolvableTest()
        {
            var unsolvableBoards = new[]
            {
                new byte[,]
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 8, 7, 0 }
                },
                new byte[,]
                {
                    { 1, 2, 3 },
                    { 4, 6, 7 },
                    { 8, 5, 0 }
                },
                new byte[,]
                {
                    { 8, 1, 2 },
                    { 0, 4, 3 },
                    { 7, 6, 5 }
                },
                new byte[,]
                {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 },
                    { 9, 10, 11, 12 },
                    { 13, 15, 14, 0 }
                },
                new byte[,]
                {
                    { 4, 5, 0 },
                    { 1, 3, 2 }
                },
                new byte[,]
                {
                    { 0, 7, 2, 1 },
                    { 4, 6, 3, 5 }
                },
                new byte[,]
                {
                    { 1, 3 },
                    { 2, 0 }
                },
            };

            var solvableBoards = new[]
            {
                new byte[,]
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 0, 7, 8 }
                },
                new byte[,]
                {
                    { 1, 8, 2 },
                    { 0, 4, 3 },
                    { 7, 6, 5 }
                },
                new byte[,]
                {
                    { 8, 6, 7 },
                    { 2, 5, 4 },
                    { 3, 0, 1 }
                },
                new byte[,]
                {
                    { 12, 1, 10, 2 },
                    { 7, 11, 4, 14 },
                    { 5, 0, 9, 15 },
                    { 8, 13, 6, 3 }
                },
                new byte[,]
                {
                    { 0, 5, 3, 2, 1 },
                    { 9, 4, 8, 7, 6 }
                },
                new byte[,]
                {
                    { 0, 7, 2, 1 },
                    { 4, 3, 6, 5 }
                },
                new byte[,]
                {
                    { 4, 5, 0 },
                    { 1, 2, 3 }
                },
                new byte[,]
                {
                    { 0, 3 },
                    { 2, 1 }
                },
            };

            foreach (var data in unsolvableBoards)
            {
                var board = CreateBoard(data);

                Assert.False(board.IsSolvable(), board.ToString());
            }

            foreach (var data in solvableBoards)
            {
                var board = CreateBoard(data);

                Assert.True(board.IsSolvable(), board.ToString());
            }
        }

        [Fact]
        public void CreateGoalBoardTest()
        {
            Assert.Equal(Board.CreateGoalBoard(3, 3), new Board(new byte[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 0 }
            }));

            Assert.Equal(Board.CreateGoalBoard(2, 3), new Board(new byte[,]
            {
                { 1, 2, 3 },
                { 4, 5, 0 }
            }));
        }

        private Board CreateBoard(byte[,] data)
        {
            var board = new Board(data);

            Assert.True(board.IsCorrect(), "incorrect board used in tests");

            return board;
        }
    }
}

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
            var board1 = new Board(new byte[,]
            {
                { 0, 1, 2 },
                { 5, 4, 3 }
            });

            var board2 = new Board(new byte[,]
            {
                { 0, 1, 2 },
                { 5, 4, 3 }
            });

            var board3 = new Board(new byte[,]
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
        public void ShouldReturnDataCopy()
        {
            var board = new Board(new byte[,]
            {
                { 0, 1, 2 },
                { 5, 4, 3 }
            });

            var data = board.GetData();
            data[1, 0] = 6;

            Assert.Equal(5, board[1, 0]);
        }

        [Fact]
        public void BlankTilePositionTest()
        {
            var board = new Board(new byte[,]
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
            var board = new Board(new byte[,]
            {
                { 5, 1, 2 },
                { 4, 3, 0 }
            });

            Assert.True(board.CanMove(MoveDirection.Left));
            Assert.False(board.CanMove(MoveDirection.Right));
            Assert.True(board.CanMove(MoveDirection.Top));
            Assert.False(board.CanMove(MoveDirection.Bottom));

            Assert.Throws<IndexOutOfRangeException>(() => board.Move(MoveDirection.Right));

            var newBoard = board.Move(MoveDirection.Left);

            Assert.Equal(new Board(new byte[,]
            {
                { 5, 1, 2 },
                { 4, 0, 3 }
            }), newBoard);
            Assert.True(newBoard.CanMove(MoveDirection.Left));
            Assert.True(newBoard.CanMove(MoveDirection.Right));
            Assert.True(newBoard.CanMove(MoveDirection.Top));
            Assert.False(newBoard.CanMove(MoveDirection.Bottom));

            newBoard = newBoard.Move(MoveDirection.Top);

            Assert.Equal(new Board(new byte[,]
            {
                { 5, 0, 2 },
                { 4, 1, 3 }
            }), newBoard);
            Assert.True(newBoard.CanMove(MoveDirection.Left));
            Assert.True(newBoard.CanMove(MoveDirection.Right));
            Assert.False(newBoard.CanMove(MoveDirection.Top));
            Assert.True(newBoard.CanMove(MoveDirection.Bottom));

            newBoard = newBoard.Move(MoveDirection.Right);

            Assert.Equal(new Board(new byte[,]
            {
                { 5, 2, 0 },
                { 4, 1, 3 }
            }), newBoard);
            Assert.True(newBoard.CanMove(MoveDirection.Left));
            Assert.False(newBoard.CanMove(MoveDirection.Right));
            Assert.False(newBoard.CanMove(MoveDirection.Top));
            Assert.True(newBoard.CanMove(MoveDirection.Bottom));

            newBoard = newBoard.Move(MoveDirection.Bottom);

            Assert.Equal(new Board(new byte[,]
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

            Assert.Equal(new Board(new byte[,]
            {
                { 0, 2, 3 },
                { 5, 4, 1 }
            }), newBoard);
            Assert.False(newBoard.CanMove(MoveDirection.Left));
            Assert.True(newBoard.CanMove(MoveDirection.Right));
            Assert.False(newBoard.CanMove(MoveDirection.Top));
            Assert.True(newBoard.CanMove(MoveDirection.Bottom));

            Assert.Equal(new Board(new byte[,]
            {
                { 5, 1, 2 },
                { 4, 3, 0 }
            }), board);
        }

        [Fact]
        public void ShouldNotHaveDuplicates()
        {
            var board = new Board(new byte[,]
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
    }
}

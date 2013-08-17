using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace QueensEight.Processor.Test
{
    [TestFixture]
    public class PlacingQueens
    {
        private Board board;

        [SetUp]
        public void SetupBoard()
        {
            board = new Board();
        }

        [Test]
        public void PlacedQueenIsOnBoard()
        {
            var position = new Position { Row = 3, Column = 2 };
            var queen = new Queen { Position = position };
            var queens = new[] { queen };

            board.PlaceQueens(queens);

            Assert.AreEqual(board.Queens.Count(), 1);
        }

        [Test]
        public void PlacedQueenIsInCorrectPosition()
        {
            var position = new Position { Row = 3, Column = 2 };
            var queen = new Queen { Position = position };
            var queens = new[] { queen };

            board.PlaceQueens(queens);

            Assert.AreEqual(board.Queens.First().Position, position);
        }

        [Test]
        public void InvalidPositionQueensAreNotAccepted()
        {
            var position = new Position { Row = 3, Column = 8 };
            var queen = new Queen() { Position = position };
            var queens = new[] { queen };

            Assert.Throws<ArgumentException>(() => board.PlaceQueens(queens));
        }
    }
}

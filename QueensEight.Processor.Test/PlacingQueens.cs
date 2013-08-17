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

            board.PlaceQueensAtPositions(new [] {position});

            Assert.AreEqual(board.Queens.Count(), 1);
        }

        [Test]
        public void PlacedQueenIsInCorrectPosition()
        {
            var position = new Position { Row = 3, Column = 2 };

            board.PlaceQueensAtPositions(new [] {position});

            Assert.AreEqual(board.Queens.First().Position, position);
        }

        [Test]
        public void InvalidPositionQueensAreNotAccepted()
        {
            var position = new Position { Row = 3, Column = 8 };

            Assert.Throws<ArgumentException>(() => board.PlaceQueensAtPositions(new [] {position}));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using QueensEight.Processor;

namespace Processor.Test
{
    [TestFixture]
    public class GivenAQueenAtTopLeft
    {
        private Queen _queen;
        private Board _board;
        [SetUp]
        public void SetupQueen()
        {
            var position = new Position { Row = 0, Column = 0 };
            _queen = new Queen { Position = position };
            _board = new Board();
            _board.PlaceQueensAtPositions(new[] { position });
        }

        [TestCase(0, 1, ExpectedResult = true)]
        [TestCase(0, 2, ExpectedResult = true)]
        [TestCase(0, 3, ExpectedResult = true)]
        [TestCase(0, 4, ExpectedResult = true)]
        [TestCase(0, 5, ExpectedResult = true)]
        [TestCase(0, 6, ExpectedResult = true)]
        [TestCase(0, 7, ExpectedResult = true)]
        public bool RowCellsAreAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [TestCase(1, 0, ExpectedResult = true)]
        [TestCase(2, 0, ExpectedResult = true)]
        [TestCase(3, 0, ExpectedResult = true)]
        [TestCase(4, 0, ExpectedResult = true)]
        [TestCase(5, 0, ExpectedResult = true)]
        [TestCase(6, 0, ExpectedResult = true)]
        [TestCase(7, 0, ExpectedResult = true)]
        public bool ColumnCellsAreAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(2, 2, ExpectedResult = true)]
        [TestCase(3, 3, ExpectedResult = true)]
        [TestCase(4, 4, ExpectedResult = true)]
        [TestCase(5, 5, ExpectedResult = true)]
        [TestCase(6, 6, ExpectedResult = true)]
        [TestCase(7, 7, ExpectedResult = true)]
        public bool DiagonalCellsAreAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [TestCase(1, 2, ExpectedResult = false)]
        public bool NotInRowColumnOrDiagonalNotAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [Test]
        public void RemainingCellsAreNotAttacked()
        {
            foreach (var remainingPosition in _board.RemainingPositions())
            {
                Assert.False(_queen.AttacksPosition(remainingPosition));
            }
        }

        [Test]
        public void RemainingCellsDoNotIncludeCurrent()
        {
            var remainingPositions = _board.RemainingPositions();
            var isIncluded = remainingPositions.Any(p => p.Equals(_queen.Position));

            Assert.False(isIncluded);
        }

        [Test]
        public void CellsNotInRemainingAreAttacked()
        {
            var allPositions = new Board().RemainingPositions();
            var remainingPositions = _board.RemainingPositions();

            foreach (var attackedPositions in allPositions.Except(remainingPositions))
            {
                bool attacksPosition = _queen.AttacksPosition(attackedPositions);
                if (!attacksPosition) { System.Diagnostics.Debugger.Break(); }
                Assert.True(attacksPosition);
            }
        }
    }
}

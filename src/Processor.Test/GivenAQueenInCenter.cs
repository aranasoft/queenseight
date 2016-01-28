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
    public class GivenAQueenInCenter
    {
        private Queen _queen;

        [SetUp]
        public void SetupQeen()
        {
            _queen = new Queen { Position = new Position { Row = 4, Column = 4 } };
        }

        [TestCase(4, 0, ExpectedResult = true)]
        [TestCase(4, 1, ExpectedResult = true)]
        [TestCase(4, 2, ExpectedResult = true)]
        [TestCase(4, 3, ExpectedResult = true)]
        [TestCase(4, 5, ExpectedResult = true)]
        [TestCase(4, 6, ExpectedResult = true)]
        [TestCase(4, 7, ExpectedResult = true)]
        public bool RowCellsAreAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [TestCase(0, 4, ExpectedResult = true)]
        [TestCase(1, 4, ExpectedResult = true)]
        [TestCase(2, 4, ExpectedResult = true)]
        [TestCase(3, 4, ExpectedResult = true)]
        [TestCase(5, 4, ExpectedResult = true)]
        [TestCase(6, 4, ExpectedResult = true)]
        [TestCase(7, 4, ExpectedResult = true)]
        public bool ColumnCellsAreAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(2, 2, ExpectedResult = true)]
        [TestCase(3, 3, ExpectedResult = true)]
        [TestCase(5, 5, ExpectedResult = true)]
        [TestCase(6, 6, ExpectedResult = true)]
        [TestCase(7, 7, ExpectedResult = true)]
        [TestCase(1, 7, ExpectedResult = true)]
        [TestCase(2, 6, ExpectedResult = true)]
        [TestCase(3, 5, ExpectedResult = true)]
        [TestCase(5, 3, ExpectedResult = true)]
        [TestCase(6, 2, ExpectedResult = true)]
        [TestCase(7, 1, ExpectedResult = true)]
        public bool DiagonalCellsAreAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [TestCase(1, 2, ExpectedResult = false)]
        public bool NotInRowColumnOrDiagonalNotAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }
    }
}

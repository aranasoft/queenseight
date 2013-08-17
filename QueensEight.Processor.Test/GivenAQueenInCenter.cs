using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace QueensEight.Processor.Test
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

        [TestCase(4, 0, Result = true)]
        [TestCase(4, 1, Result = true)]
        [TestCase(4, 2, Result = true)]
        [TestCase(4, 3, Result = true)]
        [TestCase(4, 5, Result = true)]
        [TestCase(4, 6, Result = true)]
        [TestCase(4, 7, Result = true)]
        public bool RowCellsAreAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [TestCase(0, 4, Result = true)]
        [TestCase(1, 4, Result = true)]
        [TestCase(2, 4, Result = true)]
        [TestCase(3, 4, Result = true)]
        [TestCase(5, 4, Result = true)]
        [TestCase(6, 4, Result = true)]
        [TestCase(7, 4, Result = true)]
        public bool ColumnCellsAreAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [TestCase(0, 0, Result = true)]
        [TestCase(1, 1, Result = true)]
        [TestCase(2, 2, Result = true)]
        [TestCase(3, 3, Result = true)]
        [TestCase(5, 5, Result = true)]
        [TestCase(6, 6, Result = true)]
        [TestCase(7, 7, Result = true)]
        [TestCase(1, 7, Result = true)]
        [TestCase(2, 6, Result = true)]
        [TestCase(3, 5, Result = true)]
        [TestCase(5, 3, Result = true)]
        [TestCase(6, 2, Result = true)]
        [TestCase(7, 1, Result = true)]
        public bool DiagonalCellsAreAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }

        [TestCase(1, 2, Result = false)]
        public bool NotInRowColumnOrDiagonalNotAttacked(int row, int column)
        {
            return _queen.AttacksPosition(new Position { Row = row, Column = column });
        }
    }
}

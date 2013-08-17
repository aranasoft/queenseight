using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace QueensEight.Processor.Test
{
    [TestFixture]
    public class Positions
    {
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(-1, -1)]
        public void NegativePositionsAreInvalid(int row, int column)
        {
            var position = new Position { Row = row, Column = column };
            Assert.False(position.IsValid);
        }

        [TestCase(0, 8)]
        [TestCase(8, 0)]
        [TestCase(8, 8)]
        public void OffBoardPositionsAreInvalid(int row, int column)
        {
            var position = new Position { Row = row, Column = column };
            Assert.False(position.IsValid);
        }

        [TestCase(0, 0)]
        [TestCase(7, 7)]
        [TestCase(4, 4)]
        [TestCase(2, 3)]
        public void OnBoardPositionsAreValid(int row, int column)
        {
            var position = new Position { Row = row, Column = column };
            Assert.True(position.IsValid);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace QueensEight.Processor.Test
{
    [TestFixture]
    public class GivenAnEmptyBoard
    {
        private Board board;

        [SetUp]
        public void SetupBoard()
        {
            board = new Board();
        }
        [Test]
        public void AllCellsAreEmpty()
        {
            Assert.IsFalse(board.Queens.Any());
        }
    }
}

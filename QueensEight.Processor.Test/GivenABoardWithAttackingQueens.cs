using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace QueensEight.Processor.Test
{
    [TestFixture]
    public class GivenBoardWithAttackingQueens
    {
        private Board board;
        [SetUp]
        public void SetupBoard()
        {
            board = new Board();
            var queen1 = new Queen { Position = new Position { Row = 2, Column = 3 } };
            var queen2 = new Queen { Position = new Position { Row = 5, Column = 3 } };
            var queens = new[] { queen1, queen2 };

            board.PlaceQueens(queens);
        }

        [Test]
        public void SolveReturnsEmptyList()
        {
            var result = board.Solve();
            Assert.NotNull(result);
            Assert.AreEqual(result.Count(), 0);
        }
    }
}

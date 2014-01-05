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
            var positions = new[]
                {
                    new Position {Row = 2, Column = 3},
                    new Position {Row = 5, Column = 3}
                };
            board.PlaceQueensAtPositions(positions);
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

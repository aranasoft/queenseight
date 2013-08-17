using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace QueensEight.Processor.Test
{
    [TestFixture]
    [Category("Integration")]
    public class Solver
    {
        [Test]
        public void Run()
        {
            var board = new Board();
            board.PlaceQueens(new List<Queen>());
            var result = board.Solve().ToList();
            Assert.AreEqual(result.Count(), 8);
        }

        [Test]
        public void SolvableWith1Placed()
        {
            var board = new Board();
            var initialQueens = new[] { new Queen { Position = new Position { Row = 0, Column = 0 } } };
            board.PlaceQueens(initialQueens);
            var result = board.Solve().ToList();
            Assert.AreEqual(result.Count(), 8);
        }

        [Test]
        public void SolvableWith2Placed()
        {
            var board = new Board();
            var initialQueens = new[]
                { 
                    new Queen {Position = new Position {Row = 2, Column = 2}},
                    new Queen {Position = new Position {Row = 0, Column = 3}},
                };
            board.PlaceQueens(initialQueens);
            var result = board.Solve().ToList();
            Assert.AreNotEqual(result.Count(), 0);
        }
        [Test]
        public void UnsolvableWith2Placed()
        {
            var board = new Board();
            var initialQueens = new[]
                { 
                    new Queen {Position = new Position {Row = 0, Column = 0}},
                    new Queen {Position = new Position {Row = 1, Column = 7}},
                };
            board.PlaceQueens(initialQueens);
            var result = board.Solve().ToList();
            Assert.AreEqual(result.Count(), 0);
        }

        [Test]
        public void Remaining()
        {
            var board = new Board();
            var result = board.RemainingPositions();

            Assert.AreEqual(result.Count(), 64);
        }
    }
}

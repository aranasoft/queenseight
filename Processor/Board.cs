using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueensEight.Processor
{
    public class Board
    {
        private int CurrentQueen;
        private int NumberOfRemainingQueens;
        private int[] QueenPositionIndicies;
        public List<Queen> Queens = new List<Queen>();
        private List<Position> _remainingPositions;

        private bool QueensCanAttack
        {
            get
            {
                if (Queens.Count == 1) return false;

                foreach (Queen queen in Queens)
                {
                    bool isAttacked = Queens
                        .Except(new[] { queen })
                        .Any(attackingQueen => attackingQueen.AttacksPosition(queen.Position));
                    if (isAttacked) return true;
                }
                return false;
            }
        }

        public void PlaceQueensAtPositions(IEnumerable<Position> positions)
        {
            var queens = positions.Select(position => new Queen() {Position = position});
            if (!AreValidPositions(queens)) throw new ArgumentException();

            Queens.AddRange(queens);

            ResetRemainingPositions();

            SetupRemainingQueens();

            ResetQueenPositions();
        }

        public IEnumerable<Queen> Solve()
        {
            var result = new List<Queen>();

            if (QueensCanAttack) return result;

            while (UnprocessedQueensRemain())
            {
                if (NoValidSolutionAvailable()) return result;

                if (AttemptedAllPositions())
                {
                    ResetAttemptedPositionIndicies();
                    CurrentQueen--;
                    if (BacktrackHasNotExcludedAllOptions())
                    {
                        SetNextPositionFor(CurrentQueen);
                    }
                    continue;
                }

                var attemptedQueens = new List<Queen>();
                for (int positionIndex = 0; positionIndex <= CurrentQueen; positionIndex++)
                {
                    int currentQueenPosition = QueenPositionIndicies[positionIndex];
                    if (currentQueenPosition == -1) continue;
                    var attemptedQueen = new Queen { Position = RemainingPositions()[currentQueenPosition] };
                    attemptedQueens.Add(attemptedQueen);
                }

                Queens.AddRange(attemptedQueens);
                if (QueensCanAttack)
                {
                    SetNextPositionFor(CurrentQueen);
                }
                else
                {
                    int previousQueen = CurrentQueen;
                    CurrentQueen++;

                    if (CurrentQueenIsLastQueen())
                    {
                        result.AddRange(Queens);
                        return result;
                    }

                    SetNextPositionAfter(CurrentQueen, previousQueen);
                    //SetNextPositionFor(CurrentQueen);
                }
                foreach (Queen queen in attemptedQueens)
                {
                    Queens.Remove(queen);
                }
            }

            result.AddRange(Queens);

            return result;
        }

        private bool BacktrackHasNotExcludedAllOptions()
        {
            return CurrentQueen >= 0;
        }

        private bool CurrentQueenIsLastQueen()
        {
            return CurrentQueen == NumberOfRemainingQueens;
        }

        private void ResetAttemptedPositionIndicies()
        {
            for (int queenPositionIndex = CurrentQueen;
                 queenPositionIndex < QueenPositionIndicies.Length;
                 queenPositionIndex++)
            {
                QueenPositionIndicies[queenPositionIndex] = -1;
            }
        }

        private bool AttemptedAllPositions()
        {
            return QueenPositionIndicies[CurrentQueen] == RemainingPositions().Count();
        }

        private bool NoValidSolutionAvailable()
        {
            return CurrentQueen < 0;
        }

        private bool UnprocessedQueensRemain()
        {
            return CurrentQueen < NumberOfRemainingQueens;
        }

        private bool QueenAtPosition(Position position)
        {
            bool isAtPosition = Queens.Any(queen => queen.Position.Equals(position));
            if (isAtPosition) return true;

            if (QueenPositionIndicies == null) return false;

            for (int positionIndex = 0; positionIndex <= CurrentQueen; positionIndex++)
            {
                int positionIndexUnderTest = QueenPositionIndicies[positionIndex];

                if (positionIndexUnderTest == -1) continue;

                Position positionUnderTest = RemainingPositions()[positionIndexUnderTest];
                isAtPosition = positionUnderTest.Equals(position);
                if (isAtPosition) return true;
            }

            return false;
        }

        private bool AreValidPositions(IEnumerable<Queen> queens)
        {
            return queens.All(queen => queen.Position.IsValid);
        }

        public List<Position> RemainingPositions()
        {
            if (_remainingPositions != null) return _remainingPositions;

            _remainingPositions = new List<Position>();
            foreach (int row in Enumerable.Range(0, 8))
            {
                foreach (int column in Enumerable.Range(0, 8))
                {
                    var testPosition = new Position { Row = row, Column = column };
                    if (QueenAtPosition(testPosition)) continue;
                    var testQueen = new Queen { Position = testPosition };
                    Queens.Add(testQueen);
                    if (!QueensCanAttack)
                    {
                        _remainingPositions.Add(testPosition);
                    }
                    Queens.Remove(testQueen);
                }
            }
            return _remainingPositions;
        }

        private void ResetRemainingPositions()
        {
            _remainingPositions = null;
            RemainingPositions();
        }

        private void SetupRemainingQueens()
        {
            NumberOfRemainingQueens = 8 - Queens.Count;
        }

        private void ResetQueenPositions()
        {
            QueenPositionIndicies = new int[NumberOfRemainingQueens];
            for (int positionIndex = 0; positionIndex < NumberOfRemainingQueens; positionIndex++)
            {
                QueenPositionIndicies[positionIndex] = -1;
            }
            QueenPositionIndicies[0] = 0;
        }


        private void SetNextPositionAfter(int queenIndex, int previousQueenIndex)
        {
            int currentQueenPosition = QueenPositionIndicies[previousQueenIndex];
            QueenPositionIndicies[queenIndex] = currentQueenPosition;
            SetNextPositionFor(queenIndex);
        }

        private void SetNextPositionFor(int queenIndex)
        {
            int currentQueenPosition = QueenPositionIndicies[queenIndex];
            bool found = false;
            while (!found)
            {
                currentQueenPosition++;
                if (currentQueenPosition == RemainingPositions().Count)
                    found = true;
                else
                    found = !QueenAtPosition(RemainingPositions()[currentQueenPosition]);
            }
            QueenPositionIndicies[queenIndex] = currentQueenPosition;
        }
    }


}

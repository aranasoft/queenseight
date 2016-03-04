﻿using System.Collections.Generic;
using QueensEight.Processor;

namespace QueensEight.Api.Data {
    public class FakeDataGenerator {
        public static void AddTestSolutions( List<Solution> solutions ) {
            var testSolutions = new List<Solution>();

            testSolutions.Add(new Solution
            {
                Positions = new List<Position>
                {
                    new Position {Row = 2, Column = 2},
                    new Position {Row = 3, Column = 0},
                    new Position {Row = 6, Column = 1},
                    new Position {Row = 7, Column = 3},
                    new Position {Row = 1, Column = 4},
                    new Position {Row = 4, Column = 5},
                    new Position {Row = 0, Column = 6},
                    new Position {Row = 5, Column = 7}
                }
            });
            testSolutions.Add(new Solution
            {
                Positions = new List<Position>
                {
                    new Position {Row = 3, Column = 2},
                    new Position {Row = 4, Column = 0},
                    new Position {Row = 7, Column = 1},
                    new Position {Row = 0, Column = 3},
                    new Position {Row = 2, Column = 4},
                    new Position {Row = 5, Column = 5},
                    new Position {Row = 1, Column = 6},
                    new Position {Row = 6, Column = 7}
                }
            });
            /*
            testSolutions.Add(new Solution
            {
                Positions = new List<Position>
                {
                    new Position {Row = 4, Column = 2},
                    new Position {Row = 5, Column = 0},
                    new Position {Row = 0, Column = 1},
                    new Position {Row = 1, Column = 3},
                    new Position {Row = 3, Column = 4},
                    new Position {Row = 6, Column = 5},
                    new Position {Row = 2, Column = 6},
                    new Position {Row = 7, Column = 7}
                }
            });
            testSolutions.Add(new Solution
            {
                Positions = new List<Position>
                {
                    new Position {Row = 5, Column = 2},
                    new Position {Row = 6, Column = 0},
                    new Position {Row = 1, Column = 1},
                    new Position {Row = 2, Column = 3},
                    new Position {Row = 4, Column = 4},
                    new Position {Row = 7, Column = 5},
                    new Position {Row = 3, Column = 6},
                    new Position {Row = 0, Column = 7}
                }
            });
            */
            testSolutions.ForEach( (solution) => solution.Hash = Position.ListHash(solution.Positions));

            solutions.AddRange(testSolutions);
        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueensEight.Processor;

namespace QueensEight.Api.Data
{
    public class InMemoryDataStore
    {
        public static List<Solution> Solutions = new List<Solution>();

        static InMemoryDataStore() {
            AddTestSolutions();
        }

        private static void AddTestSolutions()
        {
            Solutions.Add(new Solution
            {
                Hash = "12345",
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
            Solutions.Add(new Solution
            {
                Hash = "23456",
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
            Solutions.Add(new Solution
            {
                Hash = "34567",
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
            Solutions.Add(new Solution
            {
                Hash = "45678",
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
        }

    }
}
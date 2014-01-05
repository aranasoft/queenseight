using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueensEight.Processor
{
    public class Position
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public bool IsValid
        {
            get
            {
                var isRowValid = Row >= 0 && Row < 8;
                var isColumnValid = Column >= 0 && Column < 8;

                return isRowValid && isColumnValid;
            }
        }

        public Position Offset(int row, int column)
        {
            return new Position { Row = Row + row, Column = Column + column };
        }

        public override bool Equals(object obj)
        {
            var position = obj as Position;
            if (position == null) return false;

            return (position.Row == Row) && (position.Column == Column);
        }

        public override int GetHashCode()
        {
            return (Row * 10) + Column;
        }

        public static string ListHash(IEnumerable<Position> positions)
        {
            var hashBuilder = new StringBuilder();

            var hashes = positions.OrderBy(position => position.Row)
                                  .Select(position => position.GetHashCode().ToString());

            foreach (var hash in hashes) { hashBuilder.Append(hash); }

            return hashBuilder.ToString();
        }
    }
}

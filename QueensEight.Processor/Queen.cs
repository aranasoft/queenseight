using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueensEight.Processor
{
    public class Queen
    {
        public Position Position { get; set; }

        public bool AttacksPosition(Position position)
        {
            if (Position.Row == position.Row) return true;
            if (Position.Column == position.Column) return true;

            foreach (var offset in Enumerable.Range(1, 7))
            {
                var upperAttackPosition = position.Offset(-offset, -offset);
                var lowerAttackPosition = position.Offset(offset, offset);
                var ap1 = position.Offset(-offset, offset);
                var ap2 = position.Offset(offset, -offset);

                if (upperAttackPosition.IsValid && Position.Equals(upperAttackPosition)) return true;
                if (lowerAttackPosition.IsValid && Position.Equals(lowerAttackPosition)) return true;
                if (ap1.IsValid && Position.Equals(ap1)) return true;
                if (ap2.IsValid && Position.Equals(ap2)) return true;
            }

            return false;
        }

    }
}

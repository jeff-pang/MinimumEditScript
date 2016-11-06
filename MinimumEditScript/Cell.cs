using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimumEditScript
{
    public class Cell
    {
        public int Cost { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public EditActions Edit { get; set; }
        public Cell From { get; set; }
        public Cell(int x, int y, EditActions edit, int cost = 0)
        {
            X = x;
            Y = y;
            Edit = edit;
            Cost = cost;
        }

        public Cell(Cell original) : this(original.X, original.Y, original.Edit, original.Cost) { }

        public Cell(int x, int y, int cost = 0, EditActions edit = EditActions.Copy) : this(x, y, edit, cost) { }

        public override string ToString()
        {
            return $"X={X},Y={Y},Cost={Cost},Edit={Edit.ToString()}";
        }
    }
}
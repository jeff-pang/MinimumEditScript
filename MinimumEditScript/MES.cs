using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimumEditScript
{
    public enum EditActions
    {
        Copy /*aka Do nothing*/,
        Insert,
        Delete,
        Substitute
    }

    public enum Directions
    {
        Top,
        Left,
        Diagonal
    }
    
    public class MES
    {
        public List<Script> Find(string to, string from)
        {
            // columns and rows + 1 because we need to create an empty cell at [0,0]
            int columns = to.Length + 1;
            int rows = from.Length + 1;

            Cell[] cells = new Cell[rows * columns];
            cells[0] = new Cell(0, 0, EditActions.Copy);

            //initialise columns 0 with sequential values from 1 to X
            for (int c = 1; c < columns; c++)
            {
                cells[c] = new Cell(c, 0, EditActions.Insert, c);
            }

            //initialise rows 0 with sequential values from 1 to Y
            for (int r = 1; r < rows; r++)
            {
                cells[r * columns] = new Cell(0, r, EditActions.Delete, r);
            }

            //Start to Find Levenstein Distance
            for (int r = 1; r < rows; r++)
            {
                for (int c = 1; c < columns; c++)
                {
                    var leftcell = cells[r * columns + c - 1];
                    var topcell = cells[(r - 1) * columns + c];
                    var diagcell = cells[(r - 1) * columns + c - 1];
                    var currcell = cells[r * columns + c] = new Cell(c, r);

                    char topchar = to[c - 1];
                    char leftchar = from[r - 1];

                    Cell d = new Cell(diagcell.X, diagcell.Y, EditActions.Copy, diagcell.Cost);
                    if (topchar != leftchar)
                    {
                        d = Minimum(currcell, leftcell, topcell, diagcell);
                    }

                    currcell.From = cells[d.Y * columns + d.X];
                    currcell.Cost = d.Cost;
                    currcell.Edit = d.Edit;
                }
            }

            var lastCell = cells[cells.Length - 1];

            Stack<Cell> stack = new Stack<Cell>();

            var next = lastCell;
            while (next != null)
            {
                stack.Push(next);
                next = next.From;
            }
            stack.Pop();

            StringBuilder resultString = new StringBuilder();
            StringBuilder operandString = new StringBuilder();
            List<Script> script = new List<Script>();

            int len = stack.Count;

            for (int x = 0; x < len; x++)
            {
                Script s = new Script();
                var cell = stack.Pop();

                s.A = to[cell.X - 1];
                s.B = from[cell.Y - 1];
                s.Edit = cell.Edit;
                s.OperandString = resultString.ToString();
                resultString.Append(s.A);
                s.Distance = cell.Cost;
                script.Add(s);
            }

            return script;
        }

        public Cell Minimum(Cell refcell,params Cell[] compares)
        {
            Cell min = null;

            for (int x = 0; x < compares.Length; x++)
            {
                EditActions action = EditActions.Copy;

                var curr = compares[x];
                Directions dir = GetDirection(refcell, curr);

                int cost = curr.Cost;

                if (dir == Directions.Left)
                {
                    cost += 1;
                    action = EditActions.Insert;
                }
                else if (dir == Directions.Top)
                {
                    cost += 1;
                    action = EditActions.Delete;
                }
                else if (dir == Directions.Diagonal)
                {
                    action = EditActions.Substitute;
                    cost += 1;
                }

                if (min == null)
                {
                    min = new Cell(curr.X, curr.Y, action, cost);
                }
                else
                {
                    min = min.Cost <= cost ? min : new Cell(curr.X, curr.Y, action, cost);
                }
            }

            return min;
        }
        
        public Directions GetDirection(Cell curr,Cell rel)
        {
            Directions dir = Directions.Diagonal;

            int left = rel.X - curr.X;
            int top = rel.Y - curr.Y;

            if(top == -1 && left == -1)//diagnal
            {
                dir = Directions.Diagonal;
            }
            else if(top == -1 && left == 0)//top
            {
                dir = Directions.Top;
            }
            else if(top == 0 && left == -1)
            {
                dir = Directions.Left;
            }

            return dir;
        }
    }
}
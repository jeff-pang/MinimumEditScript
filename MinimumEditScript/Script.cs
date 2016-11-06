using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimumEditScript
{
    public class Script
    {
        public EditActions Edit { get; set; }
        public char A { get; set; }
        public char B { get; set; }
        public string OperandString { get; set; }
        public int Distance { get; set; }
    }
}
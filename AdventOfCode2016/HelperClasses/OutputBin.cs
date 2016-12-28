using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.HelperClasses
{
    public class OutputBin
    {
        public int identifier;
        public List<int> values;

        public OutputBin()
        {
            values = new List<int>();
        }
    }
}

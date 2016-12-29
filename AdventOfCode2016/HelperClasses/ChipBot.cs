using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.HelperClasses
{
    public class ChipBot
    {
        public int identity;

        public List<int> chips;

        public ChipBot()
        {
            chips = new List<int>();
        }

        public bool HasBothChips()
        {
            return chips.Count() == 2;
        }

        public int GetHighValue()
        {
            try
            {
                return chips.Max();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int GetLowValue()
        {
            try
            {
                return chips.Min();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void AddValue(int value)
        {
            if (!chips.Contains(value))
                chips.Add(value);
        }
    }
}

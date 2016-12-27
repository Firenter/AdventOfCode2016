using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day7 : IDay
    {
        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 7: IPv7");

            List<string> instructions = File.ReadLines("InputFiles/inputday7.txt").ToList();

            int countSupportingTLS = instructions
               .Select(i => i.Split('[', ']'))
               .Select(i => new List<IEnumerable<bool>>
               {
                   i.Where((c, a) => a % 2 == 0).Select(a => HasABBA(a)),//outside brackets
                   i.Where((c, a) => a % 2 != 0).Select(a => HasABBA(a)) //inside brackets
               }).Count(i => i[0].Any(a => a) && i[1].All(a => !a));
            
            Console.WriteLine("Amount of adresses supporting TLS: " + countSupportingTLS);
        }

        public void RunAdvanced()
        {
            Console.WriteLine("Welcome to Day 7: IPv7");

            List<string> instructions = File.ReadLines("InputFiles/inputday7.txt").ToList();

            int countSupportingSSL = instructions
               .Select(i => i.Split('[', ']'))
               .Select(i => new List<IEnumerable<string>>
               {
                   i.Where((c, a) => a % 2 == 0)
                   .SelectMany(a => GetABA(a))
                   .Select(aba => ConvertABAToBAB(aba)),
                   i.Where((c, a) => a % 2 != 0)
               }).Count(i => ContainsBAB(i[0], i[1]));

            Console.WriteLine("Amount of adresses supporting SSL: " + countSupportingSSL);
        }

        private bool HasABBA(string toCheck)
        {
            for (int i = 0; i < toCheck.Length - 3; i++)
            {
                if (toCheck[i] == toCheck[i + 3] && toCheck[i + 1] == toCheck[i + 2] && toCheck[i] != toCheck[i + 1])
                {
                    return true;
                }
            }
            return false;
        }

        private string ConvertABAToBAB(string aba)
        {
            return string.Join("", aba[1], aba[0], aba[1]);
        }

        private bool ContainsBAB(IEnumerable<string> abaList, IEnumerable<string> hypernetSequences)
        {
            foreach (string hypernetSequence in hypernetSequences)
            {
                if (abaList.Any(hypernetSequence.Contains))
                {
                    return true;
                }
            }
            return false;
        }

        private List<string> GetABA(string supernetSequence)
        {
            List<string> abaList = new List<string>();
            for (int i = 0; i < supernetSequence.Length - 2; i++)
            {
                if (supernetSequence[i] == supernetSequence[i + 2] && supernetSequence[i] != supernetSequence[i + 1])
                {
                    abaList.Add(string.Join("", supernetSequence[i], supernetSequence[i + 1], supernetSequence[i + 2]));
                }
            }
            return abaList;
        }
    }
}

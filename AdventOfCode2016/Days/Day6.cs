using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day6 : IDay
    {
        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 6: Decrypting signals");

            List<string> instructions = File.ReadLines("InputFiles/inputday6.txt").ToList();

            int positions = instructions[0].Length;

            string word = "";

            for (int i = 0; i < positions; i++)
            {
                List<string> letters = instructions.Select(x => x[i].ToString()).ToList();

                List<string> distinctLetters = letters.Distinct().ToList();

                List<KeyValuePair<string, int>> letterCounts = new List<KeyValuePair<string, int>>();

                foreach (string letter in distinctLetters)
                {
                    letterCounts.Add(new KeyValuePair<string, int>(letter.ToString(), letters.Count(l => l == letter)));
                }

                letterCounts = letterCounts.OrderByDescending(kv => kv.Value).ToList();

                word += letterCounts.First().Key;
            }

            Console.WriteLine("The signal is: " + word);
        }

        public void RunAdvanced()
        {
            Console.WriteLine("Welcome to Day 6: Decrypting signals again");

            List<string> instructions = File.ReadLines("InputFiles/inputday6.txt").ToList();

            int positions = instructions[0].Length;

            string word = "";

            for (int i = 0; i < positions; i++)
            {
                List<string> letters = instructions.Select(x => x[i].ToString()).ToList();

                List<string> distinctLetters = letters.Distinct().ToList();

                List<KeyValuePair<string, int>> letterCounts = new List<KeyValuePair<string, int>>();

                foreach (string letter in distinctLetters)
                {
                    letterCounts.Add(new KeyValuePair<string, int>(letter.ToString(), letters.Count(l => l == letter)));
                }

                letterCounts = letterCounts.OrderByDescending(kv => kv.Value).ToList();

                word += letterCounts.Last().Key;
            }

            Console.WriteLine("The signal is: " + word);
        }

    }
}

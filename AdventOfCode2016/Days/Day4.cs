using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day4 : IDay
    {
        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 4: Finding rooms");

            List<string> roomKeys = File.ReadLines("InputFiles/inputday4.txt").ToList();

            int idTotal = 0;

            foreach (string roomKey in roomKeys)
            {
                int lastDash = roomKey.LastIndexOf('-');

                string identifier = roomKey.Substring(0, lastDash);
                string checkSum = roomKey.Substring(lastDash + 1);

                identifier = identifier.Replace("-", "");

                List<char> usedLetters = identifier.Distinct().ToList();

                List<KeyValuePair<string, int>> letterCounts = new List<KeyValuePair<string, int>>();

                foreach (char letter in usedLetters)
                {
                    int amount = identifier.Count(l => l == letter);

                    letterCounts.Add(new KeyValuePair<string, int>(letter.ToString(), amount));
                }

                letterCounts = letterCounts.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).ToList();

                string checkString = "";

                for(int i = 0; i< 5; i++)
                {
                    checkString += letterCounts[i].Key;
                }

                int checkSumSplitIndex = checkSum.IndexOf('[');

                int sectionID = Convert.ToInt32(checkSum.Substring(0, checkSumSplitIndex));
                string checkValue = checkSum.Substring(checkSumSplitIndex + 1);

                checkValue = checkValue.Replace("]", "");

                if (checkValue.Equals(checkString))
                {
                    idTotal += sectionID;
                }

            }

            Console.WriteLine("The total of all the IDs is: " + idTotal);
        }

        public void RunAdvanced()
        {
            Console.WriteLine("Welcome to Day 4: Getting the North Pole");

            List<string> roomKeys = File.ReadLines("InputFiles/inputday4.txt").ToList();

            int northPoleID = 0;

            foreach (string roomKey in roomKeys)
            {
                int lastDash = roomKey.LastIndexOf('-');

                char[] identifier = roomKey.Substring(0, lastDash).ToCharArray();
                string checkSum = roomKey.Substring(lastDash + 1);

                int checkSumSplitIndex = checkSum.IndexOf('[');

                int sectionID = Convert.ToInt32(checkSum.Substring(0, checkSumSplitIndex));

                for(var i = 0; i < identifier.Length; i++)
                {
                    for(var j = 0; j < sectionID%26; j++)
                    {
                        identifier[i] = identifier[i] == 'z' ? 'a' : (char)(identifier[i] + 1);
                    }
                }

                string updatedIdentifier = new string(identifier);

                if (updatedIdentifier.Contains("northpole"))
                {
                    Console.WriteLine(updatedIdentifier);
                    northPoleID = sectionID;
                    break;
                }
            }

            Console.WriteLine("The North Pole is in: " + northPoleID);
        }
    }
}

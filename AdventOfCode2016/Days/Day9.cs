using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day9 : IDay
    {
        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 9: File decryption");

            List<string> instructions = File.ReadLines("InputFiles/inputday9.txt").ToList();

            List<string> decryptions = new List<string>();

            long decompressedlength = 0;

            foreach (string instruction in instructions)
            {
                /*int counter = 0;

                string usedInstruction = instruction;

                while (counter < usedInstruction.Length)
                {
                    if (usedInstruction[counter] == '(')
                    {
                        int closeParens = usedInstruction.IndexOf(')') - counter + 1;

                        if (closeParens > 0)
                        {
                            string part = usedInstruction.Substring(counter, usedInstruction.IndexOf(')') - counter + 1);

                            string strippedPart = part.Remove(0, 1);

                            strippedPart = strippedPart.Remove(strippedPart.Length - 1, 1);

                            string[] calculation = strippedPart.Split('x');

                            int numberOfCharacters = Convert.ToInt32(calculation[0]);
                            int numberOfTimes = Convert.ToInt32(calculation[1]);

                            int partLength = part.Length;

                            string stringToCopy = usedInstruction.Substring(counter + partLength, numberOfCharacters);

                            string pasteString = string.Empty;

                            for (int j = 0; j < numberOfTimes; j++)
                            {
                                pasteString += stringToCopy;
                            }

                            usedInstruction = usedInstruction.Remove(counter, partLength + numberOfCharacters);

                            usedInstruction = usedInstruction.Insert(counter, pasteString);

                            //skip the pasted bit
                            counter += pasteString.Length - 1;
                        }
                    }

                    counter++;
                }

                decryptions.Add(usedInstruction);

                decompressedlength += usedInstruction.Length;*/

                decompressedlength = Decompress(instruction, false);
            }

            Console.WriteLine("Decompressed length: " + decompressedlength);
            
        }

        public void RunAdvanced()
        {
            Console.WriteLine("Welcome to Day 9: File decryption");

            List<string> instructions = File.ReadLines("InputFiles/inputday9.txt").ToList();

            List<string> decryptions = new List<string>();

            long decompressedlength = 0;

            foreach (string instruction in instructions)
            {
                decompressedlength = Decompress(instruction, true);
            }

            Console.WriteLine("Decompressed length: " + decompressedlength);
        }

        private long Decompress(string input, bool recurse)
        {
            long length = 0;

            for (var index = 0; index < input.Length; ++index)
            {
                if (input[index].Equals('('))
                {
                    var formula = input.Substring(index + 1, (input.IndexOf(')', index) - 1) - index).Split('x');
                    var numberOfCharacters = Convert.ToInt32(formula[0]);
                    var times = Convert.ToInt32(formula[1]);
                    var endOfString = input.IndexOf(')', index) + numberOfCharacters;

                    if (input.Substring(input.IndexOf(')', index) + 1, numberOfCharacters).Contains("(") && recurse)
                    {
                        length += (times * Decompress(input.Substring(input.IndexOf(')', index) + 1, numberOfCharacters), true));
                    }
                    else
                    {
                        length += (numberOfCharacters * times);
                    }

                    index = endOfString;
                }
                else
                {
                    ++length;
                }
            }

            return length;
        }
    }
}

using AdventOfCode2016.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2016!");

            bool running = true;
            
            //TODO: generic ways of doing input
            while (running)
            {
                Console.WriteLine("Please select your day to play or type (exit) to stop");

                string input;
                bool inputValid = false;

                bool canConvertInput;
                int day;
                
                while (!inputValid)
                {
                    input = Console.ReadLine();

                    if (!string.IsNullOrEmpty(input))
                    {
                        canConvertInput = int.TryParse(input, out day);

                        if (canConvertInput)
                        {
                            IDay thisDay = null;

                            switch (day)
                            {
                                case 1:
                                    inputValid = true;
                                    thisDay = new Day1();
                                    break;
                                default:
                                    Console.WriteLine("I don't know that day, try again");
                                    break;
                            }

                            if (thisDay != null)
                            {
                                bool isAdvancedInputCorrect = false;
                                string advancedInput = null;

                                while (!isAdvancedInputCorrect)
                                {
                                    Console.WriteLine("The simple (S) or the advanced (A) version?");

                                    advancedInput = Console.ReadLine();

                                    if (advancedInput.ToLower().Equals("simple") || advancedInput.ToUpper().Equals("S"))
                                    {
                                        isAdvancedInputCorrect = true;
                                        thisDay.RunSimple();
                                    }
                                    else if (advancedInput.ToLower().Equals("advanced") || advancedInput.ToUpper().Equals("A"))
                                    {
                                        isAdvancedInputCorrect = true;
                                        thisDay.RunAdvanced();
                                    }
                                    else
                                    {
                                        isAdvancedInputCorrect = false;
                                        Console.WriteLine("That's not what I asked!");
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (input.ToLower().Equals("exit"))
                            {
                                running = false;
                                inputValid = true;
                            }
                            else
                            {
                                Console.WriteLine("Please input the day number or (exit)");
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Press enter to shut down");
            Console.ReadLine();
        }
    }
}

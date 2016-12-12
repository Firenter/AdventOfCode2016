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
                                case 2:
                                    inputValid = true;
                                    thisDay = new Day2();
                                    break;
                                case 3:
                                    inputValid = true;
                                    thisDay = new Day3();
                                    break;
                                case 4:
                                    inputValid = true;
                                    thisDay = new Day4();
                                    break;
                                case 5:
                                    inputValid = true;
                                    thisDay = new Day5();
                                    break;
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                case 22:
                                case 23:
                                case 24:
                                case 25:
                                default:
                                    inputValid = false;
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

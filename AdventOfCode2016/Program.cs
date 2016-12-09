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

            string input;
            bool inputValid = false;

            bool canConvertInput;
            int day;

            //TODO: generic ways of doing things

            while (!inputValid)
            {
                Console.WriteLine("Please select your day to play");

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

                        if(thisDay != null)
                        {
                            //TODO: ask if simple or advanced
                            thisDay.RunSimple();
                        }
                    }
                }
            }

            Console.WriteLine("Shutting down");
            Console.ReadLine();
        }
    }
}

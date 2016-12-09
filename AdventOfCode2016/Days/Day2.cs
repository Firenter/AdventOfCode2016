using AdventOfCode2016.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day2 : IDay
    {
        private readonly int[,] pad = new int[,] {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        private readonly string[,] pad2 = new string[,] {
            { "",   "", "1",  "", "" },
            { "",  "2", "3", "4", "" },
            { "5", "6", "7", "8", "9" },
            { "",  "A", "B", "C", "" },
            { "",   "", "D",  "", "" }
        };

        private Coordinate finger = new Coordinate();

        private string code = "";

        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 2: Taking a piss");

            finger.X = 1;
            finger.Y = 1;

            List<string> instructions = File.ReadLines("InputFiles/inputday2.txt").ToList();

            foreach (string instruction in instructions)
            {
                //string movedplaces = pad[finger.Y, finger.X].ToString(); ;
                foreach (char dir in instruction)
                {
                    MoveFinger(dir);
                    //movedplaces += pad[finger.Y, finger.X].ToString();
                }
                //Console.WriteLine("Moved like this: " + movedplaces);
                code += pad[finger.Y, finger.X].ToString();
            }

            Console.WriteLine("The code for opening the toilet is " + code);
        }

        public void RunAdvanced()
        {
            Console.WriteLine("Welcome to Day 2: Seriously, more doors?");

            finger.X = 0;
            finger.Y = 2;

            List<string> instructions = File.ReadLines("InputFiles/inputday2.txt").ToList();

            foreach (string instruction in instructions)
            {
                //string movedplaces = pad2[finger.Y, finger.X].ToString(); ;
                foreach (char dir in instruction)
                {
                    MoveFinger2(dir);
                    //movedplaces += pad2[finger.Y, finger.X].ToString();
                }
                //Console.WriteLine("Moved like this: " + movedplaces);
                code += pad2[finger.Y, finger.X].ToString();
            }

            Console.WriteLine("The code for opening the toilet is " + code);
        }

        private void MoveFinger(char direction)
        {
            switch (direction)
            {
                case 'U':
                    finger.Y -= 1;
                    break;
                case 'L':
                    finger.X -= 1;
                    break;
                case 'R':
                    finger.X += 1;
                    break;
                case 'D':
                    finger.Y += 1;
                    break;
                default:
                    Console.WriteLine("That's not a real direction!");
                    break;
            }

            if (finger.X > 2)
            {
                finger.X = 2;
            }

            if (finger.X < 0)
            {
                finger.X = 0;
            }

            if (finger.Y > 2)
            {
                finger.Y = 2;
            }

            if (finger.Y < 0)
            {
                finger.Y = 0;
            }
        }

        private void MoveFinger2(char direction)
        {
            switch (direction)
            {
                case 'U':
                    if (IsLegalMove(finger.X, finger.Y - 1))
                        finger.Y -= 1;
                    break;
                case 'L':
                    if (IsLegalMove(finger.X - 1, finger.Y))
                        finger.X -= 1;
                    break;
                case 'R':
                    if (IsLegalMove(finger.X + 1, finger.Y))
                        finger.X += 1;
                    break;
                case 'D':
                    if (IsLegalMove(finger.X, finger.Y + 1))
                        finger.Y += 1;
                    break;
                default:
                    Console.WriteLine("That's not a real direction!");
                    break;
            }

            if (finger.X > 4)
            {
                finger.X = 4;
            }

            if (finger.X < 0)
            {
                finger.X = 0;
            }

            if (finger.Y > 4)
            {
                finger.Y = 4;
            }

            if (finger.Y < 0)
            {
                finger.Y = 0;
            }
        }

        private bool IsLegalMove(int x, int y)
        {
            try
            {
                string buttonValue = pad2[y, x];

                return !string.IsNullOrEmpty(buttonValue);
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
    }
}

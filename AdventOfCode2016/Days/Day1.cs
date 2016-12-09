using AdventOfCode2016.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day1 : IDay
    {
        private enum Direction
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        private Direction currentDirection;
        private Coordinate directionStep;
        private Coordinate ourLocation;

        public Day1()
        {
            directionStep = new Coordinate();
            ourLocation = new Coordinate();
        }

        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 1: Navigating the city");
            Console.WriteLine("Our instructions are these: ");
            
            string input = File.ReadAllText("InputFiles/inputday1.txt");

            Console.WriteLine(input);

            //We start facing North
            currentDirection = Direction.North;
            SetDirectionStep();
            
            string[] steps = input.Split(',');

            foreach (string step in steps)
            {
                string thisStep = step.Trim();

                string turnDirection = thisStep[0].ToString().ToUpper();
                string stepDistance = thisStep.Substring(1);
                int distance = int.Parse(stepDistance);

                if (turnDirection.Equals("R"))
                {
                    SwitchDirectionRight();
                }
                else if (turnDirection.Equals("L"))
                {
                    SwitchDirectionLeft();
                }

                SetDirectionStep();

                ourLocation.X += directionStep.X * distance;
                ourLocation.Y += directionStep.Y * distance;
            }

            Console.WriteLine("Our final location is X:" + ourLocation.X + ", Y:" + ourLocation.Y);

            int finalDistance = Math.Abs(ourLocation.X) + Math.Abs(ourLocation.Y);

            Console.WriteLine("Which is " + finalDistance + " blocks away from where we started!");
        }

        public void RunAdvanced()
        {
            Console.WriteLine("Not implemented yet, sorry!");
        }

        private void SetDirectionStep()
        {
            switch (currentDirection)
            {
                case Direction.North:
                    directionStep.X = 0;
                    directionStep.Y = 1;
                    break;
                case Direction.East:
                    directionStep.X = 1;
                    directionStep.Y = 0;
                    break;
                case Direction.South:
                    directionStep.X = 0;
                    directionStep.Y = -1;
                    break;
                case Direction.West:
                    directionStep.X = -1;
                    directionStep.Y = 0;
                    break;
            }
        }

        private void SwitchDirectionRight()
        {
            switch (currentDirection)
            {
                case Direction.North:
                    currentDirection = Direction.East;
                    break;
                case Direction.East:
                    currentDirection = Direction.South;
                    break;
                case Direction.South:
                    currentDirection = Direction.West;
                    break;
                case Direction.West:
                    currentDirection = Direction.North;
                    break;
                default:
                    Console.WriteLine("Hold on, that direction doesn't exist!");
                    break;
            }
        }

        private void SwitchDirectionLeft()
        {
            switch (currentDirection)
            {
                case Direction.North:
                    currentDirection = Direction.West;
                    break;
                case Direction.West:
                    currentDirection = Direction.South;
                    break;
                case Direction.South:
                    currentDirection = Direction.East;
                    break;
                case Direction.East:
                    currentDirection = Direction.North;
                    break;
                default:
                    Console.WriteLine("Hold on, that direction doesn't exist!");
                    break;
            }
        }
    }
}

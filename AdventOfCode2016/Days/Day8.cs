using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    class Day8 : IDay
    {
        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 8: Two factor authentication");

            List<string> instructions = File.ReadLines("InputFiles/inputday8.txt").ToList();

            int screenwidth = 50;
            int screenheight = 6;

            int[,] screen = new int[screenwidth, screenheight];

            screen.Initialize();
            
            foreach (string instruction in instructions)
            {
                if (isRectInstruction(instruction))
                {
                    //Fill a rectangle starting from 0,0
                    string[] stringData = instruction.Substring(5).Split(new char[] { 'x' });

                    int width = Convert.ToInt32(stringData[0]);
                    int height = Convert.ToInt32(stringData[1]);

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            screen[i, j] = 1;
                        }
                    }
                }

                if (isShiftXInstruction(instruction))
                {
                    string[] stringData = instruction.Substring(instruction.IndexOf("y=") + 2).Split(new string[] { "by" }, StringSplitOptions.None);

                    int rowNumber = Convert.ToInt32(stringData[0]);
                    int distance = Convert.ToInt32(stringData[1]);

                    int[] rowcopy = new int[screenwidth];

                    rowcopy.Initialize();

                    while (distance > 0)
                    {
                        for (int i = 0; i < screenwidth; i++)
                        {
                            int nextIndex = i + 1;

                            if (nextIndex == screenwidth)
                            {
                                nextIndex = 0;
                            }

                            rowcopy[nextIndex] = screen[i, rowNumber];
                        }

                        for (int j = 0; j < screenwidth; j++)
                        {
                            screen[j, rowNumber] = rowcopy[j];
                        }

                        rowcopy.Initialize();
                        distance--;
                    }
                }

                if (isShiftYInstruction(instruction))
                {
                    string[] stringData = instruction.Substring(instruction.IndexOf("x=") + 2).Split(new string[] { "by" }, StringSplitOptions.None);

                    int columnNumber = Convert.ToInt32(stringData[0]);
                    int distance = Convert.ToInt32(stringData[1]);

                    int[] columncopy = new int[screenwidth];

                    columncopy.Initialize();

                    while (distance > 0)
                    {
                        for (int i = 0; i < screenheight; i++)
                        {
                            int nextIndex = i + 1;

                            if (nextIndex == screenheight)
                            {
                                nextIndex = 0;
                            }

                            columncopy[nextIndex] = screen[columnNumber, i];
                        }

                        for (int j = 0; j < screenheight; j++)
                        {
                            screen[columnNumber, j] = columncopy[j];
                        }

                        columncopy.Initialize();
                        distance--;
                    }
                }
            }

            drawScreen(screen);

            int pixelCounter = 0;

            for (int w = 0; w < screenwidth; w++)
            {
                for (int h = 0; h < screenheight; h++)
                {
                    if (screen[w, h] == 1)
                        pixelCounter++;
                }
            }

            Console.WriteLine("Pixels lit: " + pixelCounter);
        }

        public void RunAdvanced()
        {
            Console.WriteLine("Welcome to Day 8: Two factor authentication");

            Console.WriteLine("Go to Simple and check what the output screen says!");
        }

        private void drawScreen(int[,] screen)
        {
            int screenwidth = screen.GetLength(0);
            int screenheight = screen.GetLength(1);

            for (int i = 0; i < screenheight; i++)
            {
                for (int j = 0; j < screenwidth - 1; j++)
                {
                    Console.Write(screen[j, i] == 1 ? "#" : ".");
                }

                Console.WriteLine(screen[screenwidth - 1, i] == 1 ? "#" : ".");
            }

            Console.WriteLine();
        }

        private bool isRectInstruction(string instruction)
        {
            return instruction.StartsWith("rect");
        }

        private bool isShiftXInstruction(string instruction)
        {
            return instruction.StartsWith("rotate row");
        }

        private bool isShiftYInstruction(string instruction)
        {
            return instruction.StartsWith("rotate column");
        }

    }
}

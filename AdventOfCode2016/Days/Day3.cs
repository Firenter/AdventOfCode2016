using AdventOfCode2016.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day3 : IDay
    {
        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 3: Checking triangles");

            List<string> triangles = File.ReadLines("InputFiles/inputday3.txt").ToList();

            int correctTriangles = 0;

            foreach (string triangle in triangles)
            {
                string[] lines = triangle.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                List<int> linesList = lines.Select(p => Convert.ToInt32(p)).ToList();

                int total = linesList.Sum();

                int highest = linesList.Max();

                total -= highest;

                if (total > highest)
                {
                    //it's possible
                    correctTriangles++;
                }
            }

            Console.WriteLine("Possible triangles: " + correctTriangles);
        }

        public void RunAdvanced()
        {
            Console.WriteLine("Welcome to Day 3: Checking triangles vertically");

            List<string> triangles = File.ReadLines("InputFiles/inputday3.txt").ToList();

            List<Triangle> triangleList = new List<Triangle>();

            int correctTriangles = 0;

            int counter = 1;

            Triangle triangle1 = null;
            Triangle triangle2 = null;
            Triangle triangle3 = null;

            foreach (string triangle in triangles)
            {
                string[] lines = triangle.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                List<int> linesList = lines.Select(p => Convert.ToInt32(p)).ToList();

                if(counter == 1)
                {
                    triangle1 = new Triangle() { side1 = linesList[0] };
                    triangle2 = new Triangle() { side1 = linesList[1] };
                    triangle3 = new Triangle() { side1 = linesList[2] };

                    triangleList.Add(triangle1);
                    triangleList.Add(triangle2);
                    triangleList.Add(triangle3);
                }else if (counter == 2)
                {
                    triangle1.side2 = linesList[0];
                    triangle2.side2 = linesList[1];
                    triangle3.side2 = linesList[2];
                }else if(counter == 3)
                {
                    triangle1.side3 = linesList[0];
                    triangle2.side3 = linesList[1];
                    triangle3.side3 = linesList[2];

                    counter = 0;
                }

                counter++;
            }

            foreach (Triangle t in triangleList)
            {
                List<int> linesList = new List<int>();
                linesList.Add(t.side1);
                linesList.Add(t.side2);
                linesList.Add(t.side3);

                int total = linesList.Sum();

                int highest = linesList.Max();

                total -= highest;

                if (total > highest)
                {
                    //it's possible
                    correctTriangles++;
                }
            }
            
            Console.WriteLine("Possible triangles: " + correctTriangles);
        }

    }
}

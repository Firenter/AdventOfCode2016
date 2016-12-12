using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day5 : IDay
    {
        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 5: Hacking doors");

            Console.WriteLine("What is the door ID?");

            string input = Console.ReadLine();

            MD5 crypto = MD5.Create();

            string password = "";

            int counter = 0;

            bool running = true;

            while (running)
            {

                byte[] hash = crypto.ComputeHash(Encoding.UTF8.GetBytes(input + counter));

                StringBuilder sb = new StringBuilder();

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                string hashString = sb.ToString();

                if (hashString.StartsWith("00000"))
                {
                    password += hashString[5];
                }

                if (password.Length == 8)
                {
                    running = false;
                }

                counter++;
            }

            Console.WriteLine("The password is: " + password);
        }

        public void RunAdvanced()
        {
            Console.WriteLine("Welcome to Day 5: Hacking the second door");

            Console.WriteLine("What is the door ID?");

            string input = Console.ReadLine();

            MD5 crypto = MD5.Create();

            List<KeyValuePair<int, string>> passwordParts = new List<KeyValuePair<int, string>>();

            int counter = 0;

            bool running = true;

            while (running)
            {

                byte[] hash = crypto.ComputeHash(Encoding.UTF8.GetBytes(input + counter));

                StringBuilder sb = new StringBuilder();

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                string hashString = sb.ToString();

                if (hashString.StartsWith("00000"))
                {
                    int position = 8;
                    if (int.TryParse(hashString[5].ToString(), out position))
                    {
                        if (position <= 7 && passwordParts.Count(kv => kv.Key == position) == 0)
                        {
                            passwordParts.Add(new KeyValuePair<int, string>(position, hashString[6].ToString()));
                        }
                    }
                }

                if (passwordParts.Count == 8)
                {
                    running = false;
                }

                counter++;
            }

            passwordParts = passwordParts.OrderBy(kv => kv.Key).ToList();

            StringBuilder pwsb = new StringBuilder();

            foreach (KeyValuePair<int, string> pwp in passwordParts)
            {
                pwsb.Append(pwp.Value);
            }

            Console.WriteLine("The password is: " + pwsb.ToString());
        }
    }
}

using AdventOfCode2016.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day10 : IDay
    {
        private List<ChipBot> swarm;
        private List<OutputBin> outputs;

        private List<KeyValuePair<int, string>> commandBacklog;

        ChipBot currentBot = null;

        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 10: Flying robots");

            List<string> instructions = File.ReadLines("InputFiles/inputday10.txt").ToList();

            swarm = new List<ChipBot>();
            outputs = new List<OutputBin>();

            commandBacklog = new List<KeyValuePair<int, string>>();

            foreach (string instruction in instructions)
            {
                int botNumber = GetIdentifierFromInstruction(instruction);

                if (botNumber != -1)
                {
                    currentBot = GetBot(botNumber);

                    string[] commands = instruction.Split(new string[] { " and " }, StringSplitOptions.None);

                    foreach (string command in commands)
                    {
                        ChipBotCommands commandType = GetCommandType(command);

                        ExecuteCommand(commandType, command);
                    }
                }
            }

            int commandCounter = 0;

            //some commands might not have run, run them again
            while (commandCounter < commandBacklog.Count)
            {
                KeyValuePair<int, string> com = commandBacklog[commandCounter];

                currentBot = GetBot(com.Key);

                ChipBotCommands cmdType = GetCommandType(com.Value);

                ExecuteCommand(cmdType, com.Value);

                commandCounter++;
            }

            /*List<ChipBot> goodBots = swarm.Where(b => b.GetHighValue() == 61).ToList();
            List<ChipBot> goodBots2 = swarm.Where(b => b.GetLowValue() == 17).ToList();*/
            
            ChipBot ourBot = swarm.Where(b => (b.GetHighValue() == 61 && b.GetLowValue() == 17)).FirstOrDefault();

            if (ourBot != null)
                Console.WriteLine("The bot we need is " + ourBot.identity);

        }

        public void RunAdvanced()
        {
            throw new NotImplementedException();
        }

        private void ExecuteCommand(ChipBotCommands commandType, string command)
        {
            switch (commandType)
            {
                case ChipBotCommands.Pickup:
                    command = command.Remove(0, 5);
                    string[] pickupBits = command.Split(new string[] { " goes to bot " }, StringSplitOptions.None);

                    int value = Convert.ToInt32(pickupBits[0]);
                    int botNumber = Convert.ToInt32(pickupBits[1]);

                    currentBot.AddValue(value);
                    break;
                case ChipBotCommands.DropOff:
                    if (command.StartsWith("bot"))
                        command = command.Remove(0, command.IndexOf("gives") + 5);

                    string[] dropoffbits = command.Split(new string[] { " to output " }, StringSplitOptions.None);

                    string what = dropoffbits[0];
                    int outputId = Convert.ToInt32(dropoffbits[1]);

                    OutputBin outputBin = GetOutput(outputId);

                    int valueToDrop = what.Equals("high") ? currentBot.GetHighValue() : currentBot.GetLowValue();

                    if (valueToDrop != -1)
                        outputBin.values.Add(valueToDrop);
                    else
                        commandBacklog.Add(new KeyValuePair<int, string>(currentBot.identity, command));

                    break;
                case ChipBotCommands.HandOff:
                    if (command.StartsWith("bot"))
                        command = command.Remove(0, command.IndexOf("gives") + 5);

                    string[] handoffbits = command.Split(new string[] { " to bot " }, StringSplitOptions.None);

                    string highorlow = handoffbits[0];
                    int otherBotId = Convert.ToInt32(handoffbits[1]);

                    int valueToHandOff = highorlow.Equals("high") ? currentBot.GetHighValue() : currentBot.GetLowValue();

                    ChipBot otherBot = GetBot(otherBotId);

                    if (valueToHandOff != -1)
                        otherBot.AddValue(valueToHandOff);
                    else
                        commandBacklog.Add(new KeyValuePair<int, string>(currentBot.identity, command));

                    break;
                default:
                    Console.WriteLine("How the hell did you force this?");
                    break;
            }
        }

        private int GetIdentifierFromInstruction(string instruction)
        {
            int identifier = -1;

            string[] parts = instruction.Split(new string[] { "bot" }, StringSplitOptions.None);

            if (instruction.StartsWith("bot"))
                identifier = Convert.ToInt32(parts[1].Substring(0, parts[1].IndexOf("gives")));
            else
                identifier = Convert.ToInt32(parts[1]);

            return identifier;
        }

        private ChipBot GetBot(int identity)
        {
            ChipBot usedBot = swarm.Where(b => b.identity == identity).FirstOrDefault();

            if (usedBot == null)
            {
                usedBot = new ChipBot() { identity = identity };

                swarm.Add(usedBot);
            }

            return usedBot;
        }

        private OutputBin GetOutput(int identity)
        {
            OutputBin usedBin = outputs.Where(o => o.identifier == identity).FirstOrDefault();

            if (usedBin == null)
            {
                usedBin = new OutputBin() { identifier = identity };

                outputs.Add(usedBin);
            }

            return usedBin;
        }

        private ChipBotCommands GetCommandType(string command)
        {
            ChipBotCommands output = ChipBotCommands.Pickup;

            if (command.StartsWith("value"))
            {
                output = ChipBotCommands.Pickup;
            }
            else if (command.Contains("to output"))
            {
                output = ChipBotCommands.DropOff;
            }
            else if (command.Contains("to bot"))
            {
                output = ChipBotCommands.HandOff;
            }

            return output;
        }
    }
}

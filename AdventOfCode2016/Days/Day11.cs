using AdventOfCode2016.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016.Days
{
    public class Day11 : IDay
    {
        /**
         * LOGIC:
         * 
         * Always pick two items when going up. If possible, pick items that form a pair (chip + generator of same type). Otherwise just pick any two that can be moved without problems.
         * Always pick a single item when going down. If possible, pick one that has its counterpart on the floor below.
         * Don't go down if there are no items left
         */


        List<IRTG> firstFloor;
        List<IRTG> secondFloor;
        List<IRTG> thirdFloor;
        List<IRTG> fourthFloor;

        IRTG[] itemsToMove = new IRTG[2];

        int elevatorLevel = 1;

        public void RunSimple()
        {
            Console.WriteLine("Welcome to Day 11: Elevator puzzle");

            GenerateBaseLine();

            int itemCount = firstFloor.Count() + secondFloor.Count() + thirdFloor.Count() + fourthFloor.Count();

            bool isDone = false;

            int stepcounter = 0;

            while (!isDone)
            {
                //GOING UP?
                IRTG[] items = GetFloorItems(elevatorLevel, elevatorLevel + 1);

                //get combo floor items
                //if no combo check floor above for moves that don't break it
                //if still nothing, check below us for anything to bring up

                //if we can't go up, find out why and fix it by going down

                //check if everything is on the fourth floor
                if (fourthFloor.Count() == itemCount)
                {
                    //we're done!
                    isDone = true;
                    Console.WriteLine("It took us " + stepcounter + " steps to do!");
                }
            }


        }

        public void RunAdvanced()
        {
            throw new NotImplementedException();
        }

        private bool IsValidMove(IRTG[] items, int toFloor, int fromFloor)
        {
            bool isValid = false;

            List<IRTG> destinationFloor = GetFloor(toFloor);
            List<IRTG> startFloor = GetFloor(fromFloor);

            //check if items will break together
            if (DoItemsBreak(items))
            {
                //check if items will break on the next floor
                if (items.Count() == 0)
                {
                    //if we're holding nothing we can't move
                    isValid = false;
                }
                else if (items.Count() == 1)
                {
                    //if we're holding 1 thing see if it breaks anything or itself on this floor
                    if (!DoesSingleItemBreakFloor(items[0], destinationFloor))
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else if (IsChipReactorCombo(items))
                {
                    //if we're holding a chip and reactor we can move them with no worries, because they don't break together
                    isValid = true;
                }
                else if (IsTwoChips(items))
                {
                    //if we're holding 2 chips check for reactors on floor
                    if (FloorContainsReactor(destinationFloor))
                    {
                        //if there are reactors check if they break the chips
                        if (!DoesSingleItemBreakFloor(items[0], destinationFloor) && !DoesSingleItemBreakFloor(items[1], destinationFloor))
                        {
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
                        //if there are no reactors we're safe
                        isValid = true;
                    }
                }
                else if (!IsTwoChips(items))
                {
                    //If we're holding 2 reactors check if they break the floor
                    if (!DoesSingleItemBreakFloor(items[0], destinationFloor) && !DoesSingleItemBreakFloor(items[1], destinationFloor))
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }

                //if we're good, let's test some more
                if (isValid)
                {
                    List<IRTG> testFloor = new List<IRTG>();

                    testFloor.AddRange(startFloor);

                    testFloor.Remove(items[0]);

                    if (items.Length > 1)
                    {
                        testFloor.Remove(items[1]);
                    }

                    //check if we're leaving a broken floor behind
                    if (!IsFloorBroken(testFloor))
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
            }

            return isValid;
        }

        private bool IsFloorBroken(List<IRTG> floor)
        {
            bool isValid = true;

            if (FloorContainsReactor(floor) && FloorContainsChip(floor))
            {
                //floor contains reactors and chips, possibly dangerous
                int numberOfElements = floor.Distinct().Count();

                if (floor.Count() == numberOfElements * 2)
                {
                    //all elements have matches
                    isValid = false;
                }
                else
                {
                    //cross off the pairs and then see if the rest break eachother
                    List<IRTG> remaining = floor.Where(item => floor.Count(x => x.element == item.element) < 2).ToList();

                    if (FloorContainsChip(remaining) && FloorContainsReactor(remaining))
                    {
                        //we got reactors and chips remaining, they will break each other
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
            }
            else
            {
                //only reactors or only chips is safe
                isValid = false;
            }

            return isValid;
        }

        private bool DoesSingleItemBreakFloor(IRTG item, List<IRTG> floor)
        {
            bool broken = false;

            if (item.GetType() == typeof(RTGChip))
            {
                if (FloorContainsReactor(floor))
                {
                    List<RTG> reactors = floor.Where(r => r.GetType() == typeof(RTG)).Select(x => (RTG)x).ToList();

                    //if any reactor has a matching element we're fine
                    if (reactors.Any(x => x.element == item.GetElement()))
                    {
                        broken = false;
                    }
                    else
                    {
                        broken = true;
                    }
                }
                else
                {
                    broken = false;
                }
            }
            else if (item.GetType() == typeof(RTG))
            {
                if (FloorContainsChip(floor))
                {
                    List<RTGChip> chips = floor.Where(r => r.GetType() == typeof(RTGChip)).Select(x => (RTGChip)x).ToList();

                    //if any chip has a matching element we're fine
                    if (chips.Any(x => x.element == item.GetElement()))
                    {
                        broken = false;
                    }
                    else
                    {
                        broken = true;
                    }
                }
                else
                {
                    broken = false;
                }
            }

            return !broken;
        }

        private bool FloorContainsChip(List<IRTG> floor)
        {
            return floor.Any(r => r.GetType() == typeof(RTGChip));
        }

        private bool FloorContainsReactor(List<IRTG> floor)
        {
            return floor.Any(r => r.GetType() == typeof(RTG));
        }

        private bool IsTwoChips(IRTG[] items)
        {
            if (items.Length > 1)
            {
                if (items[0].GetType() == typeof(RTGChip) && items[1].GetType() == typeof(RTGChip))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsChipReactorCombo(IRTG[] items)
        {
            if (items.Length > 1)
            {
                if ((items[0].GetType() == typeof(RTGChip) && items[1].GetType() == typeof(RTG)) || (items[1].GetType() == typeof(RTGChip) && items[0].GetType() == typeof(RTG)))
                {
                    return true;
                }
            }

            return false;
        }

        private bool DoItemsBreak(IRTG[] items)
        {
            if (items.Length > 1)
            {
                if (IsChipReactorCombo(items))
                {
                    //same element, won't break
                    if (items[0].GetElement() == items[1].GetElement())
                    {
                        return false;
                    }

                    //different element, will break
                    return true;
                }
            }

            //anything else won't break
            return false;
        }

        private IRTG[] GetFloorItems(int floorNumber, int destinationFloor)
        {
            List<IRTG> floor = GetFloor(floorNumber);

            IRTG[] items = null;

            //attempt to take a reactor chip combo up
            List<string> elements = floor.Select(x => x.element).Distinct().ToList();

            foreach (string element in elements)
            {
                var possibleitems = floor.Where(x => x.element == element);

                if (possibleitems.Count() > 1)
                {
                    items = possibleitems.ToArray();
                    break;
                }
            }

            if (items == null)
            {
                //if none available, take 2 things
                List<IRTG> remaining = floor.Where(item => floor.Count(x => x.element == item.element) < 2).ToList();

                IRTG firstItem = floor.FirstOrDefault();

                if (firstItem != null)
                {
                    Type itemType = firstItem.GetType();

                    if (remaining.Any(x => x.GetType() == itemType))
                    {
                        items = new IRTG[] { firstItem, remaining.Where(x => x.GetType() == itemType).First() };
                    }

                    if (items == null)
                    {
                        //if we can't take 1 thing
                        items = new IRTG[] { firstItem };
                    }
                }
            }
            return items;

        }

        private List<IRTG> GetFloor(int floorNumber)
        {
            switch (floorNumber)
            {
                case 1:
                    return firstFloor;
                case 2:
                    return secondFloor;
                case 3:
                    return thirdFloor;
                case 4:
                    return fourthFloor;
                default:
                    Console.WriteLine("Dafuq you doin mang?");
                    return null;
            }
        }

        private void GenerateBaseLine()
        {
            /**
             * First floor
             */
            //thulium generator
            firstFloor.Add(new RTG() { element = "T" });
            //thulium chip
            firstFloor.Add(new RTGChip() { element = "T" });
            //plutonium generator
            firstFloor.Add(new RTG() { element = "P" });
            //strontium generator
            firstFloor.Add(new RTG() { element = "S" });

            /**
             * Second floor
             */
            //plutonium chip
            secondFloor.Add(new RTGChip() { element = "P" });
            //strontium chip
            secondFloor.Add(new RTGChip() { element = "S" });

            /**
             * Third floor
             */
            //promethium generator
            thirdFloor.Add(new RTG() { element = "X" });
            //promethium chip
            thirdFloor.Add(new RTG() { element = "X" });
            //ruthenium generator
            thirdFloor.Add(new RTG() { element = "R" });
            //ruthenium chip
            thirdFloor.Add(new RTG() { element = "R" });

            /**
             * Fourth floor
             */
            //empty
        }

    }
}

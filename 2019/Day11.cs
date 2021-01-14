using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day11 : Solver
    {
        long[] intCodes;
        public Day11(string input) : base(input)
        {
            intCodes = input.Split(",").Select(x => long.Parse(x)).ToArray();
        }

        private class Computer
        {
            public class State
            {
                public bool waitingForInput = false;
                public bool halted = false;
                public long? output = null;
            }

            public class DynamicArray
            {
                long[] array;
                public DynamicArray(long[] array)
                {
                    this.array = array;
                }

                public long this[long i]
                {
                    get
                    {
                        Extend(i);
                        return array[i];
                    }
                    set
                    {
                        Extend(i);
                        array[i] = value;
                    }
                }

                private void Extend(long i)
                {
                    if (i >= array.Length)
                    {
                        long[] oldArray = array;
                        array = new long[i + 10000];
                        oldArray.CopyTo(array, 0);
                    }
                }
            }

            private long pointer = 0;
            private long relativeBase = 0;
            private readonly DynamicArray program;
            private readonly long[] modes = new long[4];
            public readonly State state = new State();

            public Computer(long[] program)
            {
                this.program = new DynamicArray((long[])program.Clone());
            }

            private void Step(long? input)
            {
                var memVal = program[pointer];
                long opCode = memVal % 100;
                memVal /= 100;
                for (int i = 1; i < 4; i++)
                {
                    modes[i] = memVal % 10;
                    memVal /= 10;
                }

                state.waitingForInput = false;
                state.output = null;
                switch (opCode)
                {
                    case 99:
                        state.halted = true;
                        break;
                    case 1:
                        Set(3, Get(1) + Get(2));
                        pointer += 4;
                        break;
                    case 2:
                        Set(3, Get(1) * Get(2));
                        pointer += 4;
                        break;
                    case 3:
                        if (input.HasValue)
                        {
                            Set(1, input.Value);
                            pointer += 2;
                        }
                        else
                            state.waitingForInput = true;
                        break;
                    case 4:
                        state.output = Get(1);
                        pointer += 2;
                        if (program[pointer] % 100 == 99)
                            state.halted = true;
                        break;
                    case 5:
                        pointer = Get(1) != 0 ? Get(2) : pointer + 3;
                        break;
                    case 6:
                        pointer = Get(1) == 0 ? Get(2) : pointer + 3;
                        break;
                    case 7:
                        Set(3, Get(1) < Get(2) ? 1 : 0);
                        pointer += 4;
                        break;
                    case 8:
                        Set(3, Get(1) == Get(2) ? 1 : 0);
                        pointer += 4;
                        break;
                    case 9:
                        relativeBase += Get(1);
                        pointer += 2;
                        break;
                    default:
                        throw new Exception("Accessed invalid memory");
                }
            }

            public void Run(long? input = null)
            {
                while (true)
                {
                    Step(input);
                    if (input.HasValue)
                        input = null;
                    if (state.output.HasValue || state.halted || state.waitingForInput)
                        return;
                }
            }

            private long Get(long pointerOffset)
            {
                if (modes[pointerOffset] == 0)
                    return program[program[pointer + pointerOffset]];
                else if (modes[pointerOffset] == 1)
                    return program[pointer + pointerOffset];
                else if (modes[pointerOffset] == 2)
                    return program[program[pointer + pointerOffset] + relativeBase];
                else
                    throw new Exception("Invalid Mode");
            }

            private void Set(long pointerOffset, long value)
            {
                if (modes[pointerOffset] == 0)
                    program[program[pointer + pointerOffset]] = value;
                else if (modes[pointerOffset] == 1)
                    program[pointer + pointerOffset] = value;
                else if (modes[pointerOffset] == 2)
                    program[program[pointer + pointerOffset] + relativeBase] = value;
                else
                    throw new Exception("Invalid Mode");
            }
        }

        public class Robot
        {
            (int x, int y) direction = (0, -1);
            (int x, int y) location = (0, 0);
            public readonly Dictionary<(int x, int y), long> tiles = new Dictionary<(int x, int y), long>();
            readonly Computer computer;

            public Robot(long[] program, int startingTileColour)
            {
                tiles[location] = startingTileColour;
                computer = new Computer(program);
            }

            public void PaintTiles()
            {
                computer.Run();
                while (!computer.state.halted)
                {
                    computer.Run(GetCurrentTile);
                    PaintCurrentTile(computer.state.output.Value);
                    computer.Run();
                    TurnAndMove(computer.state.output.Value);
                    computer.Run();
                }
            }

            public int TilesPainted => tiles.Count;

            void TurnAndMove(long programOutput)
            {
                direction = programOutput switch
                {
                    1 => (direction.y * -1, direction.x),
                    0 => (direction.y, direction.x * -1),
                    _ => throw new Exception($"Invalid rotation input {programOutput}")
                };
                location = (location.x + direction.x, location.y + direction.y);
            }

            long GetCurrentTile => tiles.GetValueOrDefault(location, 0);

            void PaintCurrentTile(long programOuput)
            {
                if (programOuput != 0 && programOuput != 1)
                    throw new Exception($"Invalid paint input {programOuput}");
                tiles[location] = programOuput;
            }
        }

        public override object Part1()
        {
            var robot = new Robot(intCodes, 0);
            robot.PaintTiles();
            return robot.TilesPainted;
        }

        public override object Part2()
        {
            var robot = new Robot(intCodes, 1);
            robot.PaintTiles();
            (int x, int y) min = (0,0), max = (0,0);
            foreach (var (key, value) in robot.tiles)
            {
                if (value == 1)
                {
                    min = (Math.Min(min.x, key.x), Math.Min(min.y, key.y));
                    max = (Math.Max(max.x, key.x), Math.Max(max.y, key.y));
                }
            }
            long[,] tiles = new long[max.x - min.x + 1, max.y - min.y + 1];
            foreach (var (key, value) in robot.tiles)
            {
                if (value == 1)
                    tiles[key.x - min.x, key.y - min.y] = value;
            }
            PrintArray(tiles, 2, (1, '8'));
            return null;
        }

        void PrintArray(long[,] array, int zoom, (int, char) replace)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int x = 0; x < zoom; x++)
                {
                    for (int i = 0; i < array.GetLength(0); i++)
                    {
                        Console.Write(string.Concat(Enumerable.Repeat(array[i, j] == replace.Item1 ? replace.Item2.ToString() : " ", zoom)));
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}

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

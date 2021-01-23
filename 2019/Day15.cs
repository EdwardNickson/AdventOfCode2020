//using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day15 : Solver
    {
        long[] intCodes;
        public Day15(string input) : base(input)
        {
            intCodes = input.Split(",").Select(x => long.Parse(x)).ToArray();
        }

        class Robot
        {
            public (int x, int y) position;
            public int distance;
            public int direction;
            Computer computer;

            public Robot(long[] program)
            {
                position = (0, 0);
                distance = 0;
                computer = new Computer(program);
            }

            public Robot(Robot prev, int direction)
            {
                position = prev.position;
                distance = prev.distance;
                computer = (Computer) prev.computer.Clone();
                this.direction = direction;
            }

            public long Run()
            {
                computer.Run();
                computer.Run(direction);
                return computer.state.output.Value;
            }
        }

        HashSet<(int x, int y)> walls = new HashSet<(int x, int y)>();
        Dictionary<(int x, int y), int> visited = new Dictionary<(int x, int y), int>();
        int shortestPath = int.MaxValue;
        (int x, int y) oxygenLocation = (0, 0);

        public (int x, int y) GetDirection(int input) => input switch
            {
                1 => (0, -1),
                2 => (0, 1),
                3 => (1, 0),
                4 => (-1, 0),
                _ => throw new Exception($"Invalid direction input {input}")
            };

        public override object Part1()
        {
            List<Robot> robots = new List<Robot>()
            {
                new Robot(intCodes)
            };
            visited[(0,0)] = 0;
            while (true)
            {
                List<Robot> nextRobots = new List<Robot>();
                foreach(var robot in robots)
                {
                    for (int d = 1; d < 5; d++)
                    {
                        var (x, y) = GetDirection(d);
                        var nextPos = (robot.position.x + x, robot.position.y + y);
                        var nextDistance = robot.distance + 1;
                        if (walls.Contains(nextPos))
                            continue;
                        if (!visited.TryGetValue(nextPos, out var bestDistance) || bestDistance > nextDistance)
                        {
                            var next = new Robot(robot, d);
                            var output = next.Run();
                            if (output == 0)
                                walls.Add(nextPos);
                            else if (output == 1)
                            {
                                next.position = nextPos;
                                next.distance = nextDistance;
                                visited[nextPos] = nextDistance;
                                nextRobots.Add(next);
                            }
                            else if (output == 2)
                            {
                                if (nextDistance < shortestPath)
                                {
                                    shortestPath = nextDistance;
                                    oxygenLocation = nextPos;
                                }
                            }
                        }
                    }
                }
                if (nextRobots.Count == 0)
                    return shortestPath;
                robots = nextRobots;
            }
        }

        public override object Part2()
        {
            int minutes = 0;
            List<(int x, int y)> oxygenFrontier = new List<(int x, int y)>()
            {
                oxygenLocation
            };
            HashSet<(int x, int y)> noOxygen = visited.Keys.ToHashSet();
            while (noOxygen.Count > 0)
            {
                minutes++;
                List<(int x, int y)> changed = new List<(int x, int y)>();
                foreach (var pos in oxygenFrontier)
                {
                    for (int d = 1; d < 5; d++)
                    {
                        var (x, y) = GetDirection(d);
                        var nextPos = (pos.x + x, pos.y + y);
                        if (noOxygen.Contains(nextPos))
                        {
                            changed.Add(nextPos);
                        }
                    }
                }
                changed.ForEach(x =>
                {
                    noOxygen.Remove(x);
                });
                oxygenFrontier = changed;
            }
            return minutes;
        }
    }
}

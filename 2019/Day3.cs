using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day3 : Solver
    {
        readonly Dictionary<(int, int), int> pathA;
        readonly Dictionary<(int, int), int> pathB;
        readonly List<(int, int)> intersection;
        public Day3(string input) : base(input)
        {
            foreach (var line in input.Split("\n"))
            {
                var pathPoints = new Dictionary<(int, int), int>();
                var cur = (0, 0);
                var wireLength = 0;
                foreach (var dir in line.Split(","))
                {
                    var d = dir[0];
                    var l = int.Parse(dir[1..]);
                    for (int i=0; i < l; i++)
                    {
                        wireLength++;
                        var vector = GetDirectionVector(d);
                        cur = (cur.Item1 + vector.Item1, cur.Item2 + vector.Item2);
                        pathPoints.TryAdd(cur, wireLength);
                    }
                }
                if (pathA is null)
                    pathA = pathPoints;
                else
                    pathB = pathPoints;
            }
            intersection = pathA.Keys.Intersect(pathB.Keys).ToList();
        }

        public static (int, int) GetDirectionVector(char direction) =>
            direction switch
            {
                'R' => (0, 1),
                'L' => (0, -1),
                'U' => (1, 0),
                'D' => (-1, 0),
                _ => throw new NotImplementedException()
            };

        public static int ManhattanDistance((int, int) a, (int, int) b) =>
            Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);

        public override object Part1()
        {
            var origin = (0, 0);
            return intersection.Select(x => ManhattanDistance(x, origin)).Min();
        }

        public override object Part2()
        {
            return intersection.Select(x => pathA[x] + pathB[x]).Min();
        }
    }
}

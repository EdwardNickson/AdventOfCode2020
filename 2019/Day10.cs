using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day10 : Solver
    {
        readonly List<(int, int)> asteroids = new List<(int, int)>();
        (int, int) selectedAsteroid;

        public Day10(string input) : base(input)
        {
            var lines = input.Split("\n");
            var y = 0;
            foreach (var line in lines)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                        asteroids.Add((x, y));
                }
                y++;
            }
        }

        public class RelativeAsteroid
        {
            public (int, int) coord;
            public decimal degrees;
            public decimal radius;
            const int PRECISION = 4;

            public RelativeAsteroid((int, int) coord, (int, int) origin)
            {
                this.coord = coord;
                var vector = (coord.Item1 - origin.Item1, coord.Item2 - origin.Item2);
                radius = decimal.Round((decimal)Math.Sqrt(Math.Pow(vector.Item1, 2) + Math.Pow(vector.Item2, 2)), PRECISION);
                degrees = (decimal)(Math.Atan2(vector.Item2, vector.Item1) - Math.Atan2(-1, 0));
                if (vector.Item1 < 0 && vector.Item2 < 0)
                    degrees = (decimal)Math.PI * 2 + degrees;
                degrees = decimal.Round(degrees, PRECISION);
            }
        }

        public override object Part1()
        {
            var max = 0;
            foreach (var asteroid in asteroids)
            {
                var visibleAsteroids = asteroids.Where(x => x != asteroid)
                                .Select(x => new RelativeAsteroid(x, asteroid))
                                .Select(x => x.degrees).ToHashSet().Count;
                if (visibleAsteroids > max)
                {
                    selectedAsteroid = asteroid;
                    max = visibleAsteroids;
                }
            }
            return max;
        }

        public override object Part2()
        {
            var targets = asteroids.Where(x => x != selectedAsteroid)
                            .Select(x => new RelativeAsteroid(x, selectedAsteroid))
                            .OrderBy(x => x.degrees).ThenBy(x=>x.radius).ToList();
            decimal d = -1;
            int i = 0;
            int destroyed = 0;
            while (true)
            {
                if (i >= targets.Count)
                    i = 0;
                if (targets[i].degrees > d)
                {
                    destroyed++;
                    if (destroyed == 200)
                        return targets[i].coord.Item1 * 100 + targets[i].coord.Item2;
                    d = targets[i].degrees;
                    targets.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}

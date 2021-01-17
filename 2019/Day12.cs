using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2019
{
    public class Day12 : Solver
    {
        public class Moon
        {
            public (int x, int y, int z) pos;
            public (int x, int y, int z) vel = (0, 0, 0);

            public Moon(string x, string y, string z)
            {
                pos = (int.Parse(x), int.Parse(y), int.Parse(z));
            }

            public Moon((int x, int y, int z) pos)
            {
                this.pos = (pos.x, pos.y, pos.z);
            }

            public void UpdatePosition()
            {
                pos = (pos.x + vel.x, pos.y + vel.y, pos.z + vel.z);
            }

            public int Energy => (Math.Abs(pos.x) + Math.Abs(pos.y) + Math.Abs(pos.z)) * (Math.Abs(vel.x) + Math.Abs(vel.y) + Math.Abs(vel.z));
        }

        readonly Moon[] moons;
        public Day12(string input) : base(input)
        {
            moons = input.Split("\n").Select(x => {
                var match = Regex.Match(x, "<x=(.*), y=(.*), z=(.*)>");
                return new Moon(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value);
            }).ToArray();
        }

        public static void ApplyGravity(Moon[] moons)
        {
            for (int i = 0; i < moons.Length-1; i++)
            {
                for (int j = i+1; j < moons.Length; j++)
                {
                    var c = moons[i].pos.x.CompareTo(moons[j].pos.x);
                    moons[i].vel.x += c < 0 ? 1 : c > 0 ? -1 : 0;
                    moons[j].vel.x += c < 0 ? -1 : c > 0 ? 1 : 0;

                    c = moons[i].pos.y.CompareTo(moons[j].pos.y);
                    moons[i].vel.y += c < 0 ? 1 : c > 0 ? -1 : 0;
                    moons[j].vel.y += c < 0 ? -1 : c > 0 ? 1 : 0;

                    c = moons[i].pos.z.CompareTo(moons[j].pos.z);
                    moons[i].vel.z += c < 0 ? 1 : c > 0 ? -1 : 0;
                    moons[j].vel.z += c < 0 ? -1 : c > 0 ? 1 : 0;
                }
            }
        }

        public override object Part1()
        {
            var newMoons = moons.Select(x => new Moon(x.pos)).ToArray();
            for (int i = 0; i < 1000; i++)
            {
                ApplyGravity(newMoons);
                newMoons.ForEach(x => x.UpdatePosition());
            }
            return newMoons.Select(x => x.Energy).Sum();
        }

        public override object Part2()
        {
            long?[] cycles = new long?[3];
            cycles[0] = null; cycles[1] = null; cycles[2] = null;

            long steps = 0;
            var newMoons = moons.Select(x => new Moon(x.pos)).ToArray();
            do
            {
                steps++;
                ApplyGravity(newMoons);
                newMoons.ForEach(x => x.UpdatePosition());
                if (cycles[0] == null)
                {
                    bool equal = true;
                    for (int i = 0; i < moons.Length; i++)
                    {
                        equal &= moons[i].pos.x == newMoons[i].pos.x && moons[i].vel.x == newMoons[i].vel.x;
                    }
                    if (equal)
                        cycles[0] = steps;
                }
                if (cycles[1] == null)
                {
                    bool equal = true;
                    for (int i = 0; i < moons.Length; i++)
                    {
                        equal &= moons[i].pos.y == newMoons[i].pos.y && moons[i].vel.y == newMoons[i].vel.y;
                    }
                    if (equal)
                        cycles[1] = steps;
                }
                if (cycles[2] == null)
                {
                    bool equal = true;
                    for (int i = 0; i < moons.Length; i++)
                    {
                        equal &= moons[i].pos.z == newMoons[i].pos.z && moons[i].vel.z == newMoons[i].vel.z;
                    }
                    if (equal)
                        cycles[2] = steps;
                }
            } while (cycles.Any(x => x == null));
            return LCM(cycles.Select(x => x.Value).ToList());
        }

        public long LCM(List<long> numbers)
        {
            long a, b;
            if (numbers.Count > 2)
            {
                a = numbers[0];
                b = LCM(numbers.Skip(1).ToList());
            }
            else
            {
                a = numbers[0];
                b = numbers[1];
            }
            long am = a;
            long bm = b;
            while (am != bm)
            {
                if (am < bm)
                    am += a * (long) Math.Ceiling((double)(bm - am) / a);
                else
                    bm += b * (long)Math.Ceiling((double)(am - bm) / b); ;
            }
            return am;
        }
    }
}

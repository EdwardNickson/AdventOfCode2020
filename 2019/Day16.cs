//using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day16 : Solver
    {
        int[] signal;
        public Day16(string input) : base(input)
        {
            signal = input.Select(x => int.Parse(x.ToString())).ToArray();
        }

        List<(int, int, int)> getBoosts(int position)
        {
            return new List<(int, int, int)>()
            {
                (position, position * 2, 1),
                (position * 3 + 2, position * 4 + 2, -1)
            };
        }

        long GetSum(int limit, int p, int[] signal)
        {
            long sum = 0;
            foreach (var boost in getBoosts(p))
            {
                var (start, end, modulation) = boost;
                end = Math.Min(end, limit);
                var index = start;
                while (index < signal.Length)
                {
                    while (index <= end)
                    {
                        sum += signal[index] * modulation;
                        index++;
                    }
                    start += (p + 1) * 4;
                    end = Math.Min(end + (p + 1) * 4, limit);
                    index = start;
                }
            }
            return sum;
        }

        public override object Part1()
        {
            var returnSignal = (int[])signal.Clone();
            for (int i = 0; i < 100; i++)
            {
                var newSignal = new int[returnSignal.Length];
                for (int p = 0; p < returnSignal.Length; p++)
                {
                    newSignal[p] = (int)Math.Abs(GetSum(returnSignal.Length - 1, p, returnSignal) % 10);
                }
                returnSignal = newSignal;
            }
            return string.Join("", returnSignal.Take(8));
        }

        public override object Part2()
        {
            int repeatCount = 10000;
            var skip = int.Parse(string.Join("", signal.Take(7)));
            
            var returnSignal = new int[signal.Length * repeatCount - skip];
            for (int i = skip; i < repeatCount * signal.Length; i++)
            {
                returnSignal[i - skip] = signal[i % signal.Length];
            }
            for (int i = 0; i < 100; i++)
            {
                var total = 0;
                for (int p = returnSignal.Length - 1; p >= 0; p--)
                {
                    total += returnSignal[p];
                    returnSignal[p] = total % 10;
                }
            }
            return string.Join("", returnSignal.Take(8));
        }
    }
}

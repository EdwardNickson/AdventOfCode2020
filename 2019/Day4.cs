using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day4 : Solver
    {
        readonly int from, to;
        public Day4(string input) : base(input)
        {
            var tokens = input.Split("-");
            from = int.Parse(tokens[0]);
            to = int.Parse(tokens[1]);
        }

        public static bool ValidPassword(int password)
        {
            var s = password.ToString("D6");
            bool hasDouble = false;
            for (int i = 0; i < s.Length -1; i++)
            {
                if (s[i] == s[i + 1])
                    hasDouble = true;
                if (int.Parse(s[i + 1].ToString()) < int.Parse(s[i].ToString()))
                    return false;
            }
            return hasDouble;
        }

        public override object Part1()
        {
            var count = 0;
            for (int i = from; i <= to; i++)
            {
                count += ValidPassword(i) ? 1 : 0;
            }
            return count;
        }

        public static bool ReallyValidPassword(int password)
        {
            var s = password.ToString("D6");
            bool hasDouble = false;
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (!hasDouble && s[i] == s[i + 1])
                {
                    hasDouble = true;
                    if (i - 1 >= 0)
                        hasDouble &= s[i] != s[i - 1];
                    if (i + 2 < s.Length)
                        hasDouble &= s[i] != s[i + 2];
                }
                if (int.Parse(s[i + 1].ToString()) < int.Parse(s[i].ToString()))
                    return false;
            }
            return hasDouble;
        }

        public override object Part2()
        {
            var count = 0;
            for (int i = from; i <= to; i++)
            {
                count += ReallyValidPassword(i) ? 1 : 0;
            }
            return count;
        }
    }
}

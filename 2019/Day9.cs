using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day9 : Solver
    {
        long[] intCodes;
        public Day9(string input) : base(input)
        {
            intCodes = input.Split(",").Select(x => long.Parse(x)).ToArray();
        }

        public override object Part1()
        {
            var computer = new Computer(intCodes);
            computer.Run();
            computer.Run(1);
            while (!computer.state.halted)
            {
                Console.WriteLine(computer.state.output);
                computer.Run();
            }
            return computer.state.output;
        }

        public override object Part2()
        {
            var computer = new Computer(intCodes);
            computer.Run();
            computer.Run(2);
            return computer.state.output;
        }
    }
}

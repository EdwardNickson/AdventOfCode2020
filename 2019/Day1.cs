using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day1 : Solver
    {
        List<int> modules;
        public Day1(string input) : base(input)
        {
            modules = input.Split("\n").Select(x => int.Parse(x)).ToList();
        }
        public override string Part1()
        {
            return modules.Select(x => x / 3 - 2).Sum().ToString();
        }

        int CalculateFuel(int mass)
        {
            var required = mass / 3 - 2;
            return required > 0 ? required + CalculateFuel(required) : 0;
        }

        public override string Part2()
        {
            return modules.Select(x => CalculateFuel(x)).Sum().ToString();
        }
    }
}

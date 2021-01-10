using System;
using System.Collections.Generic;
using System.Text;

namespace _2019
{
    public abstract class Solver
    {
        protected readonly string Input;
        public Solver(string input)
        {
            Input = input;
        }
        public abstract string Part1();
        public abstract string Part2();
    }
}

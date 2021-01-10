using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day2 : Solver
    {
        int[] intCodes;
        public Day2(string input) : base(input)
        {
            intCodes = input.Split(",").Select(x => int.Parse(x)).ToArray();
        }

        public string Compute(int[] program)
        {
            var p = 0;
            while (program[p] != 99)
            {
                if (program[p] == 1)
                {
                    program[program[p + 3]] = program[program[p + 1]] + program[program[p + 2]];
                    p += 4;
                }
                else if (program[p] == 2)
                {
                    program[program[p + 3]] = program[program[p + 1]] * program[program[p + 2]];
                    p += 4;
                }
                else
                {
                    throw new Exception("Accessed invalid memory");
                }
            }
            return program[0].ToString();
        }

        public override string Part1()
        {
            var program = (int[]) intCodes.Clone();
            program[1] = 12;
            program[2] = 2;
            return Compute(program);
        }

        public override string Part2()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    try
                    {
                        var program = (int[])intCodes.Clone();
                        program[1] = i;
                        program[2] = j;
                        var result = Compute(program);
                        if (result == "19690720")
                            return (100 * i + j).ToString();
                    }
                    catch { } //bad noun and verb
                }
            }
            return "0";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day5 : Solver
    {
        int[] intCodes;
        public Day5(string input) : base(input)
        {
            intCodes = input.Split(",").Select(x => int.Parse(x)).ToArray();
        }

        class Computer
        {
            int pointer = 0;
            int[] program;
            int input;
            int[] modes = new int[4];
            public Computer(int[] program, int input)
            {
                this.program = (int[])program.Clone();
                this.input = input;
            }

            public void Step(out int? output, out bool halted)
            {
                var memVal = program[pointer];
                int opCode = memVal % 100;
                memVal /= 100;
                for (int i = 1; i < 4; i++)
                {
                    modes[i] = memVal % 10;
                    memVal /= 10;
                }

                halted = false;
                output = null;
                switch (opCode)
                {
                    case 99:
                        halted = true;
                        break;
                    case 1:
                        Set(3, Get(1) + Get(2));
                        pointer += 4;
                        break;
                    case 2:
                        Set(3, Get(1) * Get(2));
                        pointer += 4;
                        break;
                    case 3:
                        Set(1, input);
                        pointer += 2;
                        break;
                    case 4:
                        output = Get(1);
                        pointer += 2;
                        if (program[pointer] % 100 == 99)
                            halted = true;
                        break;
                    case 5:
                        pointer = Get(1) != 0 ? Get(2) : pointer + 3;
                        break;
                    case 6:
                        pointer = Get(1) == 0 ? Get(2) : pointer + 3;
                        break;
                    case 7:
                        Set(3, Get(1) < Get(2) ? 1 : 0);
                        pointer += 4;
                        break;
                    case 8:
                        Set(3, Get(1) == Get(2) ? 1 : 0);
                        pointer += 4;
                        break;
                    default:
                        throw new Exception("Accessed invalid memory");
                }
            }

            public int Get(int pointerOffset)
            {
                if (modes[pointerOffset] == 0)
                    return program[program[pointer + pointerOffset]];
                else if (modes[pointerOffset] == 1)
                    return program[pointer + pointerOffset];
                else
                    throw new Exception("Invalid Mode");
            }

            public void Set(int pointerOffset, int value)
            {
                if (modes[pointerOffset] == 0)
                    program[program[pointer + pointerOffset]] = value;
                else if (modes[pointerOffset] == 1)
                    program[pointer + pointerOffset] = value;
                else
                    throw new Exception("Invalid Mode");
            }
        }

        public override object Part1()
        {
            var input = 1;
            var computer = new Computer(intCodes, input);
            while (true)
            {
                computer.Step(out int? output, out bool halted);
                if (output.HasValue)
                {
                    if (halted)
                        return output;
                    else if (output != 0)
                        throw new Exception($"Invalid diagnostic output: {output}");
                }
                if (halted)
                {
                    throw new Exception("Halted without output");
                }
            }
        }

        public override object Part2()
        {
            var input = 5;
            var computer = new Computer(intCodes, input);
            while (true)
            {
                computer.Step(out int? output, out bool halted);
                if (output.HasValue)
                {
                    if (halted)
                        return output;
                    else if (output != 0)
                        throw new Exception($"Invalid diagnostic output: {output}");
                }
                if (halted)
                {
                    throw new Exception("Halted without output");
                }
            }
        }
    }
}

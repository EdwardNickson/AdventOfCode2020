using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day7 : Solver
    {
        int[] intCodes;
        public Day7(string input) : base(input)
        {
            intCodes = input.Split(",").Select(x => int.Parse(x)).ToArray();
        }

        private class Computer
        {
            public class State
            {
                public bool WaitingForInput = false;
                public bool Halted = false;
                public int? OutPut = null;
            }

            private int pointer = 0;
            private int[] program;
            private int[] modes = new int[4];
            public readonly State state = new State();

            public Computer(int[] program)
            {
                this.program = (int[])program.Clone();
            }

            private void Step(int? input)
            {
                var memVal = program[pointer];
                int opCode = memVal % 100;
                memVal /= 100;
                for (int i = 1; i < 4; i++)
                {
                    modes[i] = memVal % 10;
                    memVal /= 10;
                }

                state.WaitingForInput = false;
                state.OutPut = null;
                state.WaitingForInput = false;
                switch (opCode)
                {
                    case 99:
                        state.Halted = true;
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
                        if (input.HasValue)
                        {
                            Set(1, input.Value);
                            pointer += 2;
                        }
                        else
                            state.WaitingForInput = true;
                        break;
                    case 4:
                        state.OutPut = Get(1);
                        pointer += 2;
                        if (program[pointer] % 100 == 99)
                            state.Halted = true;
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

            public void Run(int? input = null)
            {
                while (true)
                {
                    Step(input);
                    if (input.HasValue)
                        input = null;
                    if (state.OutPut.HasValue || state.Halted || state.WaitingForInput)
                        return;
                }
            }

            private int Get(int pointerOffset)
            {
                if (modes[pointerOffset] == 0)
                    return program[program[pointer + pointerOffset]];
                else if (modes[pointerOffset] == 1)
                    return program[pointer + pointerOffset];
                else
                    throw new Exception("Invalid Mode");
            }

            private void Set(int pointerOffset, int value)
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
            return new List<int>() { 0, 1, 2, 3, 4 }.Permutations().Select(input =>
            {
                var computers = new Computer[5];
                for (int i = 0; i < 5; i++)
                {
                    computers[i] = new Computer(intCodes);
                    computers[i].Run();
                    computers[i].Run(input[i]);
                }
                for (int i = 0; i < 5; i++)
                {
                    computers[i].Run(i == 0 ? 0 : computers[i - 1].state.OutPut);
                }
                return computers[4].state.OutPut.Value;
            }).Max();
        }

        public override object Part2()
        {
            return new List<int>() { 5, 6, 7, 8, 9 }.Permutations().Select(input =>
            {
                var computers = new Computer[5];
                for (int i = 0; i < 5; i++)
                {
                    computers[i] = new Computer(intCodes);
                    computers[i].Run();
                    computers[i].Run(input[i]);
                }
                computers[4].state.OutPut = 0;
                while (!computers[4].state.Halted)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        computers[i].Run(computers[i == 0 ? 4 : i - 1].state.OutPut);
                    }
                }
                return computers[4].state.OutPut.Value;
            }).Max();
        }
    }
}

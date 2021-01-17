using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019
{
    public class Computer
    {
        public class State
        {
            public bool waitingForInput = false;
            public bool halted = false;
            public long? output = null;
        }

        public class DynamicArray
        {
            long[] array;
            public DynamicArray(long[] array)
            {
                this.array = array;
            }

            public long this[long i]
            {
                get
                {
                    Extend(i);
                    return array[i];
                }
                set
                {
                    Extend(i);
                    array[i] = value;
                }
            }

            private void Extend(long i)
            {
                if (i >= array.Length)
                {
                    long[] oldArray = array;
                    array = new long[i + 10000];
                    oldArray.CopyTo(array, 0);
                }
            }
        }

        private long pointer = 0;
        private long relativeBase = 0;
        private readonly DynamicArray program;
        private readonly long[] modes = new long[4];
        public readonly State state = new State();

        public Computer(long[] program)
        {
            this.program = new DynamicArray((long[])program.Clone());
        }

        private void Step(long? input)
        {
            var memVal = program[pointer];
            long opCode = memVal % 100;
            memVal /= 100;
            for (int i = 1; i < 4; i++)
            {
                modes[i] = memVal % 10;
                memVal /= 10;
            }

            state.waitingForInput = false;
            state.output = null;
            switch (opCode)
            {
                case 99:
                    state.halted = true;
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
                        state.waitingForInput = true;
                    break;
                case 4:
                    state.output = Get(1);
                    pointer += 2;
                    if (program[pointer] % 100 == 99)
                        state.halted = true;
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
                case 9:
                    relativeBase += Get(1);
                    pointer += 2;
                    break;
                default:
                    throw new Exception("Accessed invalid memory");
            }
        }

        public void Run(long? input = null)
        {
            while (true)
            {
                Step(input);
                if (input.HasValue)
                    input = null;
                if (state.output.HasValue || state.halted || state.waitingForInput)
                    return;
            }
        }

        private long Get(long pointerOffset)
        {
            if (modes[pointerOffset] == 0)
                return program[program[pointer + pointerOffset]];
            else if (modes[pointerOffset] == 1)
                return program[pointer + pointerOffset];
            else if (modes[pointerOffset] == 2)
                return program[program[pointer + pointerOffset] + relativeBase];
            else
                throw new Exception("Invalid Mode");
        }

        private void Set(long pointerOffset, long value)
        {
            if (modes[pointerOffset] == 0)
                program[program[pointer + pointerOffset]] = value;
            else if (modes[pointerOffset] == 1)
                program[pointer + pointerOffset] = value;
            else if (modes[pointerOffset] == 2)
                program[program[pointer + pointerOffset] + relativeBase] = value;
            else
                throw new Exception("Invalid Mode");
        }
    }
}

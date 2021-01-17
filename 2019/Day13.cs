using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day13 : Solver
    {
        long[] intCodes;
        public Day13(string input) : base(input)
        {
            intCodes = input.Split(",").Select(x => long.Parse(x)).ToArray();
        }

        public override object Part1()
        {
            var computer = new Computer(intCodes);
            Dictionary<(long, long), long> tiles = new Dictionary<(long, long), long>();
            while (!computer.state.halted)
            {
                computer.Run();
                if (computer.state.halted)
                    break;
                long x = computer.state.output.Value;
                computer.Run();
                long y = computer.state.output.Value;
                computer.Run();
                long tileId = computer.state.output.Value;
                tiles.Add((x, y), tileId);
            }
            return tiles.Where(item => item.Value == 2).Count();
        }

        public override object Part2()
        {
            intCodes[0] = 2;
            var computer = new Computer(intCodes);
            long score = 0;

            // We don't actually care about where the blocks are
            // Just need to keep our paddle beneath the ball
            (long, long) ball = (0,0), paddle = (0,0);

            while (!computer.state.halted)
            {
                computer.Run();
                if (computer.state.halted)
                    break;
                if (computer.state.waitingForInput)
                    computer.Run(paddle.Item1 < ball.Item1 ? 1 : paddle.Item1 > ball.Item1 ? -1 : 0);
                long x = computer.state.output.Value;
                computer.Run();
                long y = computer.state.output.Value;
                computer.Run();
                long tileId = computer.state.output.Value;
                
                if (x == -1 && y == 0)
                    score = tileId;
                else if (tileId == 3)
                    paddle = (x, y);
                else if (tileId == 4)
                    ball = (x, y);
            }
            return score;
        }
    }
}

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _2019
{
    class Program
    {
        public static readonly int YEAR = 2019;
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("Requires puzzle day as first argument. Pass 0 to run solutions for all days.");
            if (!int.TryParse(args[0], out var day) || day < 0 || day > 25)
                throw new ArgumentException("Puzzle day must be an integer between 0 and 25");

            if (day == 0)
            {
                for (int i = 1; i <= 25; i++)
                {
                    try
                    {
                        await RunDayAsync(i);
                    }
                    catch
                    {
                        // Probably means input hasn't been release, or we haven't built the day yet
                        break;
                    }
                }
            }
            else
            {
                await RunDayAsync(day);
            }
        }

        private static async Task RunDayAsync(int day)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var input = await AocUtils.LoadInputAsync(day, YEAR);
            var solver = CreateSolverForDay(day, input);
            var initTime = stopWatch.ElapsedMilliseconds;
            Console.WriteLine(initTime.ToString("D6") + " Initialised");
            stopWatch.Restart();
            var part1 = solver.Part1();
            var part1Time = stopWatch.ElapsedMilliseconds;
            Console.WriteLine(part1Time.ToString("D6") + " Part1: " + part1);
            stopWatch.Restart();
            var part2 = solver.Part2();
            var part2Time = stopWatch.ElapsedMilliseconds;
            Console.WriteLine(part2Time.ToString("D6") + " Part2: " + part2);
            Console.WriteLine("Total Time: " + (initTime + part1Time + part2Time));
        }

        static Solver CreateSolverForDay(int day, string input)
        {
            Type t = Type.GetType($"_2019.Day{day}");
            return Activator.CreateInstance(t, input) as Solver;
        }
    }
}

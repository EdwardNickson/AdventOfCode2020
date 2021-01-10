using System;
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
            var input = await AocUtils.LoadInputAsync(day, YEAR);
            var solver = CreateSolverForDay(day, input);
            var part1 = solver.Part1();
            Console.WriteLine(part1);
            var part2 = solver.Part2();
            Console.WriteLine(part2);
        }

        static Solver CreateSolverForDay(int day, string input)
        {
            Type t = Type.GetType($"_2019.Day{day}");
            return Activator.CreateInstance(t, input) as Solver;
        }
    }
}

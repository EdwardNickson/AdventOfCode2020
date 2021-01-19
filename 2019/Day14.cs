using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day14 : Solver
    {
        readonly List<Reaction> reactions = new List<Reaction>();
        class Reaction
        {
            public List<(string name, long quantity)> inputs;
            public string outputName;
            public long outputQuantity;
        }
        readonly Dictionary<string, long> spareResources = new Dictionary<string, long>();
        long oreUsed = 0;
        public Day14(string input) : base(input)
        {
            input.Split("\n").ForEach(line => {
                var tokens = line.Split(" => ");
                var output = tokens[1].Split(" ");
                var reaction = new Reaction()
                {
                    outputName = output[1],
                    outputQuantity = long.Parse(output[0]),
                    inputs = tokens[0].Split(", ").Select(x =>
                    {
                        var input = x.Split(" ");
                        return (input[1], long.Parse(input[0]));
                    }).ToList()
                };
                spareResources.TryAdd(reaction.outputName, 0);
                reaction.inputs.ForEach(x => spareResources.TryAdd(x.name, 0));
                reactions.Add(reaction);  
            });
            Console.WriteLine(reactions.Count);
            Console.WriteLine(reactions.Select(x => x.outputName).Distinct().Count());
        }

        long Make(string item, long count)
        {
            var reaction = reactions.First(x => x.outputName == item);
            long reactionCount = (long)Math.Ceiling((decimal)count / reaction.outputQuantity);
            reaction.inputs.ForEach(x =>
            {
                if (x.name == "ORE")
                    oreUsed += x.quantity * reactionCount;
                else
                {
                    var subReaction = reactions.First(y => y.outputName == x.name);
                    var required = reactionCount * x.quantity - spareResources[x.name];
                    if (required > 0)
                    {
                        spareResources[x.name] = Make(subReaction.outputName, required);
                    }
                    else
                    {
                        spareResources[x.name] = required * -1;
                    }
                }

            });
            return reaction.outputQuantity * reactionCount - count;
        }

        public long MakeFuel(long count)
        {
            oreUsed = 0;
            spareResources.ForEach(x => spareResources[x.Key] = 0);
            Make("FUEL", count);
            return oreUsed;
        }

        public override object Part1()
        {
            return MakeFuel(1);
        }

        public override object Part2()
        {
            long hold = 1000000000000;
            long lower = hold / oreUsed; //lower bound assuming perfect reaction with no leftovers ingredients
            long upper = lower;
            while (MakeFuel(upper) <= hold)
            {
                upper += lower;
            }
            lower = upper - lower;
            long i = lower + (upper - lower) / 2;
            while (true)
            {
                var oreUsed = MakeFuel(i);
                if (oreUsed == hold)
                    return i;
                else if (oreUsed < hold)
                {
                    lower = i;
                    i += (upper - i) / 2;
                    if (lower == i)
                        return lower;
                }
                else if (oreUsed > hold)
                {
                    upper = i;
                    i -= (i - lower) / 2;
                }
                if (upper == lower)
                    return upper;
            }
        }
    }
}

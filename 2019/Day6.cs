using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day6 : Solver
    {
        public class SpaceObject
        {
            public List<SpaceObject> children = new List<SpaceObject>();
            public SpaceObject parent;
            public string name;
            public SpaceObject(string name)
            {
                this.name = name;
            }
        }
        Dictionary<string, SpaceObject> objects = new Dictionary<string, SpaceObject>();
        public Day6(string input) : base(input)
        {
            input.Split("\n").ToList().ForEach(x => {
                var tokens = x.Split(")");
                objects.TryAdd(tokens[0], new SpaceObject(tokens[0]));
                objects.TryAdd(tokens[1], new SpaceObject(tokens[1]));
                objects[tokens[0]].children.Add(objects[tokens[1]]);
                objects[tokens[1]].parent = objects[tokens[0]];
            });
        }

        public override object Part1()
        {
            SpaceObject root = objects.Where(x => x.Value.parent == null).First().Value;
            return OrbitCount(root, 0);
        }

        public int OrbitCount(SpaceObject obj, int count)
        {
            return count + obj.children.Select(x => OrbitCount(x, count + 1)).Sum();
        }

        public int? Transfer(SpaceObject obj, string from, int transfers)
        {
            if (obj.children.Exists(x => x.name == "SAN"))
            {
                return transfers;
            }

            var targets = new List<SpaceObject>(obj.children);
            if (obj.parent != null)
                targets.Add(obj.parent);

            var moves = targets
                        .Where(x => x.name != from)
                        .Select(x => Transfer(x, obj.name, transfers + 1));
            
            return moves.Min();
        }

        public override object Part2()
        {
            return Transfer(objects["YOU"].parent, "YOU", 0);
        }
    }
}

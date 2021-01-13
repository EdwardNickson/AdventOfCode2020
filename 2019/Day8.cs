using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2019
{
    public class Day8 : Solver
    {
        List<string> layers = new List<string>();
        int width = 25;
        int height = 6;
        public Day8(string input) : base(input)
        {
            for (int i = 0; i < input.Length; i += width * height)
            {
                layers.Add(input.Substring(i, width * height));
            }
        }

        public override object Part1()
        {
            var layer = layers.MinBy(x => x.Count(y => y =='0')).First();
            return layer.Count(x => x == '1') * layer.Count(x => x == '2');
        }

        public override object Part2()
        {
            var pic = new char[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pic[i,j] = '2';
                }
            }
            foreach (var layer in layers)
            {
                for (int i = 0; i < width * height; i++)
                {
                    var x = i % width;
                    var y = i / width;
                    if (layer[i] != '2' && pic[x,y] == '2')
                    {
                        pic[x, y] = layer[i];
                    }
                }
            }
            int zoom = 2;
            for (int j = 0; j < height; j++)
            {
                for (int x =0; x < zoom; x++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Console.Write(string.Concat(Enumerable.Repeat(pic[i, j] == '1' ? "X" : " ", zoom)));
                    }
                    Console.WriteLine();
                }
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] settings = Console.ReadLine().Split(' ');
            int n = int.Parse(settings[0]);
            int m = int.Parse(settings[1]);

            List<Tuple<int, int>> things = new List<Tuple<int, int>>();
            for (int i = 0; i < n; i++)
            {
                string[] data = Console.ReadLine().Split(' ');
                things.Add(Tuple.Create(int.Parse(data[0]), int.Parse(data[1])));
            }
            int totalB = 0;
            totalB +=things.Sum(t=>t.Item2==0?t.Item1:0);
            things = things.Where(t => t.Item2 != 0).ToList();
            while (things.Count > 0)
            {
                things = things.Where(t => t.Item2 < m).ToList();
                int b = 0;
                int maxFinesse = 0;
                for(int i =0;i<things.Count;i++)
                {
                    int finesse =things[i].Item1 / things[i].Item2;
                    if (finesse > maxFinesse)
                    {
                        maxFinesse = finesse;
                        b = i;
                    }
                }
                totalB += things[b].Item1;
                things.RemoveAt(b);
            }
            Console.WriteLine(totalB);
        }
    }
}

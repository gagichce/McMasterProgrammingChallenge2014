using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Program
    {
        static double SolveHeight(double theta, double x, double v)
        {
            theta = theta * Math.PI / 180;
            return -9.81 / 2 * (x * x / (v * v * Math.Cos(theta) * Math.Cos(theta))) + Math.Tan(theta) * x;
        }
        static void Main(string[] args)
        {
            int numWalls = int.Parse(Console.ReadLine());
            Tuple<double, double> Target = Tuple.Create(0.0,0.0);
            List<Tuple<double, double>> Obstacles = new List<Tuple<double, double>>(); 
            for (int i = 0; i < numWalls; i++)
            {
                string[] line = Console.ReadLine().Split(' ');
                var thing = Tuple.Create(double.Parse(line[0]),double.Parse(line[1]));
                if (i == 0)
                    Target = thing;
                else
                    Obstacles.Add(thing);
            }
            bool solved = false;
            for (int bags = 1; bags <= 6&&!solved; bags++)
                for (int d = 0; d <= 75&&!solved;d++)
                {
                    var speed = 150 * bags;
                    if (Math.Abs(SolveHeight(d, Target.Item1, speed)-Target.Item2) < 10)
                    {
                        var cleared = true;
                        foreach (var obstacle in Obstacles)
                        {
                            if (!(SolveHeight(d, obstacle.Item1, speed) > 10 + Target.Item2))
                            {
                                cleared = false;
                                break;
                            }
                        }
                        if (cleared)
                        {
                            Console.WriteLine("{0} {1}", d, bags);
                            solved=true;
                            break;
                        }
                    }
                }
            if (solved == false)
                Console.WriteLine("No Solution");
        }
    }
}

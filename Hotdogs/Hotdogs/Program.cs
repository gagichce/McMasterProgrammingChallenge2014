using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotdogs
{
    class Program
    {
        static double CrossProduct(Tuple<double, double> dog1, Tuple<double, double> dog2)
        {
            return dog1.Item1 * dog2.Item2 - dog1.Item2 * dog2.Item1;
        }
        static Tuple<double, double> Subtract(Tuple<double, double> dog1, Tuple<double, double> dog2)
        {
            return Tuple.Create(dog1.Item1 - dog2.Item1, dog1.Item2 - dog2.Item2);
        }
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Tuple<double,double>> points = new List<Tuple<double,double>>(n);
            for(int iter=0;iter<n;iter++)
            {
                string[] pieces = Console.ReadLine().Split(' ');
                points.Add(Tuple.Create(double.Parse(pieces[0]),double.Parse(pieces[1])));

                
            }
            var candidateDogs = points.AsEnumerable().ToList();
            var perimDogs = new List<Tuple<double,double>>();
            while (candidateDogs.Count > 0)
            {
                var dog = candidateDogs.Last();
                candidateDogs.Remove(dog);
                foreach (var other in points)
                {
                    if (other == dog)
                        continue;
                var line = Subtract(dog,other);
                    bool isPerim = true;
                    int sign = 0;
                    foreach (var inner in points)
                    {
                        if (inner == dog || inner == other)
                            continue;
                        var innerLine = Subtract(inner, dog);
                        var inSign = Math.Sign(CrossProduct(line,innerLine));
                        if (sign == 0)
                            sign = inSign;
                        else if (inSign == 0)
                        {
                        }
                        else if (inSign != sign)
                        {
                            isPerim = false;
                            break;
                        }
                    }
                    if (isPerim)
                    {
                        perimDogs.Add(dog);
                        break;
                    }
                }
            }
            Console.WriteLine("{0}", perimDogs.Sum(x => x.Item1+x.Item2));
        }
    }
}

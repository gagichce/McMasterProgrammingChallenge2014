using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int iter = 0; iter < 1; iter++)
            {
                string[] line = Console.ReadLine().Split(' ');
                string t1 = line[0] + line[1];
                string t2 = line[2] + line[3];
                var d1 = DateTime.Parse(t1);
                var d2 = DateTime.Parse(t2);
                if (d2 <= d1) d2.AddDays(1);
                double fee = 0;
                if (d1<DateTime.Parse("7:00 AM"))
                    d1 = DateTime.Parse("7:00 AM");
                else if (d1.AddHours(1) <= d2)
                {
                    d1 = d1.AddHours(1);
                    fee += 2;
                }
                Tuple<string, double, double>[] fees = { 
                                                           Tuple.Create("6:59 AM", 1.0, 0.0),
                                                           Tuple.Create("11:59 AM", 30.0, 0.5) ,
                                                           Tuple.Create("4:59 PM", 20.0, 0.5) ,
                                                           Tuple.Create("9:59 PM", 60.0, 0.75)
                                                       };
                foreach (var feeRange in fees)
                {
                    var date = DateTime.Parse(feeRange.Item1);
                    if (date > d1)
                    {
                        if (d2 < date)
                        {
                            fee += d2.Subtract(d1).TotalMinutes / feeRange.Item2 * feeRange.Item3;
                            d1 = d2;
                            break;
                        }
                        fee += date.Subtract(d1).TotalMinutes / feeRange.Item2 * feeRange.Item3;
                        d1 = date.AddMinutes(1);
                    }
                }
                Console.WriteLine("${0:0.00}", Math.Floor(fee * 100) / 100);
            }
        }
    }
}

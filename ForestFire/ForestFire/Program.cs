using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestFire
{
    class Program
    {
        static bool HasTrees(char[,] forest)
        {
            bool hasTrees = false;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (forest[x, y] == 'T')
                    {
                        hasTrees = true;
                        break;
                    }
                }
            }
            return hasTrees;
        }
        static void Main(string[] args)
        {
            //var lines = System.IO.File.ReadAllLines("input.txt");
            char[,] forest = new char[10, 10];
            for (int i = 0; i < 10; i++)
            {
                string line = Console.ReadLine();
                for (int y = 0; y < 10; y++)
                {
                    //forest[i, y] = lines[i][y];
                    forest[i, y] = line[y];
                }
            }
            bool changed = true;
            int iterations = 0;
            while (changed && HasTrees(forest))
            {
                changed = false;
                iterations++;
                char[,] newForest = new char[10,10];

                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        newForest[x, y] = forest[x, y];
                    }
                }
                for(int x=0;x<10;x++)
                {
                    for(int y=0;y<10;y++)
                    {
                        if (forest[x, y] == 'F')
                        {
                            for (int xd = -1; xd < 2; xd ++)
                            {
                                for(int yd = -1; yd<2;yd++)
                                {
                                    try
                                    {
                                        if (Math.Abs(xd) != Math.Abs(yd))
                                        {
                                            if (forest[x + xd, y + yd] == 'T')
                                            {
                                                bool isProtected = false;
                                                for (int xdi = -1; xdi < 2; xdi++)
                                                {
                                                    for (int ydi = -1; ydi < 2; ydi++)
                                                    {
                                                        try
                                                        {
                                                            if (Math.Abs(xdi) != Math.Abs(ydi))
                                                            {
                                                                if (forest[x + xd + xdi, y + yd + ydi] == 'W')
                                                                    isProtected = true;
                                                            }
                                                        }
                                                        catch { }
                                                    }
                                                }
                                                if (!isProtected)
                                                {
                                                    newForest[x + xd, y + yd] = 'F';
                                                    changed = true;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }

                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        if (forest[x, y] == 'W')
                        {
                            for (int xd = -1; xd < 2; xd++)
                            {
                                for (int yd = -1; yd < 2; yd++)
                                {
                                    try
                                    {
                                        if (!(xd == 0 && yd == 0))
                                            if (forest[x + xd, y + yd] == 'F')
                                            {
                                                newForest[x, y] = '.';
                                                changed = true;
                                            }
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
                forest=newForest;
            }
            if (HasTrees(forest))
            {
                Console.WriteLine("-1");
            }
            else
                Console.WriteLine(iterations);
        }
    }
}

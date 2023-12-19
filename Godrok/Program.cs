using System;

namespace Godrok
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            double WIDTH = 10;
            string filename = "melyseg.txt";
            
            //1. feladat
            List<int> godrok = Read(filename);
            Console.WriteLine($"1. feladat\nA fájl adatainak száma: {godrok.Count}");

            //2. feladat
            Console.Write("\n2. feladat\nAdjon meg egy távolságértéket! ");
            int lengthIndex = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Ezen a helyen a felszín {godrok[lengthIndex-1]} méter mélyen van.");

            //3. feladat
            Console.WriteLine("\n3. feladat");
            double unhandeld = godrok.Count(x => x == 0);
            Console.WriteLine($"Az érintetlen terület aránya {Math.Round(unhandeld/godrok.Count*100, 2)}%.");

            //4. feladat
            List<string> godroktxt = new List<string>();
            for (int i = 1; i < godrok.Count; i++)
            {
                if (godrok[i] != 0)
                {
                    if (godrok[i - 1] == 0)
                        godroktxt.Add(godrok[i].ToString());
                    else
                        godroktxt[godroktxt.Count - 1] = godroktxt[godroktxt.Count - 1] + " " + godrok[i].ToString();
                }
                
            }
            StreamWriter sw = new StreamWriter("godrok.txt");
            foreach (var item in godroktxt)
            {
                sw.WriteLine(item);
            }
            sw.Close();

            //5. feladat
            Console.WriteLine($"\n5. feladat\nA gödrök száma: {godroktxt.Count}");

            //6. feladat
            Console.WriteLine("\n6. feladat");
            if (godrok[lengthIndex - 1] == 0) Console.WriteLine("Az adott helyen nincs gödör.");
            else
            {
                //6a
                int start = 0;
                int end = 0;
                for (int i = lengthIndex; i >= 0; i--)
                {
                    if (godrok[i-1] == 0)
                    {
                        start = i;
                        break;
                    }
                }
                for (int i = lengthIndex; i < godrok.Count; i++)
                {
                    if (godrok[i + 1] == 0)
                    {
                        end = i;
                        break;
                    }
                }

                Console.WriteLine($"a)\nA gödör kezdete: {start+1} méter, a gödör vége: {end+1} méter.");

                //6b
                int maxLenghtInd = start;
                for (int i = start; i <= end; i++)
                {
                    maxLenghtInd = godrok[i] >= godrok[maxLenghtInd] ? i : maxLenghtInd;
                }
                bool monotonous = true;
                for (int i = start+1; i <= maxLenghtInd; i++)
                {
                    if (godrok[i-1] > godrok[i])
                    {
                        monotonous = false;
                        break;
                    }
                }
                if (monotonous)
                {
                    for (int i = maxLenghtInd +1; i <= end; i++)
                    {
                        if (godrok[i - 1] < godrok[i])
                        {
                            monotonous = false;
                            break;
                        }
                    }
                }
                if (monotonous) Console.WriteLine("b)\nFolyamatosan mélyül.");
                else Console.WriteLine("b)\nNem mélyül folyamatosan.");

                //6c
                Console.WriteLine($"c)\nA legnagyobb mélysége {godrok[maxLenghtInd]} méter.");

                //6d
                int capacity = 0;
                for (int i = start; i <= end; i++)
                {
                    capacity += godrok[i] * 10;
                }
                Console.WriteLine($"d)\nA térfogata {capacity} m^3.");

                //6e
                int waterCapacity = 0;
                for (int i = start; i <= end; i++)
                {
                    if (godrok[i] > 1)
                    {
                        waterCapacity += (godrok[i] -1) * 10;
                    }
                }
                Console.WriteLine($"e)\nA vízmennyiség {waterCapacity} m^3.");

            }
        }

        static List<int> Read(string filename)
        {
            List<int> list = new List<int>();
            StreamReader sr = new StreamReader(filename);
            while (!sr.EndOfStream)
            {
                list.Add(Convert.ToInt32(sr.ReadLine()));
            }
            sr.Close();
            return list;
        }
    }
}
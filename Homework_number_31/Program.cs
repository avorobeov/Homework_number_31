using System;
using System.IO;
using System.Threading;

namespace Homework_number_31
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            bool isPlaying = true;

            string mapName = "m1";
            char[,] map;
            int packmanX;
            int packmanY;
            int packmanDX = 0;
            int packmanDY = 1;

            map = ReadMap(mapName, out packmanX,out packmanY);

            DrawMap(map);

            while (isPlaying)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(key, ref packmanDX, ref packmanDY);
                }

                if (map[packmanX + packmanDX, packmanY + packmanDY] != '#')
                {
                    Move(ref packmanX, ref packmanY, packmanDX, packmanDY);
                }

                Thread.Sleep(200);
            }
            Console.SetCursorPosition(packmanY, packmanX);
            Console.Write("@");
        }

        private static void Move(ref int X, ref int Y, int DX, int DY)
        {
            Console.SetCursorPosition(Y, X);
            Console.Write(" ");

            X += DX;
            Y += DY;

            Console.SetCursorPosition(Y, X);
            Console.Write('@');
        }
        
        private static void ChangeDirection(ConsoleKeyInfo key,ref int DX,ref int DY)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    DX = -1; DY = 0;
                    break;

                case ConsoleKey.DownArrow:
                    DX = 1; DY = 0;
                    break;

                case ConsoleKey.LeftArrow:
                    DX = 0; DY = -1;
                    break;

                case ConsoleKey.RightArrow:
                    DX = 0; DY = 1;
                    break;
            }
        }

        private static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        private static char[,] ReadMap(string mapName, out int packmanX, out int packmanY)
        {
            packmanX = 0;
            packmanY = 0;

            string[] newFile = File.ReadAllLines($"Maps\\{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == '@')
                    {
                        packmanX = i;
                        packmanY = j;
                    }
                    else if (map[i, j] == ' ')
                    {
                        map[i, j] = '.';
                    }
                }
            }

            return map;
        }
    }
}
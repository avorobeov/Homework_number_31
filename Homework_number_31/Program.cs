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
            char obstacle = '#';
            char hero = '@';
            char emptyCell = ' ';
            char bonusCell = '.';
            int packmanX;
            int packmanY;
            int packmanDirectionX = 0;
            int packmanDirectionY = 1;
            int lagTime = 200;

            map = ReadMap(mapName, out packmanX,out packmanY, hero, emptyCell, bonusCell);

            DrawMap(map);

            while (isPlaying)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(key, ref packmanDirectionX, ref packmanDirectionY);
                }

                if (map[packmanX + packmanDirectionX, packmanY + packmanDirectionY] != obstacle)
                {
                    Move(ref packmanX, ref packmanY, packmanDirectionX, packmanDirectionY, hero, emptyCell);
                }

                Thread.Sleep(lagTime);
            }
        }

        private static void Move(ref int positionX, ref int positionY, int directionX, int directionY, char hero,char emptyCell)
        {
            PrintMove(positionX, positionY, emptyCell);

            positionX += directionX;
            positionY += directionY;

            PrintMove(positionX, positionY, hero);
        }

        private static void PrintMove(int positionX, int positionY,char symbol)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(symbol);
        }
        
        private static void ChangeDirection(ConsoleKeyInfo key,ref int directionX,ref int directionY)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    directionX = -1; directionY = 0;
                    break;

                case ConsoleKey.DownArrow:
                    directionX = 1; directionY = 0;
                    break;

                case ConsoleKey.LeftArrow:
                    directionX = 0; directionY = -1;
                    break;

                case ConsoleKey.RightArrow:
                    directionX = 0; directionY = 1;
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

        private static char[,] ReadMap(string mapName, out int packmanX, out int packmanY,char hero, char emptyCell,char bonusCell)
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

                    if (map[i, j] == hero)
                    {
                        packmanX = i;
                        packmanY = j;
                    }
                    else if (map[i, j] == emptyCell)
                    {
                        map[i, j] = bonusCell;
                    }
                }
            }

            return map;
        }
    }
}
using System;
using System.ComponentModel.Design;
using System.IO;
using System.Threading;

namespace Homework_number_31
{
    class Program
    {
        const ConsoleKey CommandToUp = ConsoleKey.UpArrow;
        const ConsoleKey CommandToDown = ConsoleKey.DownArrow;
        const ConsoleKey CommandToLeft = ConsoleKey.LeftArrow;
        const ConsoleKey CommandToRight = ConsoleKey.RightArrow;

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
            int packmanPositionX;
            int packmanPositionY;
            int packmanDirectionX = 0;
            int packmanDirectionY = 1;
            int lagTime = 200;

            map = ReadMap(mapName, out packmanPositionX, out packmanPositionY, hero, emptyCell, bonusCell);

            DrawMap(map);

            while (isPlaying)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(key, ref packmanDirectionX, ref packmanDirectionY);
                }

                if (map[packmanPositionX + packmanDirectionX, packmanPositionY + packmanDirectionY] != obstacle)
                {
                    Move(ref packmanPositionX, ref packmanPositionY, packmanDirectionX, packmanDirectionY, hero, emptyCell);
                }

                Thread.Sleep(lagTime);
            }
        }

        private static void Move(ref int positionX, ref int positionY, int directionX, int directionY, char hero, char emptyCell)
        {
            PrintMove(positionX, positionY, emptyCell);

            positionX += directionX;
            positionY += directionY;

            PrintMove(positionX, positionY, hero);
        }

        private static void PrintMove(int positionX, int positionY, char symbol)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(symbol);
        }

        private static void ChangeDirection(ConsoleKeyInfo key, ref int directionX, ref int directionY)
        {
            switch (key.Key)
            {
                case CommandToUp:
                    directionX = -1; directionY = 0;
                    break;

                case CommandToDown:
                    directionX = 1; directionY = 0;
                    break;

                case CommandToLeft:
                    directionX = 0; directionY = -1;
                    break;

                case CommandToRight:
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

        private static char[,] ReadMap(string mapName, out int packmanPositionX, out int packmanPositionY, char hero, char emptyCell, char bonusCell)
        {
            packmanPositionX = 0;
            packmanPositionY = 0;

            string[] newFile = File.ReadAllLines($"Maps\\{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == hero)
                    {
                        packmanPositionX = i;
                        packmanPositionY = j;
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
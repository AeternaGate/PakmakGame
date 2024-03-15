using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Pakmak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            char[,] map = ReadMap("map.txt");
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            int pakmakX = 1, pakmakY = 1, score = 0;

            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            while (true)
            {
                Console.Clear();

                HandleInput(pressedKey, ref pakmakX, ref pakmakY, map, ref score);

                Console.ForegroundColor = ConsoleColor.Green;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pakmakX, pakmakY);
                Console.Write("@");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(32, 0);
                Console.Write($"Score: {score}");

                Thread.Sleep(500);
            }
        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);
            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];

            return map;
        }

        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }

                Console.Write("\n");
            }
        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pakmakX, ref int pakmakY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPakmakPositionX = pakmakX + direction[0];
            int nextPakmakPositionY = pakmakY + direction[1];

            if (map[nextPakmakPositionX, nextPakmakPositionY] != '#')
            {
                pakmakX = nextPakmakPositionX;
                pakmakY = nextPakmakPositionY;
            }
            if (map[nextPakmakPositionX, nextPakmakPositionY] == '.')
            {
                score++;
                map[nextPakmakPositionX, nextPakmakPositionY] = ' ';
            }
        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    direction[1]--;
                    break;
                case ConsoleKey.DownArrow:
                    direction[1]++;
                    break;
                case ConsoleKey.LeftArrow:
                    direction[0]--;
                    break;
                case ConsoleKey.RightArrow:
                    direction[0]++;
                    break;
            }
            return direction;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = 0;

            foreach (string line in lines)
            {
                if (line.Length > maxLength) maxLength = line.Length;
            }
            return maxLength;
        }
    }
}

using System;
using System.Collections.Generic;

namespace ConsolePain
{
    struct Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class Paint
    {
        int[,] map;
        int width, height;
        string symbol = " #+ox";
        ConsoleColor[] colors =
        {
            ConsoleColor.White,
            ConsoleColor.DarkBlue,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Cyan
        };
        public Paint(int width, int height)
        {
            this.width = width;
            this.height = height;
            map = new int[width, height];
        }

        void SetMap(int x, int y, int number)
        {
            if (x < 0 || x >= width) return;
            if (y < 0 || y >= height) return;
            map[x, y] = number;
            PrintAt(x, y);
        }

        private void PrintAt(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = colors[map[x, y]];
            Console.Write(symbol[map[x, y]]);
            Console.SetCursorPosition(0, 0);
        }

        public void PutRandomNumber(int count)
        {
            Random random = new Random();
            for (int j = 0; j < count; j++)
            {
                SetMap(random.Next(width), random.Next(height), 1);
            }
        }

        public void FillFor(int px, int py)
        {
            
            SetMap(px, py, 2);
            while (true)
            {
                bool stop = true;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (map[x, y] == 2)
                        {
                            SetMap(x, y, 4);
                            stop = false;
                            if (IsEmpty(x - 1, y)) SetMap(x - 1, y, 3);
                            if (IsEmpty(x + 1, y)) SetMap(x + 1, y, 3);
                            if (IsEmpty(x, y - 1)) SetMap(x, y - 1, 3);
                            if (IsEmpty(x, y + 1)) SetMap(x, y + 1, 3);
                        }
                    }
                }
                if (stop) break;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (map[x, y] == 3)
                        {
                            SetMap(x, y, 2);
                        }
                    }
                }
            }
        }
        public void FillQueue(int px, int py)
        {
            SetMap(px, py, 2);
            Queue<Coord> queue = new Queue<Coord>();
            queue.Enqueue(new Coord(px, py));
            while(queue.Count>0)
            {
                Coord coord = queue.Dequeue();
                int x = coord.x;
                int y = coord.y;
                SetMap(x, y, 4);
                if (IsEmpty(x - 1, y)) { queue.Enqueue(new Coord(x - 1, y)); SetMap(x - 1, y, 2); }
                if (IsEmpty(x + 1, y)) { queue.Enqueue(new Coord(x + 1, y)); SetMap(x + 1, y, 2); }
                if (IsEmpty(x, y - 1)) { queue.Enqueue(new Coord(x, y - 1)); SetMap(x, y - 1, 2); }
                if (IsEmpty(x, y + 1)) { queue.Enqueue(new Coord(x, y + 1)); SetMap(x, y + 1, 2); }
            }
        }

        public void FillStack(int px, int py) 
        {
            SetMap(px, py, 2);
            Stack<Coord> stack = new Stack<Coord>();
            stack.Push(new Coord(px, py));
            while (stack.Count > 0)
            {
                Coord coord = stack.Pop();
                int x = coord.x;
                int y = coord.y;
                SetMap(x, y, 4);
                if (IsEmpty(x - 1, y)) { stack.Push(new Coord(x - 1, y)); SetMap(x - 1, y, 2); }
                if (IsEmpty(x + 1, y)) { stack.Push(new Coord(x + 1, y)); SetMap(x + 1, y, 2); }
                if (IsEmpty(x, y - 1)) { stack.Push(new Coord(x, y - 1)); SetMap(x, y - 1, 2); }
                if (IsEmpty(x, y + 1)) { stack.Push(new Coord(x, y + 1)); SetMap(x, y + 1, 2); }
            }
        }

        public void FillDepth(int x, int y)
        {
            if (!IsEmpty(x, y)) return;
            SetMap(x, y, 2);
            FillDepth(x - 1, y);
            FillDepth(x + 1, y);
            FillDepth(x, y - 1);
            FillDepth(x, y + 1);
            SetMap(x, y, 4);
        }
        bool IsEmpty(int x, int y)
        {
            if (x < 0 || x >= width) return false;
            if (y < 0 || y >= height) return false;
            return map[x, y] == 0;
        }
    }
}

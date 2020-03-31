using System;

namespace ConsolePain
{
    class Program
    {
        static void Main(string[] args)
        {
            Paint paint = new Paint(40, 20);
            paint.PutRandomNumber(100);
            //paint.FillFor(20, 10);
            //paint.FillQueue(20, 10);
            //paint.FillStack(10, 20);
            paint.FillDepth(20, 10);
            Console.ReadKey();
        }
    }
}

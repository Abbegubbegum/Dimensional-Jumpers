using System;
using System.Numerics;
using Raylib_cs;


namespace Dimensional_Jumper
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(1920, 1000, "Dimensional Jumper");

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                Raylib.EndDrawing();
            }
        }
    }
}

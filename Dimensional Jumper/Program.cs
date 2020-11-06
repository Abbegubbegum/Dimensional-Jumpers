using System;
using System.Numerics;
using Raylib_cs;

namespace Dimensional_Jumper
{
    class Program
    {
        static void Main(string[] args)
        {
            //Define global variables
            const int windowX = 1920;
            const int windowY = 1000;
            string gameState = "level1";
            int level = 1;

            //Intro variables
            int introMenuIndex = 0;
            Color[] menuColors = { Color.BLACK, Color.GRAY, Color.GRAY, Color.GRAY };

            //Controlscreen variables
            Color wColor = Color.GRAY;
            Color aColor = Color.GRAY;
            Color dColor = Color.GRAY;
            Color spaceColor = Color.GRAY;
            Color tabColor = Color.GRAY;

            //Game variables
            Color playerC = Color.RED;
            Rectangle playerRec = new Rectangle(600, 800, 50, 100);


            Raylib.InitWindow(windowX, windowY, "Dimensional Jumper");

            while (!Raylib.WindowShouldClose())
            {
                if (gameState == "intro")
                {
                    //Logic Introscreen
                    //Menu selecting and coloring
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
                    {
                        menuColors[introMenuIndex] = Color.GRAY;
                        if (introMenuIndex == menuColors.Length - 1)
                        {
                            introMenuIndex = 0;
                        }
                        else
                        {
                            introMenuIndex++;
                        }
                        menuColors[introMenuIndex] = Color.BLACK;
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                    {
                        menuColors[introMenuIndex] = Color.GRAY;
                        if (introMenuIndex == 0)
                        {
                            introMenuIndex = menuColors.Length - 1;
                        }
                        else
                        {
                            introMenuIndex--;
                        }
                        menuColors[introMenuIndex] = Color.BLACK;
                    }
                    //Menu confirming
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        switch (introMenuIndex)
                        {
                            case 0:
                                gameState = "level" + level;
                                break;

                            case 1:
                                gameState = "controls";
                                break;
                            case 3:
                                Raylib.CloseWindow();
                                break;
                        }
                    }



                    //Drawing Introscreen
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);

                    //Title
                    Raylib.DrawText("Dimensional", 50, 100, 100, Color.BLACK);
                    Raylib.DrawText("Jumper", 50, 200, 100, Color.BLACK);

                    //The menu
                    Raylib.DrawText("Start", 50, 400, 64, menuColors[0]);
                    Raylib.DrawText("Controls", 50, 460, 64, menuColors[1]);
                    Raylib.DrawText("Level Select", 50, 520, 64, menuColors[2]);
                    Raylib.DrawText("Exit", 50, 580, 64, menuColors[3]);

                    Raylib.EndDrawing();
                }
                else if (gameState == "controls")
                {
                    //Logic Controlscreen
                    //Color Changes when you press the coresponding button
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                    {
                        wColor = Color.BLACK;
                    }
                    else
                    {
                        wColor = Color.GRAY;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                    {
                        aColor = Color.BLACK;
                    }
                    else
                    {
                        aColor = Color.GRAY;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                    {
                        dColor = Color.BLACK;
                    }
                    else
                    {
                        dColor = Color.GRAY;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                    {
                        spaceColor = Color.BLACK;
                    }
                    else
                    {
                        spaceColor = Color.GRAY;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
                    {
                        tabColor = Color.BLACK;
                    }
                    else
                    {
                        tabColor = Color.GRAY;
                    }
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                    {
                        gameState = "intro";
                    }


                    //Drawing Controlscreen
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);

                    //The controls
                    Raylib.DrawText("W: Jump", 50, 100, 64, wColor);
                    Raylib.DrawText("A: Walk Left", 50, 160, 64, aColor);
                    Raylib.DrawText("D: Walk Right", 50, 220, 64, dColor);
                    Raylib.DrawText("SPACE: Switch Dimensions", 50, 280, 64, spaceColor);
                    Raylib.DrawText("TAB: Pause / Controls", 50, 340, 64, tabColor);
                    Raylib.DrawText("BACKSPACE: Go back to game/menu", 50, 400, 64, Color.GRAY);

                    Raylib.EndDrawing();
                }
                else if (gameState == "level1")
                {
                    //Drawing level 1
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);

                    Raylib.DrawRectangleRec(playerRec, playerC);

                    Raylib.EndDrawing();
                }
                else
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);
                    Raylib.EndDrawing();
                }
            }
        }
    }
}


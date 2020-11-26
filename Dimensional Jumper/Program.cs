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
            string gameState = "intro";
            int level = 1;


            //Intro variables
            int introMenuIndex = 0;
            Color[] menuColors = { Color.BLACK, Color.GRAY, Color.GRAY };

            //Game variables
            Rectangle goalRec = new Rectangle(1700, 700, 100, 100);
            Player player = new Player(new Rectangle(100, 50, 50, 100), Color.RED);
            Platform[] platforms = new Platform[25];
            //Level one platforms
            platforms[0] = new Platform(new Rectangle(0, 200, windowX, 20), true);
            platforms[1] = new Platform(new Rectangle(0, 400, windowX, 20), false);
            platforms[2] = new Platform(new Rectangle(0, 600, windowX, 20), true);
            platforms[3] = new Platform(new Rectangle(0, 800, windowX, 20), false);
            //Level two platforms
            platforms[4] = new Platform(new Rectangle(0, 600, windowX, 400), true);
            platforms[5] = new Platform(new Rectangle(700, 0, 200, windowY), true);
            //Level three platforms
            platforms[6] = new Platform(new Rectangle(100, 900, 200, 50), true);
            platforms[7] = new Platform(new Rectangle(500, 650, 200, 50), false);
            platforms[8] = new Platform(new Rectangle(100, 400, 200, 50), true);
            platforms[9] = new Platform(new Rectangle(600, 200, 200, 50), false);
            platforms[10] = new Platform(new Rectangle(1200, 800, 200, 50), true);
            //Level four platforms
            platforms[11] = new Platform(new Rectangle(200, 200, 20, 400), true);
            platforms[12] = new Platform(new Rectangle(200, 200, 400, 20), true);
            platforms[13] = new Platform(new Rectangle(580, 200, 20, 400), true);
            platforms[14] = new Platform(new Rectangle(200, 600, 400, 20), true);
            platforms[15] = new Platform(new Rectangle(800, 200, 20, 400), true);
            platforms[16] = new Platform(new Rectangle(800, 200, 400, 20), true);
            platforms[17] = new Platform(new Rectangle(1180, 200, 20, 400), true);
            platforms[18] = new Platform(new Rectangle(800, 600, 400, 20), true);
            platforms[19] = new Platform(new Rectangle(1400, 200, 20, 400), true);
            platforms[20] = new Platform(new Rectangle(1400, 200, 400, 20), true);
            platforms[21] = new Platform(new Rectangle(1780, 200, 20, 400), true);
            platforms[22] = new Platform(new Rectangle(1400, 600, 400, 20), true);


            int[] platformStartIndexes = { 0, 0, 4, 6, 11, 23 };
            int frameCount = 0;
            int dimensionFlipFrame = -200;
            int deathCount = 0;
            int seconds = 0;
            int minutes = 0;


            //Raylib stuff
            Raylib.InitWindow(windowX, windowY, "Dimensional Jumper");
            Raylib.SetTargetFPS(60);

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
                                gameState = "game";
                                break;

                            case 2:
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
                    Raylib.DrawText("Play", 50, 400, 64, menuColors[0]);
                    Raylib.DrawText("Level Select", 50, 460, 64, menuColors[1]);
                    Raylib.DrawText("Exit", 50, 520, 64, menuColors[2]);

                    //Controls
                    Raylib.DrawText("W: Jump", 500, 400, 64, Color.GRAY);
                    Raylib.DrawText("A: Walk Left", 500, 460, 64, Color.GRAY);
                    Raylib.DrawText("D: Walk Right", 500, 520, 64, Color.GRAY);
                    Raylib.DrawText("SPACE: Switch Dimensions", 500, 580, 64, Color.GRAY);
                    Raylib.DrawText("TAB: Pause / Controls", 500, 640, 64, Color.GRAY);
                    Raylib.DrawText("ENTER: SELECT", 500, 700, 64, Color.GRAY);


                    Raylib.EndDrawing();
                }
                else if (gameState == "game")
                {
                    //Logic for the game
                    //Calculating the clock with framecount

                    frameCount++;
                    if ((frameCount % 60) == 0)
                    {
                        seconds++;
                        if ((seconds % 60) == 0)
                        {
                            minutes++;
                            seconds = 0;
                        }
                    }





                    //If you switched dimensions last frame and now collide, then you respawn
                    if (dimensionFlipFrame + 1 == frameCount)
                    {
                        for (int i = platformStartIndexes[level]; i < platformStartIndexes[level + 1]; i++)
                        {
                            if (Raylib.CheckCollisionRecs(player.rec, platforms[i].rec) && platforms[i].active)
                            {
                                deathCount++;
                                player.rec.x = player.startX;
                                player.rec.y = player.startY;
                                player.accY = 0;
                            }
                        }
                    }
                    //Function for the player movement
                    player.Update();

                    if (player.rec.y >= 1000 || player.rec.x >= 1920 || player.rec.x + player.rec.width <= 0)
                    {
                        deathCount++;
                        player.rec.x = player.startX;
                        player.rec.y = player.startY;
                        player.accY = 0;
                    }

                    //Check player and platform collision
                    for (int i = platformStartIndexes[level]; i < platformStartIndexes[level + 1]; i++)
                    {
                        player.Collision(platforms[i]);
                    }

                    //Check goal Collision
                    if (Raylib.CheckCollisionRecs(goalRec, player.rec))
                    {
                        if (level == 1)
                        {
                            level++;
                            player.startX = 100;
                            player.startY = 100;
                            player.rec.x = player.startX;
                            player.rec.y = player.startY;
                            player.accY = 0;
                            goalRec.x = 1750;
                            goalRec.y = 500;
                        }
                        else if (level == 2)
                        {
                            level++;

                            player.startX = 120;
                            player.startY = 600;
                            player.rec.x = player.startX;
                            player.rec.y = player.startY;
                            player.accY = 0;
                            goalRec.x = 1750;
                            goalRec.y = 800;
                        }
                        else if (level == 3)
                        {
                            level++;

                            player.startX = 400;
                            player.startY = 400;
                            player.rec.x = player.startX;
                            player.rec.y = player.startY;
                            player.accY = 0;
                            goalRec.x = 1550;
                            goalRec.y = 700;
                        }
                        else if (level == 4)
                        {
                            gameState = "finish";
                        }

                    }

                    //Check for menu controls
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                    {
                        gameState = "intro";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        for (int i = platformStartIndexes[level]; i < platformStartIndexes[level + 1]; i++)
                        {
                            platforms[i].changeDimension();
                        }
                        dimensionFlipFrame = frameCount;
                    }




                    //Drawing level 
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.BLACK);
                    Raylib.DrawText("Death Count: " + deathCount, 25, 25, 64, Color.WHITE);
                    Raylib.DrawText("Time: " + minutes + ":" + seconds, 1500, 25, 64, Color.WHITE);



                    //Draw the platforms
                    for (int i = platformStartIndexes[level]; i < platformStartIndexes[level + 1]; i++)
                    {
                        platforms[i].Draw();
                    }

                    //Draw the goal
                    Raylib.DrawRectangleRec(goalRec, Color.GREEN);

                    //Draw the player
                    player.Draw();

                    Raylib.EndDrawing();
                }
                else if (gameState == "finish")
                {
                    //Logic
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        gameState = "intro";
                        level = 1;
                    }
                    //Drawing
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);
                    Raylib.DrawText("You won b", 50, 50, 64, Color.WHITE);
                    Raylib.DrawText("Press enter to get to menu", 50, 300, 64, Color.WHITE);
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

    //Making a player class with a rectangle and a color
    class Player
    {
        public Rectangle rec;
        public Color c;
        public int xspeed = 15;

        public float oldX;

        public float oldY;

        public bool grounded = false;

        public int startX = 100;
        public int startY = 50;

        public int g = 3;
        public int accY = 0;

        public Player(Rectangle r, Color p)
        {
            this.rec = r;
            this.c = p;
        }

        public void Update()
        {
            //Stores old x and y levels for collision later
            oldX = this.rec.x;
            oldY = this.rec.y;

            //Reads the key inputs
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                rec.x -= xspeed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                rec.x += xspeed;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) && grounded)
            {
                accY = 40;
                grounded = false;
            }

            //Calculating gravity and vertical acceleration
            rec.y -= accY;
            accY -= g;

        }


        public void Collision(Platform other)
        {
            if (Raylib.CheckCollisionRecs(this.rec, other.rec) && other.active)
            {
                //Up
                if (oldY + this.rec.height <= other.rec.y)
                {
                    g = 0;
                    accY = 0;
                    grounded = true;
                    this.rec.y = other.rec.y - this.rec.height;
                }
                //Down
                else if (oldY >= other.rec.y + other.rec.height)
                {
                    this.rec.y = other.rec.y + other.rec.height;
                }
                //Left
                else if (oldX + this.rec.width <= other.rec.x)
                {
                    this.rec.x = other.rec.x - this.rec.width;
                }
                //Right
                else if (oldX >= other.rec.x + other.rec.width)
                {
                    this.rec.x = other.rec.x + other.rec.width;
                }
                else
                {
                    this.rec.y = other.rec.y - this.rec.height;
                }
            }
            else
            {
                g = 3;
            }

        }

        //Function for drawing the player
        public void Draw()
        {
            Raylib.DrawRectangleRec(this.rec, this.c);
        }
    }


    //A class for a platform with a rectangle avd a boolean for which "dimension" its in at the start
    class Platform
    {
        public Rectangle rec;

        public Color c;

        public bool active;

        public Platform(Rectangle r, bool ac)
        {
            this.rec = r;
            this.active = ac;
            if (this.active)
            {
                this.c = Color.PURPLE;
            }
            else
            {
                this.c = Color.GRAY;
            }
        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(this.rec, this.c);
        }

        public void changeDimension()
        {
            if (this.active)
            {
                this.active = false;
                this.c = Color.GRAY;
            }
            else
            {
                this.active = true;
                this.c = Color.VIOLET;
            }
        }
    }
}


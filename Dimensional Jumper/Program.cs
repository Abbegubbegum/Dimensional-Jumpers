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
            Platform[] platforms = new Platform[20];
            //Level one platforms
            platforms[0] = new Platform(new Rectangle(20, 200, 1900, 20), true);
            platforms[1] = new Platform(new Rectangle(20, 400, 1900, 20), false);
            platforms[2] = new Platform(new Rectangle(20, 600, 1900, 20), true);
            platforms[3] = new Platform(new Rectangle(20, 800, 1900, 20), false);

            int[] platformStartIndexes = { 0, 0, 4, };


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

                    Raylib.EndDrawing();
                }
                else if (gameState == "game")
                {
                    //Logic for the game
                    //Function for the player movement
                    player.Update();

                    //Check player and platform collision
                    for (int i = platformStartIndexes[level]; i < platformStartIndexes[level + 1]; i++)
                    {
                        player.Collision(platforms[i]);
                    }

                    //Check goal Collision
                    if (Raylib.CheckCollisionRecs(goalRec, player.rec))
                    {
                        gameState = "";

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
                    }

                    //Drawing level 1
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.BLACK);


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
                else
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);
                    Raylib.EndDrawing();
                }

            }
        }

        static void incrementLevel()
        {
            /*
            level = 2;
            player.startX = ;
            player.startY = ;
            player.rec.x = player.startX;
            player.rec.y = player.startY;
            player.accY = 0;
            goalRec.x = 800;
            goalRec.y = 800;
             */
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

            if (this.rec.y + this.rec.height > 1000)
            {
                this.rec.x = startX;
                this.rec.y = startY;
                accY = 0;
            }
        }


        public void Collision(Platform other)
        {
            if (Raylib.CheckCollisionRecs(this.rec, other.rec) && other.active)
            {
                g = 0;
                accY = 0;
                grounded = true;

                if (oldY + this.rec.height <= other.rec.y)
                {
                    this.rec.y = other.rec.y - this.rec.height;
                }
                else if (oldY >= other.rec.y + other.rec.height)
                {
                    this.rec.y = other.rec.y + other.rec.height;
                }
                else if (oldX + this.rec.width <= other.rec.x)
                {
                    this.rec.x = other.rec.x - this.rec.width;
                }
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


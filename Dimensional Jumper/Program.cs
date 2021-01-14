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
            string gamestate = "intro";
            int level = 1;

            //Raylib initialization
            Raylib.InitWindow(windowX, windowY, "Dimensional Jumper");
            Raylib.InitAudioDevice();
            Raylib.SetTargetFPS(60);

            //Intro variables
            int introMenuIndex = 0;
            Color[] menuColors = { Color.BLACK, Color.GRAY, Color.GRAY, Color.GRAY };
            int levelMenuIndex = 0;
            Color[] levelMenuColors = { Color.BLACK, Color.GRAY, Color.GRAY, Color.GRAY, Color.GRAY };

            string musicText = "Music ON";

            //Game variables
            Rectangle goalRec = new Rectangle(1700, 700, 100, 100);
            Player p = new Player();
            Platform[] platforms = new Platform[30];
            //Level one platforms
            platforms[0] = new Platform(new Rectangle(0, 200, windowX, 20), 1);
            platforms[1] = new Platform(new Rectangle(0, 400, windowX, 20), 2);
            platforms[2] = new Platform(new Rectangle(0, 600, windowX, 20), 1);
            platforms[3] = new Platform(new Rectangle(0, 800, windowX, 20), 2);
            //Level two platforms
            platforms[4] = new Platform(new Rectangle(0, 600, windowX, 400), 1);
            platforms[5] = new Platform(new Rectangle(700, 0, 200, windowY), 1);
            //Level three platforms
            platforms[6] = new Platform(new Rectangle(100, 900, 200, 50), 1);
            platforms[7] = new Platform(new Rectangle(500, 650, 200, 50), 2);
            platforms[8] = new Platform(new Rectangle(100, 400, 200, 50), 1);
            platforms[9] = new Platform(new Rectangle(600, 200, 200, 50), 2);
            platforms[10] = new Platform(new Rectangle(1200, 800, 200, 50), 1);
            //Level four platforms
            platforms[11] = new Platform(new Rectangle(200, 200, 20, 400), 1);
            platforms[12] = new Platform(new Rectangle(200, 200, 400, 20), 1);
            platforms[13] = new Platform(new Rectangle(580, 200, 20, 400), 1);
            platforms[14] = new Platform(new Rectangle(200, 600, 400, 20), 1);
            platforms[15] = new Platform(new Rectangle(800, 200, 20, 400), 1);
            platforms[16] = new Platform(new Rectangle(800, 200, 400, 20), 1);
            platforms[17] = new Platform(new Rectangle(1180, 200, 20, 400), 1);
            platforms[18] = new Platform(new Rectangle(800, 600, 400, 20), 1);
            platforms[19] = new Platform(new Rectangle(1400, 200, 20, 400), 1);
            platforms[20] = new Platform(new Rectangle(1400, 200, 400, 20), 1);
            platforms[21] = new Platform(new Rectangle(1780, 200, 20, 400), 1);
            platforms[22] = new Platform(new Rectangle(1400, 600, 400, 20), 1);
            //Level five platforms
            platforms[23] = new Platform(new Rectangle(200, 400, 200, 800), 1);
            platforms[24] = new Platform(new Rectangle(200, 600, 400, 600), 1);
            platforms[25] = new Platform(new Rectangle(200, 800, 600, 400), 1);
            platforms[26] = new Platform(new Rectangle(200, 1000, 800, 200), 1);
            platforms[27] = new Platform(new Rectangle(100, 960, 150, 20), 1);
            platforms[28] = new Platform(new Rectangle(100, 980, 800, 20), 2);

            Color gameBackground = Color.SKYBLUE;
            int[] platformStartIndexes = { 0, 4, 6, 11, 23, 29 };
            int dimension = 1;
            int frameCount = 0;
            int dimensionFlipFrame = -200;
            int deathCount = 0;
            int secondCount = 0;
            int minuteCount = 0;
            Sound song = Raylib.LoadSound("song.mp3");
            Raylib.SetMasterVolume(0.2f);
            bool musicToggle = true;


            while (!Raylib.WindowShouldClose())
            {
                if (!Raylib.IsSoundPlaying(song) && musicToggle)
                {
                    Raylib.PlaySound(song);
                }

                if (gamestate == "intro")
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
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        switch (introMenuIndex)
                        {
                            case 0:
                                gamestate = "game";
                                for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                                {
                                    platforms[i].checkDimension(dimension);
                                }
                                gameBackground = Color.SKYBLUE;


                                break;

                            case 1:
                                gamestate = "levelSelect";
                                break;

                            case 2:

                                if (musicToggle)
                                {
                                    Raylib.PauseSound(song);
                                    musicToggle = false;
                                    musicText = "Music OFF";
                                }
                                else
                                {
                                    Raylib.ResumeSound(song);
                                    musicToggle = true;
                                    musicText = "Music ON";
                                }
                                break;

                            case 3:
                                gamestate = "end";
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
                    Raylib.DrawText(musicText, 50, 520, 64, menuColors[2]);
                    Raylib.DrawText("Exit", 50, 580, 64, menuColors[3]);


                    //Controls
                    Raylib.DrawText("W: Jump", 500, 400, 64, Color.GRAY);
                    Raylib.DrawText("A: Walk Left", 500, 460, 64, Color.GRAY);
                    Raylib.DrawText("D: Walk Right", 500, 520, 64, Color.GRAY);
                    Raylib.DrawText("SPACE: Switch Dimensions", 500, 580, 64, Color.GRAY);
                    Raylib.DrawText("TAB: Pause / Controls", 500, 640, 64, Color.GRAY);
                    Raylib.DrawText("ENTER: SELECT", 500, 700, 64, Color.GRAY);


                    Raylib.EndDrawing();
                }
                else if (gamestate == "levelSelect")
                {

                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
                    {
                        levelMenuColors[levelMenuIndex] = Color.GRAY;
                        if (levelMenuIndex == levelMenuColors.Length - 1)
                        {
                            levelMenuIndex = 0;
                        }
                        else
                        {
                            levelMenuIndex++;
                        }
                        levelMenuColors[levelMenuIndex] = Color.BLACK;

                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                    {
                        levelMenuColors[levelMenuIndex] = Color.GRAY;
                        if (levelMenuIndex == 0)
                        {
                            levelMenuIndex = levelMenuColors.Length - 1;
                        }
                        else
                        {
                            levelMenuIndex--;
                        }
                        levelMenuColors[levelMenuIndex] = Color.BLACK;

                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        switch (levelMenuIndex)
                        {
                            case 0:
                                gamestate = "game";
                                level = 1;
                                dimension = 1;
                                p.startX = 100;
                                p.startY = 50;
                                p.rec.x = p.startX;
                                p.rec.y = p.startY;
                                p.accY = 0;
                                goalRec.x = 1700;
                                goalRec.y = 700;
                                for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                                {
                                    platforms[i].checkDimension(dimension);
                                }
                                gameBackground = Color.SKYBLUE;
                                break;

                            case 1:
                                gamestate = "game";
                                level = 2;
                                p.startX = 100;
                                p.startY = 100;
                                p.rec.x = p.startX;
                                p.rec.y = p.startY;
                                p.accY = 0;
                                goalRec.x = 1750;
                                goalRec.y = 500;
                                dimension = 1;
                                for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                                {
                                    platforms[i].checkDimension(dimension);
                                }
                                gameBackground = Color.SKYBLUE;
                                break;

                            case 2:
                                gamestate = "game";
                                level = 3;
                                p.startX = 120;
                                p.startY = 600;
                                p.rec.x = p.startX;
                                p.rec.y = p.startY;
                                p.accY = 0;
                                goalRec.x = 1750;
                                goalRec.y = 800;
                                dimension = 1;
                                for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                                {
                                    platforms[i].checkDimension(dimension);
                                }
                                gameBackground = Color.SKYBLUE;
                                break;

                            case 3:
                                gamestate = "game";
                                level = 4;
                                p.startX = 400;
                                p.startY = 400;
                                p.rec.x = p.startX;
                                p.rec.y = p.startY;
                                p.accY = 0;
                                goalRec.x = 1550;
                                goalRec.y = 700;
                                dimension = 1;
                                for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                                {
                                    platforms[i].checkDimension(dimension);
                                }
                                gameBackground = Color.SKYBLUE;
                                break;
                            case 4:
                                gamestate = "intro";
                                break;
                        }
                    }
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);

                    Raylib.DrawText("Level 1", 50, 400, 64, levelMenuColors[0]);
                    Raylib.DrawText("Level 2", 50, 460, 64, levelMenuColors[1]);
                    Raylib.DrawText("Level 3", 50, 520, 64, levelMenuColors[2]);
                    Raylib.DrawText("Level 4", 50, 580, 64, levelMenuColors[3]);
                    Raylib.DrawText("Back", 50, 640, 64, levelMenuColors[4]);

                    Raylib.EndDrawing();
                }
                else if (gamestate == "game")
                {
                    //Logic for the game
                    //Calculating the clock with framecount

                    frameCount++;
                    if ((frameCount % 60) == 0)
                    {
                        secondCount++;
                        if ((secondCount % 60) == 0)
                        {
                            minuteCount++;
                            secondCount = 0;
                        }
                    }





                    //If you switched dimensions last frame and now collide, then you die
                    if (dimensionFlipFrame + 1 == frameCount)
                    {
                        for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                        {
                            if (Raylib.CheckCollisionRecs(p.rec, platforms[i].rec) && platforms[i].active)
                            {
                                deathCount++;
                                p.rec.x = p.startX;
                                p.rec.y = p.startY;
                                p.accY = 0;
                            }
                        }
                        //Because you cant be grounded after, this helps so you cant jump mid-air
                        if (p.grounded == true)
                        {
                            p.grounded = false;
                        }
                    }
                    //Function for the player movement
                    p.Update();

                    if (p.rec.y >= 1000 || p.rec.x >= 1920 || p.rec.x + p.rec.width <= 0)
                    {
                        deathCount++;
                        p.rec.x = p.startX;
                        p.rec.y = p.startY;
                        p.accY = 0;
                    }

                    //Check player and platform collision
                    for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                    {
                        p.Collision(platforms[i]);
                    }

                    //Check goal Collision
                    if (Raylib.CheckCollisionRecs(goalRec, p.rec))
                    {
                        if (level == 1)
                        {
                            dimension = 1;
                            for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                            {
                                platforms[i].checkDimension(dimension);
                            }
                            gameBackground = Color.SKYBLUE;

                            level++;
                            p.startX = 100;
                            p.startY = 100;
                            p.rec.x = p.startX;
                            p.rec.y = p.startY;
                            p.accY = 0;
                            goalRec.x = 1750;
                            goalRec.y = 500;
                        }
                        else if (level == 2)
                        {
                            level++;
                            dimension = 1;
                            for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                            {
                                platforms[i].checkDimension(dimension);
                            }
                            gameBackground = Color.SKYBLUE;


                            p.startX = 120;
                            p.startY = 600;
                            p.rec.x = p.startX;
                            p.rec.y = p.startY;
                            p.accY = 0;
                            goalRec.x = 1750;
                            goalRec.y = 800;
                        }
                        else if (level == 3)
                        {
                            level++;
                            dimension = 1;
                            for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                            {
                                platforms[i].checkDimension(dimension);
                            }
                            gameBackground = Color.SKYBLUE;

                            p.startX = 400;
                            p.startY = 400;
                            p.rec.x = p.startX;
                            p.rec.y = p.startY;
                            p.accY = 0;
                            goalRec.x = 1550;
                            goalRec.y = 700;
                        }
                        else if (level == 4)
                        {
                            level++;
                            dimension = 1;
                            for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                            {
                                platforms[i].checkDimension(dimension);
                            }
                            gameBackground = Color.SKYBLUE;
                            p.startX = 125;
                            p.startY = 800;
                            p.rec.x = p.startX;
                            p.rec.y = p.startY;
                            p.accY = 0;
                            goalRec.x = 200;
                            goalRec.y = 100;
                        }
                        else if (level == 5)
                        {
                            gamestate = "finish";
                        }

                    }

                    //Check for menu controls
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB))
                    {
                        gamestate = "intro";
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        if (dimension == 1)
                        {
                            gameBackground = new Color(15, 15, 15, 255);
                            dimension = 2;
                        }
                        else
                        {
                            gameBackground = Color.SKYBLUE;
                            dimension = 1;
                        }
                        for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                        {
                            platforms[i].checkDimension(dimension);
                        }



                        dimensionFlipFrame = frameCount;
                    }




                    //Drawing level 
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(gameBackground);
                    Raylib.DrawText("Death Count: " + deathCount, 25, 25, 64, Color.WHITE);
                    Raylib.DrawText("Time: " + minuteCount + ":" + secondCount, 1500, 25, 64, Color.WHITE);



                    //Draw the platforms
                    for (int i = platformStartIndexes[level - 1]; i < platformStartIndexes[level]; i++)
                    {
                        platforms[i].Draw();
                    }

                    //Draw the goal
                    Raylib.DrawRectangleRec(goalRec, Color.DARKGREEN);

                    //Draw the player
                    p.Draw();

                    Raylib.EndDrawing();
                }
                else if (gamestate == "finish")
                {
                    //Logic
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        gamestate = "intro";
                        level = 1;
                        dimension = 1;
                        p.startX = 100;
                        p.startY = 50;
                        p.rec.x = p.startX;
                        p.rec.y = p.startY;
                        deathCount = 0;
                        secondCount = 0;
                        minuteCount = 0;

                    }
                    //Drawing
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.PURPLE);
                    Raylib.DrawText("You won b", 50, 50, 64, Color.WHITE);
                    Raylib.DrawText("Time: " + minuteCount + ":" + secondCount, 450, 50, 64, Color.WHITE);
                    Raylib.DrawText("Deaths: " + deathCount, 800, 50, 64, Color.WHITE);

                    Raylib.DrawText("Press enter to get to menu", 50, 300, 64, Color.WHITE);
                    Raylib.EndDrawing();
                }

                else if (gamestate == "end")
                {
                    Raylib.CloseWindow();
                }

                else
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.WHITE);
                    Raylib.EndDrawing();
                }

            }

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();

        }



    }

    //Making a player class with a rectangle and a color
    class Player
    {
        public Rectangle rec = new Rectangle(100, 50, 50, 100);
        public Color c = Color.RED;
        public int xspeed = 15;

        public float oldX;

        public float oldY;

        public bool grounded = false;

        public int startX = 100;
        public int startY = 50;

        public int g = 3;
        public int accY = 0;

        public void Update()
        {
            //Stores old x and y levels for collision later
            oldX = this.rec.x;
            oldY = this.rec.y;

            //Reads the key inputs
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                this.rec.x -= xspeed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                this.rec.x += xspeed;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) && grounded)
            {
                accY = 40;
                this.grounded = false;
            }

            //Calculating gravity and vertical acceleration
            this.rec.y -= accY;
            accY -= g;


        }


        public void Collision(Platform platform)
        {
            if (Raylib.CheckCollisionRecs(this.rec, platform.rec) && platform.active)
            {
                //Up
                if (oldY + this.rec.height <= platform.rec.y)
                {
                    g = 0;
                    accY = 0;
                    grounded = true;
                    this.rec.y = platform.rec.y - this.rec.height;
                }
                //Down
                else if (oldY >= platform.rec.y + platform.rec.height)
                {
                    this.rec.y = platform.rec.y + platform.rec.height;
                }
                //Left
                else if (oldX + this.rec.width <= platform.rec.x)
                {
                    this.rec.x = platform.rec.x - this.rec.width;
                }
                //Right
                else if (oldX >= platform.rec.x + platform.rec.width)
                {
                    this.rec.x = platform.rec.x + platform.rec.width;
                }
                else
                {
                    this.rec.y = platform.rec.y - this.rec.height;
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

        public int dimensionActive;

        public Platform(Rectangle r, int d)
        {
            this.rec = r;
            this.dimensionActive = d;
            this.checkDimension(1);
        }

        public void Draw()
        {
            if (this.active)
            {
                this.c = Color.PURPLE;
            }
            else
            {
                this.c = Color.GRAY;
            }

            Raylib.DrawRectangleRec(this.rec, this.c);
        }

        public void checkDimension(int dimension)
        {
            if (this.dimensionActive == dimension)
            {
                this.active = true;
            }
            else
            {
                this.active = false;
            }
        }
    }
}



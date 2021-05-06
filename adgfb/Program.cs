using System;
using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;


namespace fghbj
{
    class Program
    {

        static void Main(string[] args)
        {

            Raylib.InitWindow(1000, 700, "CUBE");

            Raylib.SetTargetFPS(60);


            Rectangle block = new Rectangle(100, 50, 50, 50);
            Rectangle worldMap = new Rectangle(-200, -200, 900, 700);
            Rectangle avatar = new Rectangle(400 - 20, 300 - 20, 40, 40);
            Rectangle textBox = new Rectangle(150, -55, 250, 50);
            float timerMaxValue = 60;
            float timerCurrentValue = timerMaxValue;

            int MAX_INPUT_CHARS = 9;
            int letterCount = 0;
            List<char> name = new List<char>();

            bool mouseOnText = false;
            bool walkA = true;
            bool walkS = true;
            bool walkD = true;
            bool walkW = true;

            Camera2D camera = new Camera2D();

            camera.zoom = 1;
            //camera.target = new Vector2(400, 300);

            while (!Raylib.WindowShouldClose())
            {
                // Logic

                if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && walkA == true)
                {
                    avatar.x -= 3f;
                    camera.offset.X += 3f;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && walkD == true)
                {
                    avatar.x += 3f;
                    camera.offset.X -= 3f;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && walkS == true)
                {
                    avatar.y += 3f;
                    camera.offset.Y -= 3f;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && walkW == true)
                {
                    avatar.y -= 3f;
                    camera.offset.Y += 3f;
                }

                if (avatar.x > 662)
                {
                    avatar.x = avatar.x - 3;
                    camera.offset.X = camera.offset.X + 3;
                }

                if (avatar.x < -201)
                {
                    avatar.x = avatar.x + 3;
                    camera.offset.X = camera.offset.X - 3;
                }

                if (avatar.y < -200)
                {
                    avatar.y = avatar.y + 3;
                    camera.offset.Y = camera.offset.Y - 3;
                }

                if (avatar.y > 460)
                {
                    avatar.y = avatar.y - 3;
                    camera.offset.Y = camera.offset.Y + 3;
                }


                bool areOverlapping = Raylib.CheckCollisionRecs(block, avatar);

                // Drawing
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                Raylib.BeginMode2D(camera);
                Raylib.DrawRectangleRec(worldMap, Color.WHITE);
                Raylib.DrawRectangleRec(block, Color.RED);
                Raylib.DrawRectangleRec(avatar, Color.ORANGE);
                if (areOverlapping == true)

                {
                    walkA = false;
                    walkD = false;
                    walkS = false;
                    walkW = false;

                    Raylib.DrawText("... oh hey kid, wait you are not supposed to be here", -180, -100, 30, Color.RED);
                    Raylib.DrawText("who are you?", -100, -50, 30, Color.RED);
                    Raylib.DrawRectangleRec(textBox, Color.GRAY);
                    if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), textBox)) mouseOnText = true;
                    else mouseOnText = false;


                    {


                        // Get char pressed (unicode character) on the queue
                        int key = Raylib.GetCharPressed();

                        // Check if more characters have been pressed on the same frame
                        while (key > 0)
                        {
                            //NOTE: Only allow keys in range [32..125]
                            if ((key >= 32) && (key <= 125) && (name.Count < MAX_INPUT_CHARS))
                            {

                                name.Add((char)key);

                            }

                            key = Raylib.GetCharPressed();  // Check next character in the queue
                        }

                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                        {

                            if (name.Count > 0) name.RemoveAt(name.Count - 1);

                        }
                    }



                    string namestr = new string(name.ToArray());
                    Raylib.DrawText(namestr, 170, -55, 40, Color.RED);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {

                        int xA = (int)avatar.x;

                        int yA = (int)avatar.y;
                        Raylib.DrawText(namestr, xA + 20, yA - 40, 30, Color.YELLOW);
                    }

                    timerCurrentValue -= Raylib.GetFrameTime();
                    if (timerCurrentValue < 0)
                    {

                    }
                }
                Raylib.EndMode2D();

                Raylib.EndDrawing();

            }


        }
    }
}

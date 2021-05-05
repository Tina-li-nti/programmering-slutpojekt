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

            int MAX_INPUT_CHARS = 9;
            // char name[MAX_INPUT_CHARS + 1] = "\0";      // NOTE: One extra space required for line ending char '\0'
            int letterCount = 0;
            List<char> name = new List<char>();

            bool mouseOnText = false;

            Camera2D camera = new Camera2D();

            camera.zoom = 1;
            //camera.target = new Vector2(400, 300);

            while (!Raylib.WindowShouldClose())
            {
                // Logic

                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    avatar.x -= 3f;
                    camera.offset.X += 3f;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    avatar.x += 3f;
                    camera.offset.X -= 3f;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    avatar.y += 3f;
                    camera.offset.Y -= 3f;
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
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
                    Raylib.DrawText("... oh hey kid, wait you are not supposed to be here", -180, -100, 30, Color.RED);
                    Raylib.DrawText("who are you?", -100, -50, 30, Color.RED);
                    Raylib.DrawRectangleRec(textBox, Color.GRAY);
                    if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), textBox)) mouseOnText = true;
                    else mouseOnText = false;

                    //  if (mouseOnText)
                    {
                        // Set the window's cursor to the I-Beam
                        // Raylib.SetMouseCursor(MOUSE_CURSOR_IBEAM);

                        // Get char pressed (unicode character) on the queue
                        int key = Raylib.GetCharPressed();

                        // Check if more characters have been pressed on the same frame
                        while (key > 0)
                        {
                            //NOTE: Only allow keys in range [32..125]
                            if ((key >= 32) && (key <= 125) && (name.Count < MAX_INPUT_CHARS))
                            {
                                // name[letterCount] = (char)key;
                                name.Add((char)key);
                                //letterCount++;
                            }

                            key = Raylib.GetCharPressed();  // Check next character in the queue
                        }

                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                        {
                            //letterCount--;
                            if (name.Count > 0) name.RemoveAt(name.Count - 1);
                            //name[letterCount] = '\0';
                        }
                    }
                    //else Raylib.SetMouseCursor(MOUSE_CURSOR_DEFAULT);


                    string namestr = new string(name.ToArray());
                    Raylib.DrawText(namestr, 170, -55, 40, Color.RED);

                }
                Raylib.EndMode2D();

                Raylib.EndDrawing();

            }


        }
    }
}

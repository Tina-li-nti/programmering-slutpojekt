using System;
using System.Numerics;
using Raylib_cs;
using System.Collections.Generic;


namespace fghbj
{
    class Program
    {
        // Detta är en metod som säger åt användaren att han har gjort fel.
        static void thisIsNotTheWASDKeys()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                Raylib.DrawText("no i said the WASD keys dummy", 100, 150, 30, Color.PINK);
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                Raylib.DrawText("no i said the WASD keys dummy", 100, 150, 30, Color.PINK);
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                Raylib.DrawText("no i said the WASD keys dummy", 100, 150, 30, Color.PINK);
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                Raylib.DrawText("no i said the WASD keys dummy", 100, 150, 30, Color.PINK);
            }

        }
        static void Main(string[] args)
        {
            //fönsteret
            Raylib.InitWindow(1000, 700, "CUBE");
            //antal fps
            Raylib.SetTargetFPS(60);

            //den röda karatären
            Rectangle block = new Rectangle(100, 50, 50, 50);
            //mappen
            Rectangle worldMap = new Rectangle(-200, -200, 900, 700);
            //avataren som man rör på
            Rectangle avatar = new Rectangle(400 - 20, 300 - 20, 40, 40);
            //pratbubblan
            Rectangle textBox = new Rectangle(150, -55, 250, 50);

            //detta användes aldrig, men skulle ha varit för att ta main karaktären till en annan värld. 
            float timerMaxValue = 60;
            float timerCurrentValue = timerMaxValue;
            //detta är för namnet
            int MAX_INPUT_CHARS = 9;
            List<char> name = new List<char>();
            //även för namnet, står att den inte användes men namn input funkar inte utan den. Kollar man dessutom längre ner används den
           
            bool mouseOnText = false;
            
            //för att kunna gå när karaktärn ska göra de.
            bool walkA = true;
            bool walkS = true;
            bool walkD = true;
            bool walkW = true;
            //kameran som följer karaktären.
            Camera2D camera = new Camera2D();


            camera.zoom = 1;
            //camera.target = new Vector2(400, 300);



            while (!Raylib.WindowShouldClose())
            {

                //kollar kolllision för mina karaktärer
                bool areOverlapping = Raylib.CheckCollisionRecs(block, avatar);

                // Raylib Drawing
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                Raylib.BeginMode2D(camera);
                Raylib.DrawRectangleRec(worldMap, Color.WHITE);
                Raylib.DrawText("use WASD to move", 100, 100, 50, Color.BLACK);
                Raylib.DrawRectangleRec(block, Color.RED);
                Raylib.DrawRectangleRec(avatar, Color.ORANGE);

                thisIsNotTheWASDKeys();

                //detta är för att min karaktär ska kunna gå under the drawing
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


                //detta är för när karaktärerna kolliderar, då ska man så still under sekvensen.
                if (areOverlapping == true)

                {
                    walkA = false;
                    walkD = false;
                    walkS = false;
                    walkW = false;
                    Raylib.DrawText("type your name and press enter", -150, -200, 20, Color.BLACK);
                    //"prat" sekvensen
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
                        //detta är för att namnet ska kunna visas på skrämen
                        string namestr = new string(name.ToArray());
                        Raylib.DrawText(namestr, 170, -55, 40, Color.RED);

                        // så att karaktären ska gå efter prat sekevensen
                        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
                        {
                            areOverlapping = false;
                            walkA = true;
                            walkD = true;
                            walkS = true;
                            walkW = true;
                            mouseOnText = false;

                            //namnet ska visas över karaktären.
                            int xA = (int)avatar.x;
                            int yA = (int)avatar.y;
                            Raylib.DrawText(namestr, xA + 20, yA - 40, 30, Color.ORANGE);

                        }


                    }


                }
                Raylib.EndMode2D();

                Raylib.EndDrawing();

            }


        }
    }
}

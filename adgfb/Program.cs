using System;
using System.Numerics;
using Raylib_cs;

namespace fghbj
{
    class Program
    {
        static void Main(string[] args)
        {

            Raylib.InitWindow(800, 600, "CUBE");

            Raylib.SetTargetFPS(60);

            Rectangle block = new Rectangle(100, 50, 50, 50);
            Rectangle worldMap = new Rectangle(-200, -200, 900, 700);
            Rectangle avatar = new Rectangle(400 - 20, 300 - 20, 40, 40);
            bool areOverlapping = Raylib.CheckCollisionRecs(block, avatar);
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

                if (areOverlapping == true)
                {
                    Raylib.DrawText("Hello World", 100, 50, 20, Color.ORANGE);
                }


                // Drawing
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                Raylib.BeginMode2D(camera);
                Raylib.DrawRectangleRec(worldMap, Color.WHITE);
                Raylib.DrawRectangleRec(block, Color.RED);
                Raylib.DrawRectangleRec(avatar, Color.ORANGE);
                Raylib.EndMode2D();

                Raylib.EndDrawing();

            }


        }
    }
}

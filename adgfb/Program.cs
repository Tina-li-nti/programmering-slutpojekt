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

            Rectangle block = new Rectangle(50, 50, 50, 50);
            Rectangle avatar = new Rectangle(400 - 20, 300 - 20, 40, 40);

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


                // Drawing
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                Raylib.BeginMode2D(camera);
                Raylib.DrawRectangleRec(block, Color.RED);
                Raylib.DrawRectangleRec(avatar, Color.ORANGE);
                Raylib.EndMode2D();

                Raylib.EndDrawing();

            }


        }
    }
}

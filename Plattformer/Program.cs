﻿
using System.Numerics;
using Raylib_cs;
//width and height of window
int width = 1200;
int height = 900;

//create window and set target FPS
Raylib.InitWindow(width, height, "Plattformer");
//set fps to 60
Raylib.SetTargetFPS(60);

//create and start gameManager
Gamemanager gm = new();
//play until closed
while (Raylib.WindowShouldClose() == false)
{
    gm.Update();
    gm.Draw();
}

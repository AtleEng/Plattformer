using System.Numerics;
using Raylib_cs;
public class Block : GameObject
{
    public Block(string name, Gamemanager gm, Vector2 pos)
    {
        this.name = name;
        this.gm = gm;
        this.hitbox = new Rectangle(pos.X * gm.tile, pos.Y * gm.tile, gm.tile, gm.tile);

        spriteLocation = new Rectangle(0, 8, 8, 8);
    }
}

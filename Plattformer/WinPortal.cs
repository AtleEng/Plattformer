using System.Numerics;
using Raylib_cs;
public class WinPortal : GameObject
{
    public WinPortal(Gamemanager gm, Vector2 pos)
    {
        name = "Portal";
        this.gm = gm;
        this.hitbox = new Rectangle(pos.X * gm.tile, pos.Y * gm.tile, gm.tile, gm.tile);
        spriteLocation = new Rectangle(8, 8, 8, 8);
        tag = Tag.portal;

        color = Color.YELLOW;
    }
}
using System.Numerics;
using Raylib_cs;
public class WinPortal : GameObject
{
    public WinPortal(Gamemanager gm, Vector2 pos)
    {
        name = "Portal";
        this.gm = gm;
        this.hitbox = new Rectangle(pos.X * gm.tile + gm.tile / 8, pos.Y * gm.tile + gm.tile / 8, gm.tile * 0.75f, gm.tile * 0.75f);
        spriteLocation = new Rectangle(8, 8, 8, 8);
        tag = Tag.portal;

        color = Color.YELLOW;
    }
}
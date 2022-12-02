using System.Numerics;
using Raylib_cs;
public class Level
{
    /*Level is 16 * 12 tiles
    0 == ground
    1 == air
    2 == playerStartPos
    3 == portal
    4 == walking enemy
    5 == Jumping enemy
    6 == Buzzsaw
     */
    Gamemanager gm;
    public Level(Gamemanager gm)
    {
        this.gm = gm;
        alllevels.Add(level1);
    }
    public int gridWidth = 16;
    public int gridHeight = 12;

    List<int[,]> alllevels = new();
    List<GameObject> objectsInCurrentLevel = new();
    int[,] level1 = new int[12, 16]
    {
        //{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15}
    /* 0*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 1*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 2*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 3*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 4*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 5*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 6*/{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    /* 7*/{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    /* 8*/{ 1, 0, 0, 0, 2, 0, 0, 2, 0, 3, 0, 3, 3, 2, 0, 1},
    /* 9*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    /*10*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    /*11*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };

    public void ChangeLevel(int index)
    {
        if (index > alllevels.Count || index < 0) { return; }
        LoadLevel(alllevels[index]);
    }
    public void LoadLevel(int[,] level)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (level[y, x] == 1)
                {
                    objectsInCurrentLevel.Add(new Block("Block", gm, new Vector2(x, y)));
                }
                else if (level[y, x] == 2)
                {
                    objectsInCurrentLevel.Add(new Enemy("WalkingEnemy", gm, new Vector2(x, y), new Rectangle(x * gm.tile, y * gm.tile, gm.tile * 0.75f, gm.tile * 0.75f), new Rectangle(8, 0, 8, 8), 1, 0));
                }
                else if (level[y, x] == 3)
                {
                    objectsInCurrentLevel.Add(new Enemy("JumpingEnemy", gm, new Vector2(x, y), new Rectangle(x * gm.tile, y * gm.tile, gm.tile * 0.75f, gm.tile * 0.75f), new Rectangle(16, 0, 8, 8), 2, 13));
                }
            }
        }
    }
}
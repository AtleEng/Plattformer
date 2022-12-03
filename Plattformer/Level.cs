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
        alllevels.Add(level1); alllevels.Add(level2);
    }
    public int gridWidth = 16;
    public int gridHeight = 12;

    public int currentLevel = 1;
    public List<int[,]> alllevels = new();

    public List<GameObject> allObjects = new();
    public List<GameObject> allBlocks = new();
    public List<GameObject> allEnemies = new();
    public WinPortal portal;
    public Player player;
    int[,] level1 = new int[12, 16]
    {
        //{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15}
    /* 0*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    /* 1*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    /* 2*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    /* 3*/{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    /* 4*/{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    /* 5*/{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    /* 6*/{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    /* 7*/{ 1, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0,-2, 0, 1},
    /* 8*/{ 1, 0,-1, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 1},
    /* 9*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    /*10*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    /*11*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };
    int[,] level2 = new int[12, 16]
    {
        //{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15}
    /* 0*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 1*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 2*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 3*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-2, 0, 0, 0, 0},
    /* 4*/{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 5*/{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    /* 6*/{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 1},
    /* 7*/{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 1},
    /* 8*/{ 1, 0, 0, 0, 2, 0, 0, 2, 0, 3, 0, 3, 3, 2, 4, 1},
    /* 9*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    /*10*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    /*11*/{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    };

    public void ReloadLevel()
    {
        ChangeLevel(currentLevel);
    }
    public void ChangeLevel(int index)
    {
        currentLevel = index;

        ClearLevel();
        //check if level exist then load
        if (index > alllevels.Count || index < 0) { return; }
        LoadLevel(alllevels[index]);
    }
    public void ClearLevel()
    {
        //clear level
        allObjects.Clear();
        allBlocks.Clear();
        allEnemies.Clear();
        player = null;
        portal = null;
    }
    public void LoadLevel(int[,] level)
    {
        //loop throu all positions then add objects acording to number index
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (level[y, x] == -2)
                {
                    portal = new WinPortal(gm, new Vector2(x, y));
                }
                else if (level[y, x] == -1)
                {
                    player = new Player("Player", gm, new Vector2(x, y));
                }
                else if (level[y, x] == 1)
                {
                    allBlocks.Add(new Block("Block", gm, new Vector2(x, y)));
                }
                else if (level[y, x] == 2)
                {
                    allEnemies.Add(new Enemy("WalkingEnemy", gm, new Vector2(x, y), new Rectangle(x * gm.tile, y * gm.tile, gm.tile * 0.75f, gm.tile * 0.75f), new Rectangle(8, 0, 8, 8), 1, 0, false));
                }
                else if (level[y, x] == 3)
                {
                    allEnemies.Add(new Enemy("JumpingEnemy", gm, new Vector2(x, y), new Rectangle(x * gm.tile, y * gm.tile, gm.tile * 0.75f, gm.tile * 0.75f), new Rectangle(16, 0, 8, 8), 2, 13, false));
                }
                else if (level[y, x] == 4)
                {
                    allEnemies.Add(new Enemy("StaticEnemy", gm, new Vector2(x, y), new Rectangle(x * gm.tile, y * gm.tile, gm.tile * 0.75f, gm.tile * 0.75f), new Rectangle(16, 8, 8, 8), 0, 0, true));
                }
            }
        }
        foreach (Block block in allBlocks)
        {
            allObjects.Add(block);
        }
        foreach (Enemy enemmy in allEnemies)
        {
            allObjects.Add(enemmy);
        }
        if (player != null) { allObjects.Add(player); }
        if (portal != null) { allObjects.Add(portal); }
    }
}
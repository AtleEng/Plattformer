using System.Numerics;
using Raylib_cs;
public class Gamemanager
{
    public int tile = 75;
    public Level levelManager;
    //Loading in spriteSheet
    Texture2D spriteSheet = Raylib.LoadTexture(@"RaylibPlattformer.png");
    //Set the state of the game to start
    public GameStates gameState = GameStates.startScreen;
    public List<GameObject> allObjects = new();
    //actives ones at the begining of the program
    public Gamemanager() { levelManager = new Level(this); }

    //updates every frame
    public void Update()
    {
        allObjects = levelManager.allObjects;
        if (gameState == GameStates.startScreen)
        {
            Start();
        }
        else if (gameState == GameStates.playing)
        {
            Playing();
        }
        else if (gameState == GameStates.win)
        {
            Win();
        }
        else if (gameState == GameStates.dead)
        {
            Dead();
        }
        else if (gameState == GameStates.clearedLevel)
        {
            Clearedlevel();
        }
    }
    private void Start()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
        {
            StartTheGame();
        }
    }
    private void Playing()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
        {
            levelManager.ChangeLevel(0);
        }
        levelManager.player.Update();
        foreach (Enemy enemy in levelManager.allEnemies)
        {
            enemy.Update();
        }
    }
    private void Win()
    {
        System.Console.WriteLine("YOU WIN");
    }
    private void Dead()
    {
        levelManager.ReloadLevel();
        gameState = GameStates.playing;
    }

    private void Clearedlevel()
    {
        if (levelManager.currentLevel >= levelManager.alllevels.Count - 1)
        {
            levelManager.ClearLevel();
            gameState = GameStates.win;
        }
        else
        {
            levelManager.ChangeLevel(levelManager.currentLevel + 1);
            gameState = GameStates.playing;
        }
    }
    public void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(new Color(255, 205, 117, 255));
        foreach (GameObject gO in levelManager.allObjects)
        {
            bool shouldFlip = gO.isFacingRight;
            //Draw the hitbox
            Raylib.DrawRectangle((int)gO.hitbox.x, (int)gO.hitbox.y, (int)gO.hitbox.width, (int)gO.hitbox.height, Color.LIME);
            //Draw the texture
            if (!shouldFlip)
            {
                Raylib.DrawTexturePro(spriteSheet, gO.spriteLocation, new Rectangle(gO.hitbox.x + (gO.hitbox.width - tile) / 2, gO.hitbox.y + (gO.hitbox.height - tile) / 2, tile, tile), new Vector2(), 0, Color.WHITE);
            }
            else
            {
                Raylib.DrawTexturePro(spriteSheet, new Rectangle(gO.spriteLocation.x, gO.spriteLocation.y, -gO.spriteLocation.width, gO.spriteLocation.height), new Rectangle(gO.hitbox.x + (gO.hitbox.width - tile) / 2, gO.hitbox.y + (gO.hitbox.height - tile) / 2, tile, tile), new Vector2(), 0, Color.WHITE);
            }

        }

        Raylib.EndDrawing();
    }
    //when the "real" game start 
    void StartTheGame()
    {
        gameState = GameStates.playing;
        System.Console.WriteLine("Game has started");

        levelManager.ChangeLevel(0);

        //splayer = new Player("Player", this, new Vector2(3, 3));
    }
    public enum GameStates
    {
        startScreen, playing, win, dead, clearedLevel
    }
}
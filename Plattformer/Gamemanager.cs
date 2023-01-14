using System.Numerics;
using Raylib_cs;
public class Gamemanager
{
    //size of tiles in pixels
    public int tile = 75;
    public Level levelManager;
    //Loading in spriteSheet
    Texture2D spriteSheet = Raylib.LoadTexture(@"RaylibPlattformer.png");
    //Set the state of the game to start
    public GameStates gameState = GameStates.startScreen;
    public List<GameObject> allObjects = new();
    public event Action OnUppdate;
    public event Action OnReloadLevel;

    //actives once at the begining of the program
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
        Raylib.DrawText($"Plattformer", 100, 200, 150, Color.WHITE);
        Raylib.DrawText($"Made by: Atle Engelbrektsson", 100, 325, 25, Color.WHITE);
        Raylib.DrawText($"Press S to start", 100, 600, 50, Color.WHITE);

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
        {
            gameState = GameStates.playing;
            System.Console.WriteLine("Game has started");

            levelManager.ChangeLevel(0);
        }
    }
    private void Playing()
    {
        //if r is pressed restart level
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
        {
            OnReloadLevel.Invoke();
            levelManager.ReloadLevel();
            gameState = GameStates.playing;
        }
        //Active OnUppdate for all objects
        OnUppdate.Invoke();
    }
    private void Win()
    {
        System.Console.WriteLine("YOU WIN");
        Raylib.DrawText($"You win", 100, 200, 150, Color.WHITE);
        Raylib.DrawText($"Press S to exit to startscreen", 100, 600, 50, Color.WHITE);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
        {
            gameState = GameStates.startScreen;
        }
    }
    private void Dead()
    {
        OnReloadLevel.Invoke();
        levelManager.ReloadLevel();
        gameState = GameStates.playing;
    }
    private void Clearedlevel()
    {
        OnReloadLevel.Invoke();
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
            //Raylib.DrawRectangle((int)gO.hitbox.x, (int)gO.hitbox.y, (int)gO.hitbox.width, (int)gO.hitbox.height, Color.LIME);
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
        //Game Text
        if (gameState == GameStates.playing)
        {
            Raylib.DrawText($"{levelManager.currentLevel + 1}", 20, 20, 100, Color.WHITE);

            if (levelManager.currentLevel == 1)
            {
                Raylib.DrawText($"Press R to restart", 100, 350, 20, Color.WHITE);
            }
            else if (levelManager.currentLevel == 0)
            {
                Raylib.DrawText($"Press W, SPACE or ^ to jump \nPress AD or -> <- to  \nYou can double jump", 100, 450, 20, Color.WHITE);
            }
        }

        Raylib.EndDrawing();
    }

    public enum GameStates
    {
        startScreen, playing, win, dead, clearedLevel
    }
}
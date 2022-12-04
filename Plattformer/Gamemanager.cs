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
    public event Action OnUppdate;
    public event Action OnReloadLevel;

    //actives ones at the begining of the program
    public Gamemanager() { levelManager = new Level(this); }

    //updates every frame
    public void Update()
    {
        allObjects = levelManager.allObjects;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL) && Raylib.IsKeyPressed(KeyboardKey.KEY_C))
        {
            ConsoleComandos();
        }

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
            gameState = GameStates.playing;
            System.Console.WriteLine("Game has started");

            levelManager.ChangeLevel(0);
        }
    }
    private void Playing()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
        {
            levelManager.ReloadLevel();
        }

        OnUppdate.Invoke();
    }
    private void Win()
    {
        System.Console.WriteLine("YOU WIN");
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

        Raylib.EndDrawing();
    }

    void ConsoleComandos()
    {
        System.Console.WriteLine("Opend console");
        string input = Console.ReadLine();

    }
    public enum GameStates
    {
        startScreen, playing, win, dead, clearedLevel
    }
}
using System.Numerics;
using Raylib_cs;
public class Gamemanager
{
    public int tile = 75;
    Level levelManager;
    //Loading in spriteSheet
    Texture2D spriteSheet = Raylib.LoadTexture(@"RaylibPlattformer.png");
    //Set the state of the game to start
    public GameStates gameState = GameStates.startScreen;
    Player player;

    WinPortal portal;
    public List<Enemy> enemys = new();

    public List<GameObject> allObjects = new List<GameObject>();

    //actives ones at the begining of the program
    public void Start()
    {
        System.Console.WriteLine(spriteSheet);
        levelManager = new Level(this);
    }
    //updates every frame
    public void Update()
    {
        if (gameState == GameStates.startScreen)
        {
            StartScreen();
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
    }
    private void StartScreen()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
        {
            StartTheGame();
        }
    }
    private void Playing()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
        {
            player.SetPosition(new Vector2());
        }
        player.Update();
        foreach (Enemy enemy in enemys)
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
        player.fallSpeed = 0;
        player.isDead = false;
        player.SetPosition(new Vector2());
        gameState = GameStates.playing;
    }
    public void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(new Color(255, 205, 117, 255));
        foreach (GameObject gO in allObjects)
        {
            //Draw the hitbox
            Raylib.DrawRectangle((int)gO.hitbox.x, (int)gO.hitbox.y, (int)gO.hitbox.width, (int)gO.hitbox.height, Color.LIME);
            //Draw the texture
            Raylib.DrawTexturePro(spriteSheet, gO.spriteLocation, new Rectangle(gO.hitbox.x + (gO.hitbox.width - tile) / 2, gO.hitbox.y + (gO.hitbox.height - tile) / 2, tile, tile), new Vector2(), 0, Color.WHITE);
        }

        Raylib.EndDrawing();
    }
    //when the "real" game start 
    void StartTheGame()
    {
        gameState = GameStates.playing;
        System.Console.WriteLine("Game has started");

        portal = new WinPortal(this, new Vector2(12, 10));

        levelManager.ChangeLevel(0);

        player = new Player("Player", this, new Vector2(3, 3));
    }
    public enum GameStates
    {
        startScreen, playing, win, dead
    }
}
using System.Numerics;
using Raylib_cs;
public class Player : GameObject
{
    public Player(string name, Gamemanager gm, Vector2 pos)
    {
        this.name = name;
        this.gm = gm;
        this.hitbox = new Rectangle(pos.X * gm.tile, pos.Y * gm.tile, gm.tile * 0.5f, gm.tile * 0.75f);
        color = Color.BLUE;
        tag = Tag.player;
        spriteLocation = new Rectangle(0, 0, 8, 8);
        isFacingRight = true;

        gm.OnUppdate += Update;
        gm.OnReloadLevel += ReloadLevel;
    }
    void ReloadLevel()
    {
        gm.OnUppdate -= Update;
        gm.OnReloadLevel -= ReloadLevel;
    }
    private float _jumpForce = 13f;
    private float _moveSpeed = 4f;
    private int amountOfJumps = 2;
    private int _amountOfJumps = 0;
    //plays every frame when game starts
    public void Update()
    {
        xSpeed = _moveSpeed;
        //calls the physics in GameObject
        PhysicsUpdate();
        Controlls();
        if (isDead || hitbox.x > 30 * gm.tile)
        {
            gm.gameState = Gamemanager.GameStates.dead;
        }

        if (hasWon)
        {
            gm.gameState = Gamemanager.GameStates.clearedLevel;
        }
    }
    private void Controlls()
    {
        if (isGrounded)
        {
            _amountOfJumps = amountOfJumps;
        }
        //check if space is pressed, if true then jump
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && _amountOfJumps > 0)
        {
            _amountOfJumps--;
            fallSpeed = -_jumpForce;
        }
        //handle x movement
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            moveInput = 1;
            isFacingRight = true;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            moveInput = -1;
            isFacingRight = false;
        }
        else
        {
            moveInput = 0;
        }
        System.Console.WriteLine(isFacingRight);
    }
}
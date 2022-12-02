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

        SetActive(true);
    }
    private float _jumpForce = 12f;
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
        if (isDead || hitbox.x > 3000)
        {
            gm.gameState = Gamemanager.GameStates.dead;
        }

        if (hasWon)
        {
            gm.gameState = Gamemanager.GameStates.win;
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
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            moveInput = -1;
        }
        else
        {
            moveInput = 0;
        }
    }
}
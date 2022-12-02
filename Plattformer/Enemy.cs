using System.Numerics;
using Raylib_cs;
public class Enemy : GameObject
{
    public Enemy(string name, Gamemanager gm, Vector2 pos, Rectangle hitbox, Rectangle spriteLocation, float _moveSpeed, float _jumpForce)
    {
        this.name = name;
        this.gm = gm;
        this.hitbox = hitbox;
        this.spriteLocation = spriteLocation;

        color = Color.RED;
        tag = Tag.hurtPlayer;

        this._moveSpeed = _moveSpeed;
        this._jumpForce = _jumpForce;
        xSpeed = _moveSpeed;

        gm.enemys.Add(this);
        SetActive(true);
    }
    private float _jumpForce = 10f;
    private float _moveSpeed = 4f;

    private float _moveDir = -1;

    //plays ones when game starts
    public void Start()
    {

    }
    //plays every frame when game starts
    public void Update()
    {
        if (isTouchingLeftWall)
        {
            _moveDir = -1;
        }
        else if (isTouchingRightWall)
        {
            _moveDir = 1;
        }
        if (isGrounded)
        {
            fallSpeed = -_jumpForce;
        }
        xSpeed = _moveSpeed;
        moveInput = _moveDir;
        PhysicsUpdate();
    }
}

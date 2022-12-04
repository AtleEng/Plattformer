using System.Numerics;
using Raylib_cs;
public class Enemy : GameObject
{
    public Enemy(string name, Gamemanager gm, Vector2 pos, Rectangle hitbox, Rectangle spriteLocation, float _moveSpeed, float _jumpForce, bool isStatic)
    {
        this.name = name;
        this.gm = gm;
        this.hitbox = hitbox;
        this.spriteLocation = spriteLocation;

        color = Color.RED;
        tag = Tag.hurtPlayer;

        this._moveSpeed = _moveSpeed;
        this._jumpForce = _jumpForce;
        this.isStatic = isStatic;

        xSpeed = _moveSpeed;

        gm.OnUppdate += Update;
        gm.OnReloadLevel += ReloadLevel;
    }
    void ReloadLevel()
    {
        gm.OnUppdate -= Update;
        gm.OnReloadLevel -= ReloadLevel;
    }
    private float _jumpForce = 10f;
    private float _moveSpeed = 4f;
    private bool isStatic = false;
    private float _moveDir = -1;
    //plays every frame when game starts
    public void Update()
    {
        if (isTouchingLeftWall)
        {
            _moveDir = -1;
            isFacingRight = false;
        }
        else if (isTouchingRightWall)
        {
            _moveDir = 1;
            isFacingRight = true;
        }
        if (isGrounded)
        {
            fallSpeed = -_jumpForce;
        }
        xSpeed = _moveSpeed;
        moveInput = _moveDir;
        if (isStatic) return;
        PhysicsUpdate();
    }
}

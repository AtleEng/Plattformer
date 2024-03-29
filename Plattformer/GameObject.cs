using System.Numerics;
using Raylib_cs;

public class GameObject
{
    public Gamemanager gm;
    public string name = "GameObject";
    public Rectangle hitbox;
    public Tag tag = Tag.block;
    //Graphics
    public Rectangle spriteLocation = new Rectangle(0, 0, 8, 8);
    public Color color = Color.BLACK;
    //Gravity varibles
    public float gravity = 0.5f;
    public float fallSpeed = 0;
    public float maxFallSpeed = 15;
    //movement
    public float moveInput = 0;
    public float xSpeed = 1;
    //collition bools
    public bool isGrounded = false;
    public bool isTouchingRightWall = false;
    public bool isTouchingLeftWall = false;
    public bool isFacingRight = false;

    //state bools
    public bool isDead = false;
    public bool hasWon = false;

    //call to get the physics
    public void PhysicsUpdate()
    {
        isGrounded = false;
        isTouchingLeftWall = false;
        isTouchingRightWall = false;
        //gravity calculation and Y collision
        fallSpeed += gravity;
        if (fallSpeed > maxFallSpeed)
        {
            fallSpeed = maxFallSpeed;
        }
        hitbox.y += fallSpeed;
        //Sideways movement and X collition
        hitbox.x += moveInput * xSpeed;

        Rectangle rect = new();
        GameObject gO = new();
        foreach (GameObject _gO in gm.allObjects)
        {
            if (_gO != this && _gO.tag != tag && !(tag == Tag.hurtPlayer && _gO.tag == Tag.portal))
            {
                //check if colliding
                Rectangle _rect = Raylib.GetCollisionRec(hitbox, _gO.hitbox);
                if (_rect.width * _rect.height > rect.width * rect.height)
                {
                    rect = _rect;
                    gO = _gO;
                }
            }
        }
        //check if gameObjects hitbox is left or right of the other hitbox
        int isX = 1;
        if (gO.hitbox.x < hitbox.x)
        {
            isX = -1;
        }
        //check if gameObjects hitbox is over or under of the other hitbox
        int isY = 1;
        if (gO.hitbox.y < hitbox.y)
        {
            isY = -1;
        }
        //check if it is a wall or a floor/roof
        if (rect.height < rect.width && rect.height != 0)
        {
            //move hitbox to correct Y position
            hitbox.y -= rect.height * isY;
            //check if it is floor or roof (only works if you jump at the roof)
            if (rect.y > hitbox.y)
            {
                fallSpeed = 0;
                isGrounded = true;
            }
            else if (fallSpeed < 0)
            {
                fallSpeed = 0;
            }
        }
        else if (rect.width < rect.height && rect.width != 0)
        {
            //move hitbox to correct X position
            hitbox.x -= rect.width * isX;
            if (rect.x > hitbox.x)
            {
                isTouchingLeftWall = true;
            }
            else
            {
                isTouchingRightWall = true;
            }
        }

        if (rect.width != 0 || rect.height != 0)
        {
            if (tag == Tag.player && gO.tag == Tag.hurtPlayer)
            {
                isDead = true;
            }
            else if (tag == Tag.hurtPlayer && gO.tag == Tag.player)
            {
                gO.isDead = true;
            }

            if (tag == Tag.player && gO.tag == Tag.portal)
            {
                hasWon = true;
            }
            else if (tag == Tag.portal && gO.tag == Tag.player)
            {
                hasWon = true;
            }
        }
    }
    //Set position of gameobject via a Vector 2
    public void SetPosition(Vector2 vec)
    {
        hitbox.x = vec.X;
        hitbox.y = vec.Y;
    }
    //To difference diffrent object
    public enum Tag
    {
        player, block, hurtPlayer, portal
    }
}
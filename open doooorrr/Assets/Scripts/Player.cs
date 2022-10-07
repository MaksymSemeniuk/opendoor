using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 movementVector;
    CapsuleCollider2D capsuleCollider2D;
    Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    public SpriteRenderer spriteRenderer;
    public float movementSpeed = 6f;
    public float jumpHeight = 6f;
    bool timeStart;

    private float waitTime = 1.0f;
    private float timer = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        Vector2 playerVelocity = new Vector2(movementVector.x * movementSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            timeStart = true;
            boxCollider2D.enabled = false;
            spriteRenderer.enabled = false;
        }
        if (timeStart)
        {
            timer += Time.deltaTime;
        }
        if (timer > waitTime && !capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            boxCollider2D.enabled = true;
            spriteRenderer.enabled = true;
            timer = timer - waitTime;
            timeStart = false;
        }
    }
    private void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        Debug.Log(movementVector);
    }
    private void OnJump(InputValue value)
    {
        if (!capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpHeight);
        }
    }
}
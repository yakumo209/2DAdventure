using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private PhysicsCheck check;
    private CapsuleCollider2D col;
    private Vector2 originOffset;
    private Vector2 originSize;
    public Vector2 inputDirection;
    public float speed=5;
    public float jumpForce=5;
    private Rigidbody2D rb;
    private float runSpeed;
    public bool isCrouch;
    public bool isHurt;
    public float hurtForce;
    private float walkSpeed=> speed/2;
    public bool isDead;
    
    int faceDir=1;
    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        originOffset = col.offset;
        originSize = col.size;
        runSpeed = speed;
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.WalkButton.performed += (ctx) =>
        {
            if (check.isGround)
            {
                speed = walkSpeed;
            }
        };
        inputControl.GamePlay.WalkButton.canceled += (ctx) =>
        {
            speed = runSpeed;
        };
        check = GetComponent<PhysicsCheck>();
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (check.isGround)
        {
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
        }
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable(); 
    }

    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        isCrouch = inputDirection.y < -0.5f && check.isGround;
        if (isCrouch)
        {
            col.offset = new Vector2(-0.05f, 0.85f);
            col.size = new Vector2(.7f, 1.7f);
        }
        else
        {
            col.offset = originOffset;
            col.size = originSize;
        }
    }

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
        }
        
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);
    }
    private void Move()
    {
        if (!isCrouch)
        {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        }

        if (isCrouch)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (inputDirection.x>0)
        {
            faceDir = 1;
        }

        if (inputDirection.x<0)
        {
            faceDir = -1;
        }
        transform.localScale = new Vector3(faceDir, transform.localScale.y, transform.localScale.z);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.GamePlay.Disable();
    }
}

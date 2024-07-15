using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator am;
    private Rigidbody2D rb;
    private void Awake()
    {
        am = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        am.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        am.SetFloat("velocityY",rb.velocity.y);
        am.SetBool("isGround",GetComponent<PhysicsCheck>().isGround);
        am.SetBool("isCrouch",GetComponent<PlayerController>().isCrouch);
        am.SetBool("isDead",GetComponent<PlayerController>().isDead);
    }

    public void PlayerHurt()
    {
        am.SetTrigger("hurt");
    }
}

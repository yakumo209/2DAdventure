using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGround;
    public float checkRadius;
    public LayerMask groundLayer;
    private void Update()
    {
        Check();
    }

    private void Check()
    {
        isGround=Physics2D.OverlapCircle(transform.position, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,checkRadius); 
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movespeed = 5f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        rb.linearVelocity = context.ReadValue<Vector2>().normalized * movespeed;
    }
}

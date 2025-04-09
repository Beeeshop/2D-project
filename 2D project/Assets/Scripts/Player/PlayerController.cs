using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl InputControl;
    public Vector2 inputDirection;
    private Rigidbody2D rb;
    public float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        InputControl = new PlayerInputControl();
    }

    private void OnEnable()
    {
        InputControl.Enable();
    }

    private void OnDisable()
    {
        InputControl.Disable();
    }

    private void Update()
    {
        inputDirection = InputControl.Gameplay.Move.ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        Move();

    }

    private void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;
        if(inputDirection.x < 0)
            faceDir = -1;

        transform.localScale =new Vector3(faceDir, 1, 1);
    }

}

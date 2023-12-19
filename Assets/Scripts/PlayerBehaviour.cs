using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerInput CoolInputStuff;

    private InputAction WASD;
    private InputAction Jump;

    private Rigidbody2D rb2d;

    public float MoveSpeed;
    private float movePlayer;
    public float JumpForce;
    // Start is called before the first frame update
    void Start()
    {
        CoolInputStuff.currentActionMap.Enable();

        WASD = CoolInputStuff.currentActionMap.FindAction("XMovement");
        Jump = CoolInputStuff.currentActionMap.FindAction("Jump");

        Jump.started += Jump_started;

        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Jump_started(InputAction.CallbackContext obj)
    {
        rb2d.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        Jump.canceled -= Jump_started;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(movePlayer * MoveSpeed, rb2d.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer = WASD.ReadValue<float>();
    }
}

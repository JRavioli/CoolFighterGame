using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerInput CoolInputStuff;

    private InputAction LeftRight;
    private InputAction Jump;
    private InputAction TempAttack;

    private Rigidbody2D rb2d;

    public float MoveSpeed;
    private float movePlayer;
    public float JumpForce;

    private Collider2D hitBox;
    // Start is called before the first frame update
    void Start()
    {
        CoolInputStuff.currentActionMap.Enable();

        LeftRight = CoolInputStuff.currentActionMap.FindAction("XMovement");
        Jump = CoolInputStuff.currentActionMap.FindAction("Jump");
        TempAttack = CoolInputStuff.currentActionMap.FindAction("TempAttack");

        Jump.started += Jump_started;
        TempAttack.started += TempAttack_started;

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        hitBox = GetComponent<Collider2D>();
    }

    private void TempAttack_started(InputAction.CallbackContext obj)
    {
        hitBox.enabled = true;
        StartCoroutine(HitboxDuration());
    }

    private void Jump_started(InputAction.CallbackContext obj)
    {
        rb2d.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        Jump.canceled -= Jump_started;
        TempAttack.canceled -= TempAttack_started;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(movePlayer * MoveSpeed, rb2d.velocity.y);
        if (rb2d.velocity.x > 0)
        {
            hitBox.offset = new Vector2(1, 0);
        }
        else if (rb2d.velocity.x < 0)
        {
            hitBox.offset = new Vector2(-1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer = LeftRight.ReadValue<float>();
    }

    private IEnumerator HitboxDuration()
    {
        yield return new WaitForSeconds(1);
        hitBox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Collision works");
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float Jump;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector3 _jumpForce;
    private bool _isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        _jumpForce = Vector3.up * Jump;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _isJumping = true;
            rb.AddForce(_jumpForce, ForceMode2D.Impulse);
            anim.SetInteger("transition", 2);
        }
    }

    private void FixedUpdate()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector3(direction * Speed, rb.velocity.y);

        if (!_isJumping)
        {
            if (direction != 0)
            {
                anim.SetInteger("transition", 1);
            }
            else
            {
                anim.SetInteger("transition", 0);
            }
        }

        if (direction > 0)
        {
            transform.eulerAngles = Vector3.zero;
        } else if (direction < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }
}

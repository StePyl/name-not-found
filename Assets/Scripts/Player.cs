using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    Rigidbody2D body;
    BoxCollider2D boxCollider2D;

    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;
    public float jumpHeight = 10.0f;

    public bool jump = false;
    public LayerMask groundmask;

    public Animator animator;

    // Start is called before the first frame update
    private bool IsGrounded()
    {
        var groundCheck = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down,
            .1f, groundmask);

        Debug.Log(groundCheck.collider);
        return groundCheck.collider != null;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        
        body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, horizontal < 0 ? 180 : 0,
            body.transform.eulerAngles.z); // Flipped


        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jump = true;
            animator.SetBool("Jump", jump);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, jump ? jumpHeight : body.velocity.y);
        jump = false;
        animator.SetBool("Jump", jump);
    }
}
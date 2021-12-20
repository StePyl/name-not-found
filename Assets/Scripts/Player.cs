using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    Rigidbody2D body;
    BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;

    float horizontal;
    float vertical;

    public bool jump = false;
    public float runSpeed = 10.0f;
    public float jumpHeight = 10.0f;
    
    public LayerMask groundmask;
    public Animator animator;

    // Start is called before the first frame update
    private bool IsGrounded()
    {
        var groundCheck = Physics2D.BoxCast(circleCollider2D.bounds.center, new Vector2(circleCollider2D.radius, circleCollider2D.radius), 0f, Vector2.down,
            circleCollider2D.radius, groundmask);
        return groundCheck.collider != null;
    }


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        BoxCaster.BoxCast(circleCollider2D.bounds.center, new Vector2(circleCollider2D.radius, circleCollider2D.radius), 0f, Vector2.down, circleCollider2D.radius, groundmask);
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
        if (jump == true)
        {
            jump = false;
            animator.SetBool("Jump", jump);
        }
    }

  
}

class BoxCaster
{
    static public RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance,
        int mask)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance, mask);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }

}
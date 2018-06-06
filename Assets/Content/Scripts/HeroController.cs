using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {

    public float speed = 1;

    private Rigidbody2D myBody = null;
    private SpriteRenderer sr = null;
    Animator anim;
    private bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 1f;
    public float JumpSpeed = 3f;

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        isGrounded = hit;

        if (Input.GetButtonDown("Jump") && isGrounded)
            this.JumpActive = true;

        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }

        float value = Input.GetAxis("Horizontal");

        if (Mathf.Abs(value) > 0)
        {
            if (isGrounded)
                anim.Play("Rabbit_Run");
            else anim.Play("Rabbit_Jump");
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;

            if (value < 0)
            {
                sr.flipX = true;
            }
            else if (value > 0)
            {
                sr.flipX = false;
            }
        }
        else
        {
            if (isGrounded)
                anim.Play("Rabbit_Idle");
            else anim.Play("Rabbit_Jump");
        }
    }
}

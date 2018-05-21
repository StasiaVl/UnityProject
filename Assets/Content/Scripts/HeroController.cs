using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {

    public float speed = 1;

    private Rigidbody2D myBody = null;
    private SpriteRenderer sr = null;
    Animator anim;
    int runHash;
    int idleHash;

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        runHash = Animator.StringToHash("Rabbit_Run");
        idleHash = Animator.StringToHash("Rabbit_Idle");
    }

    void FixedUpdate()
    {
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            anim.Play("Rabbit_Run");
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
        else anim.Play("Rabbit_Idle");
    }
}

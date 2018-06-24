using System.Collections;
using UnityEngine;

public class OrcEnemy : MonoBehaviour
{

    public enum Mode  {  GoToA, GoToB, Attack, Dead  }

    public HeroController rabbit;

    public Vector3 pointA;
    public Vector3 pointB;

    public float attackSpeed = 2;

    Mode mode = Mode.GoToA;

    Rigidbody2D myBody;
    Animator animOrc;
    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        pointA.y = transform.position.y;
        pointB.y = transform.position.y;
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animOrc = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (mode != Mode.Dead)
        {
            updateMode();
            float value = getDirection();
            ChangeVelocity(value);
        }
        
    }

    float getDirection()
    {
        if (mode == Mode.GoToA)
        {
            return -1;
        }
        else if (mode == Mode.GoToB)
        {
            return 1;
        }
        else if (mode == Mode.Attack)
        {
            if (transform.position.x < rabbit.transform.position.x)
            {
                return attackSpeed;
            }
            else
            {
                return -attackSpeed;
            }
        }
        return 0;
    }

    void ChangeVelocity(float value)
    {
        Vector2 vel = myBody.velocity;
        vel.x = value;
        myBody.velocity = vel;
    }

    void updateMode()
    {
        if (rabbit.transform.position.x > Mathf.Min(pointA.x, pointB.x)
            && rabbit.transform.position.x < Mathf.Max(pointA.x, pointB.x))
        {
            animOrc.Play("GreenOrc_Run");
            mode = Mode.Attack;
            sr.flipX = rabbit.transform.position.x > transform.position.x; //turn the sprite
        }
        else if (!sr.flipX || mode == Mode.GoToA)
        {
            if (isArrived(pointA))
            {
                //Debug.Log("Arrived to A");
                mode = Mode.GoToB;
                sr.flipX = true; //turn the sprite
                animOrc.Play("GreenOrc_Walk");
            }
        }
        else if (sr.flipX || mode == Mode.GoToB)
        {
            if (isArrived(pointB))
            {
                //Debug.Log("Arrived to B");
                mode = Mode.GoToA;
                sr.flipX = false; //turn the sprite
                animOrc.Play("GreenOrc_Walk");
            }
        }
    }

    bool isArrived(Vector3 point)
    {
        return Vector3.Distance(point, transform.position) < 0.02f;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (mode == Mode.Dead)
            return;
        HeroController rabbit = other.transform.GetComponent<HeroController>();
        if (rabbit != null)
        {
            if (rabbit.transform.position.y > transform.position.y + 1)
            {
                mode = Mode.Dead;
                myBody.velocity = Vector2.zero;
                animOrc.Play("GreenOrc_Die");
                Destroy(gameObject, 1);
                rabbit.OnKill();
            }
            else
            {
                LevelController.current.onRabbitDeath(rabbit);
                if (mode == Mode.Attack)
                    animOrc.Play("GreenOrc_Attack");
            }
        }
    }
}

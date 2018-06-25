using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotingOrcEnemy : MonoBehaviour {

    public float range = 5;
    public float ReloadTime = 2;
    public float CarrotSpeed = 10f;
    public Carrot CarrotGameObject;

    private float timeLeft;


    public enum Mode { GoToA, GoToB, Attack, Dead }

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
            if (timeLeft > 0)
                timeLeft -= Time.deltaTime;
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
            if (timeLeft <= 0)
            {
                ThrowCarrot();
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
        if (rabbit.transform.position.x > transform.position.x - range
            && rabbit.transform.position.x < transform.position.x + range)
        {
            mode = Mode.Attack;
            sr.flipX = rabbit.transform.position.x > transform.position.x; //turn the sprite
        }
        else if (!sr.flipX || mode == Mode.GoToA)
        {
            mode = Mode.GoToA;
            if (isArrived(pointA))
            {
                mode = Mode.GoToB;
                sr.flipX = true; //turn the sprite
                animOrc.Play("BrownOrc_Walk");
            }
        }
        else if (sr.flipX || mode == Mode.GoToB)
        {
            mode = Mode.GoToB;
            if (isArrived(pointB))
            {
                mode = Mode.GoToA;
                sr.flipX = false; //turn the sprite
                animOrc.Play("BrownOrc_Walk");
            }
        }
    }

    bool isArrived(Vector3 point)
    {
        return Vector3.Distance(point, transform.position) < 0.02f;
    }

    private void ThrowCarrot()
    {
        timeLeft = ReloadTime;
        animOrc.Play("BrownOrc_Attack");
        Carrot newCarrot = Instantiate(CarrotGameObject.gameObject).GetComponent<Carrot>();
        newCarrot.transform.position = transform.position + new Vector3(0,1,0);
        newCarrot.Speed = CarrotSpeed;
        newCarrot.Direction = !sr.flipX;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (mode == Mode.Dead)
            return;
        HeroController rabbit = other.transform.GetComponent<HeroController>();
        if (rabbit != null)
        {
            myBody.velocity = Vector2.zero;
            if (rabbit.transform.position.y > transform.position.y + 1)
            {
                mode = Mode.Dead;
                animOrc.Play("BrownOrc_Die");
                Destroy(gameObject, 1);
                rabbit.OnKill();
            }
            else
            {
                animOrc.Play("BrownOrc_Attack");
                LevelController.current.onRabbitDeath(rabbit);
            }
        }
    }
}

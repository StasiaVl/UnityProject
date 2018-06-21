using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {

    public float speed = 1;

    private Rigidbody2D myBody = null;
    private SpriteRenderer sr = null;
    Animator anim;
    Transform heroParent = null;
    private bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 1f;
    public float JumpSpeed = 3f;
    private bool dead = false;

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        LevelController.current.setStartPosition(transform.position);
        this.heroParent = this.transform.parent;
    }

    void FixedUpdate()
    {
        if (dead)
        {
            anim.Play("Rabbit_Die");
            this.transform.position = LevelController.current.getStartPosition();
            dead = false;
        }
        else
        {
            Vector3 from = transform.position + Vector3.up * 0.3f;
            Vector3 to = transform.position + Vector3.down * 0.1f;
            int layer_id = 1 << LayerMask.NameToLayer("Ground");
            //Перевіряємо чи проходить лінія через Collider з шаром Ground
            RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
            isGrounded = hit;

            if (hit)
            {
                //Перевіряємо чи ми опинились на платформі
                if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
                {
                    //Приліпаємо до платформи
                    SetNewParent(this.transform, hit.transform);
                }
            }
            else
            {
                //Ми в повітрі відліпаємо під платформи
                SetNewParent(this.transform, this.heroParent);
            }

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

    public void Death()
    {
        dead = true;
    }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }
}
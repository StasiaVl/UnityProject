using System.Collections;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public float Speed;
    public float LifeTime = 3;
    public bool Direction;
    private Vector3 target;

    private void Start()
    {
        StartCoroutine(DestroyLater());
        GetComponent<SpriteRenderer>().flipX = Direction;
        if (!Direction)
            target = new Vector3(1000, transform.position.y, 0);
        else target = new Vector3(-1000, transform.position.y, 0);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Speed);
    }

    private IEnumerator DestroyLater()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HeroController>() != null)
        {
            LevelController.current.onRabbitDeath(other.GetComponent<HeroController>());
            Destroy(gameObject);
        }
    }
}
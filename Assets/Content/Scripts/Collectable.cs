using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    protected virtual void OnRabitHit(HeroController rabit)
    {
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //if (!this.hideAnimation)
        //{
            HeroController rabit = collider.GetComponent<HeroController>();
            if (rabit != null)
            {
                this.OnRabitHit(rabit);
            }
        //}
    }
    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }
}

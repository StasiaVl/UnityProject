using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current = null;
    private int lifes = 3;
    Vector3 startingPosition;

    void Awake()
    {
        if (LevelController.current == null)
            LevelController.current = this;

        if (LevelController.current != this)
            Destroy(this.gameObject);
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void onRabbitDeath(HeroController hero)
    {
        hero.Death();
        hero.transform.position = this.startingPosition;
        //if (decrementLifes() > 0) ;
        //else
        //    //TODO restart lvl
        //    ;
    }

    public int getLifesCount() { return lifes; }

    private int decrementLifes() { return --lifes; }
    private int incrementLifes() { return ++lifes; }
}


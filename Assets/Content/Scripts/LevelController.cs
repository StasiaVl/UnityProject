﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current = null;
    private int lifes = 3;
    private int coins = 0;
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

    public Vector3 getStartPosition()
    {
        return this.startingPosition;
    }

    public void addCoins(int a)
    {
        coins += a;
        //renew GUI
    }

    public void onRabbitDeath(HeroController hero)
    {
        hero.Die();
        //hero.transform.position = this.startingPosition;
        //if (decrementLifes() > 0) ;
        //else
        //    //TODO restart lvl
        //    ;
    }

    public int getLifesCount() { return lifes; }

    private int decrementLifes() { return --lifes; }
    private int incrementLifes() { return ++lifes; }
}


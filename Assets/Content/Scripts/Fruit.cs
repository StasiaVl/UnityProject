using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{

    protected override void OnRabitHit(HeroController rabit)
    {
        LevelController.current.pickUpFruit();
        this.CollectedHide();
    }
}

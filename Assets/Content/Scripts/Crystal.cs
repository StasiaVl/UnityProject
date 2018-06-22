using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{

    protected override void OnRabitHit(HeroController rabit)
    {
        LevelController.current.addCoins(50);
        this.CollectedHide();
    }
}

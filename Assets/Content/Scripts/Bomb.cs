using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

    protected override void OnRabitHit(HeroController rabit)
    {
        rabit.EatBomb();
        this.CollectedHide();
    }
}

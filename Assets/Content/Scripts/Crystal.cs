using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{
    public CrystalType type;

    protected override void OnRabitHit(HeroController rabit)
    {
        LevelController.current.pickUpCrystal(type);
        this.CollectedHide();
    }
}

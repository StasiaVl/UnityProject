using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable
{

    void Start()
    {
        StartCoroutine(destroyLater());
    }

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}

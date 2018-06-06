using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{

    public HeroController rabit;
    
    private Vector3 rabit_position;
    private Vector3 camera_position;

    void Update()
    {
        //Отримуємо доступ до координат кролика
        rabit_position = rabit.transform.position;
        camera_position = this.transform.position;

        //Рухаємо камеру тільки по X,Y
        camera_position.x = rabit_position.x;
        camera_position.y = rabit_position.y;

        //Встановлюємо координати камери
        this.transform.position = camera_position;
    }
}
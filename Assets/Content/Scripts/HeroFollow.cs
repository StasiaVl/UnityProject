using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{

    public HeroController rabit;

    private Transform rabit_transform = null;
    private Transform camera_transform = null;
    private Vector3 rabit_position;
    private Vector3 camera_position;

    void Update()
    {
        //Отримуємо доступ до компонента Transform
        //це Скорочення до GetComponent<Transform>
        rabit_transform = rabit.transform;

        //Отримуємо доступ до компонента Transform камери
        camera_transform = this.transform;

        //Отримуємо доступ до координат кролика
        rabit_position = rabit_transform.position;
        camera_position = camera_transform.position;

        //Рухаємо камеру тільки по X,Y
        camera_position.x = rabit_position.x;
        camera_position.y = rabit_position.y;

        //Встановлюємо координати камери
        camera_transform.position = camera_position;
    }
}
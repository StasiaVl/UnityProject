using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public HeroController rabit;

    private Vector3 rabit_position;
    private Vector3 zone_position;

    void Update()
    {
        //Отримуємо доступ до координат кролика
        rabit_position = rabit.transform.position;
        zone_position = this.transform.position;

        //Рухаємо камеру тільки по X
        zone_position.x = rabit_position.x;

        //Встановлюємо координати камери
        this.transform.position = zone_position;
    }

    //Стандартна функція, яка викличеться,
    //коли поточний об’єкт зіштовхнеться із іншим
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Намагаємося отримати компонент кролика
        HeroController rabbit = collider.GetComponent<HeroController>();
        //Впасти міг не тільки кролик
        if (rabbit != null)
        {
            Debug.Log("DEAD");
            //Повідомляємо рівень, про смерть кролика
            if (LevelController.current != null)
                LevelController.current.onRabbitDeath(rabbit);
            else
                Debug.LogError("Seems like something is wrong with the singleton.");
        }
    }
}
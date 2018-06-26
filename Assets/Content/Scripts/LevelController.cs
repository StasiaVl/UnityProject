using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public enum CrystalType
{
    Red, Blue, Green
}

public class LevelController : MonoBehaviour
{
    public static LevelController current = null;

    public Text coinText;
    public Image life1Img;
    public Image life2Img;
    public Image life3Img;
    public Image redCrystalImage;
    public Image blueCrystalImage;
    public Image greenCrystalImage;
    public Text fruitText;

    private int lifes = 3;
    private int coins = 0;
    private bool redCrystal = false;
    private bool blueCrystal = false;
    private bool greenCrystal = false;
    private int gotFruits = 0;
    private int levelFruits;
    Vector3 startingPosition;

    void Awake()
    {
        if (LevelController.current == null)
            LevelController.current = this;

        if (LevelController.current != this)
            Destroy(this.gameObject);
    }

    void Start()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        levelFruits = fruits.Length;

        blueCrystalImage.gameObject.SetActive(false);
        greenCrystalImage.gameObject.SetActive(false);
        redCrystalImage.gameObject.SetActive(false);

        SetLifes();
    }

    void SetLifes()
    {
        lifes = 3;
        life1Img.gameObject.SetActive(true);
        life2Img.gameObject.SetActive(true);
        life3Img.gameObject.SetActive(true);
    }

    void Update()
    {
        string coins = "" + this.coins;
        if (coins.Length == 1) coinText.text = "000" + coins;
        else if (coins.Length == 2) coinText.text = "00" + coins;
        else if (coins.Length == 3) coinText.text = "0" + coins;
        else coinText.text = coins.Substring(coins.Length - 4);

        if (blueCrystal) blueCrystalImage.gameObject.SetActive(true);
        else if (redCrystal) blueCrystalImage.gameObject.SetActive(true);
        else if (greenCrystal) blueCrystalImage.gameObject.SetActive(true);

        fruitText.text = gotFruits + "/" + levelFruits;
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void pickUpCrystal(CrystalType crystal)
    {
        if (crystal == CrystalType.Red)
        {
            redCrystal = true;
            redCrystalImage.gameObject.SetActive(true);
        }
        else if (crystal == CrystalType.Blue)
        {
            blueCrystal = true;
            blueCrystalImage.gameObject.SetActive(true);
        }
        else if (crystal == CrystalType.Green)
        {
            greenCrystal = true;
            greenCrystalImage.gameObject.SetActive(true);
        }
    }

    public void pickUpFruit()
    {
        gotFruits++;
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
        decrementLifes();
        hero.Die();
    }

    public int getLifesCount() { return lifes; }

    private int decrementLifes()
    {
        if (lifes > 0) lifes--;

        if (lifes == 2) life1Img.gameObject.SetActive(false);
        else if (lifes == 1) life2Img.gameObject.SetActive(false);
        else if (lifes == 0) life3Img.gameObject.SetActive(false);

        return lifes;
    }
    private int incrementLifes() { if (lifes < 3) return ++lifes; else return lifes; }
}


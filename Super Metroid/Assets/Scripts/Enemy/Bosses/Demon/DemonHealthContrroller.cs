using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonHealthContrroller : MonoBehaviour
{
    public static DemonHealthContrroller instance;
    public GameObject Wall;
    public GameObject Door;
    private void Awake()
    {
        instance = this;
    }

    public Slider bossHealthSlider;
    public int currentHealth = 200;

   
    public Demon demon;
    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;

            demon.EndBattle();
            Wall.SetActive(false);
            Door.SetActive(true);
            PlayerController.Demon = true;
            

        }

        bossHealthSlider.value = currentHealth;
    }
}

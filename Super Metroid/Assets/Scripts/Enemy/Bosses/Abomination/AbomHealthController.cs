using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AbomHealthController : MonoBehaviour
{
    public static AbomHealthController instance;
    

    
    public void Awake()
    {
        instance = this;
    }

    public Slider bossHealthSlider;
    public int currentHealth = 100;
    public Abomination Abomination;
    // Start is called before the first frame update
    void Start()
    {
        
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth<=0)
        {
            currentHealth = 0;
           

            Abomination.EndBattle();
        }

        bossHealthSlider.value = currentHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool charging = false;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RespawnController.instance.SetSpawn(transform.position);
            Debug.Log(PlayerHealthController.instance.currentHealth);
            if (PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
            {
                charging = true;
            }
           else
            {
                charging = false;
            }
        }
        else
        {
            charging = false;
        }
    }

    private void FixedUpdate()
    {
        if(PlayerHealthController.instance.currentHealth >= PlayerHealthController.instance.maxHealth)
        {
            PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        }
        if(charging)
        {
            PlayerHealthController.instance.currentHealth += (PlayerHealthController.instance.maxHealth / 10);
            
        }

        if (PlayerHealthController.instance.currentHealth == PlayerHealthController.instance.maxHealth)
        {
            charging = false;
        }
        UiController.instance.UpdateHealth(PlayerHealthController.instance.currentHealth, PlayerHealthController.instance.maxHealth);
        

    }
}

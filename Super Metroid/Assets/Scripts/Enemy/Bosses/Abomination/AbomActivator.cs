using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbomActivator : MonoBehaviour
{
    public GameObject bossToActivate;
    private void Start()
    {
        bossToActivate.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!PlayerController.bossBeat && PlayerAbilityTracker.canDoubleJump)
            {
                bossToActivate.SetActive(true);
                gameObject.SetActive(false);
            }


        }
    }
}

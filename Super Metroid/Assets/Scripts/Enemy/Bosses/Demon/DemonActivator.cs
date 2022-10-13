using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonActivator : MonoBehaviour
{
    public GameObject bossToActivate;
    public GameObject Door;
    public GameObject Wall;
    private void Start()
    {
        bossToActivate.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!PlayerController.Demon && PlayerAbilityTracker.canGravityReverse) 
            {
                bossToActivate.SetActive(true);
                Wall.SetActive(true);
                Door.SetActive(false);
                gameObject.SetActive(false);
            }


        }
    }
}

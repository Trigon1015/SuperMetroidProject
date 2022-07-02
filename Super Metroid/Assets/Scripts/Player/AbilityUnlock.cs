using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump, unlockDash, unlockMorphBall, unlockDropBomb, unlockFreeze, unlockGravityReverse;
    public GameObject pickupEffect;
    public string unlockMessage;
    public TMP_Text unlockText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();
            if(unlockDoubleJump)
            {
                PlayerAbilityTracker.canDoubleJump = true;
            }

            if (unlockDash)
            {
                PlayerAbilityTracker.canDash = true;
            }

            if (unlockMorphBall)
            {
                PlayerAbilityTracker.canMorphball = true;
            }

            if (unlockDropBomb)
            {
                PlayerAbilityTracker.canDropBomb = true;
            }

            if (unlockFreeze)
            {
                PlayerAbilityTracker.canFreeze = true;
            }

            if (unlockGravityReverse)
            {
                PlayerAbilityTracker.canGravityReverse = true;
            }


            Instantiate(pickupEffect, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            Destroy(unlockText.transform.parent.gameObject, 5f);

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
        
        if (unlockDoubleJump && PlayerAbilityTracker.canDoubleJump == true)
        {
            Destroy(gameObject);
        }

        if (unlockDash && PlayerAbilityTracker.canDash == true)
        {
            Destroy(gameObject);
        }

        if (unlockMorphBall && PlayerAbilityTracker.canMorphball == true)
        {
            Destroy(gameObject);
        }

        if (unlockDropBomb && PlayerAbilityTracker.canDropBomb == true)
        {
            Destroy(gameObject);
        }

        if (unlockFreeze && PlayerAbilityTracker.canFreeze == true)
        {
            Destroy(gameObject);
        }

        if (unlockGravityReverse && PlayerAbilityTracker.canGravityReverse == true)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBossBattle : MonoBehaviour
{
    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;

    public int threshold1, threshold2;

    public Transform player;

    public GameObject Thunder;
    public Transform thunderPoint;
    //phase 1
    private float fireWallCounter;
    public float fireWallCD;
    private float beamCounter;
    public float  beamCD;
    public Transform firePoint;
    public GameObject Firewall;
    public GameObject Beam;
    public GameObject Wizzard;

    //Phase 2
    public bool transform = false;
    public GameObject Vampire;
    public bool smash;
    public bool dash;

    private float smashCounter;
    public float smashCD;

    public GameObject vampireSmash;

    public Transform smashPoint;
    public Transform smashHeight;

    
    // Start is called before the first frame update
    void Start()
    {
        theCam = FindObjectOfType<CameraController>();
        theCam.enabled = false;

        fireWallCounter = fireWallCD;
        beamCounter = beamCD;

        smashCounter = smashCD;
    }

    // Update is called once per frame
    void Update()
    {
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
        smashPoint.position = new Vector2(player.transform.position.x, smashHeight.transform.position.y);
        //Phase 1
        if(DemonHealthContrroller.instance.currentHealth > threshold1)
        {
            if(fireWallCounter > 0)
            {
                fireWallCounter -= Time.deltaTime;
            }
            else
            {

                Instantiate(Firewall, firePoint.position, Quaternion.identity);
                
                fireWallCounter = fireWallCD;
            }

            if (beamCounter > 0)
            {
                beamCounter -= Time.deltaTime;
            }
            else
            {

                Instantiate(Beam, firePoint.position, Quaternion.identity);
                beamCounter = beamCD;
            }
        }
        else if(DemonHealthContrroller.instance.currentHealth <= threshold1 && DemonHealthContrroller.instance.currentHealth > threshold2)
        {
            if(!transform)
            {
                Instantiate(Thunder, thunderPoint.position, Quaternion.identity);

                StartCoroutine(Transform1());
                
                transform = true;
            }
            if(smashCounter > 0)
            {
                smashCounter -= Time.deltaTime;
            }
            else
            {

                Instantiate(vampireSmash, smashPoint.position,vampireSmash.transform.rotation);
                smashCounter = smashCD;
            }

        }
    }

    


    public IEnumerator Transform1()
    {
        Wizzard.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Vampire.SetActive(true);
    }

    
    public void EndBattle()
    {
        gameObject.SetActive(false);
    }

     
    
}


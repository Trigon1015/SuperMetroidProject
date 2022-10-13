using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBossBattle : MonoBehaviour
{
    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;

    public int threshold1, threshold2;

    private GameObject player;

    public GameObject Thunder;
    public Transform thunderPoint;

    public GameObject portal;
    
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
    private bool transform = false;
    public GameObject Vampire;
    public bool smash;
    public bool dash;

    private float smashCounter;
    public float smashCD;

    public GameObject vampireSmash;

    public Transform smashPoint;
    public Transform smashHeight;

    private bool attacking;

    //Phase 3
    private bool transform2 = false;
    public GameObject Demon;

    
    // Start is called before the first frame update
    void Start()
    {
        theCam = FindObjectOfType<CameraController>();
        theCam.enabled = false;

        fireWallCounter = fireWallCD;
        beamCounter = beamCD;

        smashCounter = smashCD;
        AudioManager.instance.PlayAbomMusic();

        player = GameObject.FindGameObjectWithTag("Player");
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
                AudioManager.instance.PlaySFX(17);

                StartCoroutine(Transform1());
                
                transform = true;
            }
            if(smashCounter > 0)
            {
                smashCounter -= Time.deltaTime;
                attacking = false;
            }
            else
            {
                attacking = true;
                Instantiate(vampireSmash, smashPoint.position,vampireSmash.transform.rotation);
                smashCounter = smashCD;
            }

        }
        else if (DemonHealthContrroller.instance.currentHealth <= threshold2 && DemonHealthContrroller.instance.currentHealth >0)
        {
            if(!transform2)
            {
                StartCoroutine(Transform2());
                transform2 = true;
            }


        }
        else
        {
            theCam.enabled = true;
        }
    }

    


    public IEnumerator Transform1()
    {
        AudioManager.instance.PlayFinalBossMusic();
        Wizzard.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Vampire.SetActive(true);
        Instantiate(portal, Vampire.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Vampire.SetActive(false);
    }

    public IEnumerator Transform2()
    {
        Vampire.SetActive(true);
        yield return new WaitForSeconds(2f);
        Instantiate(Thunder, thunderPoint.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(17);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Thunder, thunderPoint.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(17);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Thunder, thunderPoint.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(17);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Thunder, thunderPoint.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(17);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Thunder, thunderPoint.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(17);
        yield return new WaitForSeconds(0.1f);
        Vampire.SetActive(false);
        Demon.SetActive(true);
        
    }


    

     
    
}


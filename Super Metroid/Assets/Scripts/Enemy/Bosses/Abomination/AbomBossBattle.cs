using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbomBossBattle : MonoBehaviour
{
    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;

    public GameObject reward;
    

    public static bool battleEnded;
    
    // Start is called before the first frame update
    void Start()
    {
        theCam = FindObjectOfType<CameraController>();
        theCam.enabled = false;
        AudioManager.instance.PlayAbomMusic();

    }

    // Update is called once per frame
    void Update()
    {
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camSpeed * Time.deltaTime);
        if(battleEnded)
        {
            reward.SetActive(true);
            reward.transform.SetParent(null);
            theCam.enabled = true;
            gameObject.SetActive(false);
            AudioManager.instance.PlayLevelMusic();
            PlayerController.Abomination = true;
        }
    }



    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallController : MonoBehaviour
{
    
    public GameObject bullet;
    private float counter;
    public float cd;
    public Transform shotPoint;
    // Start is called before the first frame update
    void Start()
    {
        counter = cd;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter >= cd)
        {
            
            Instantiate(bullet, shotPoint.position, Quaternion.identity);
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }
    }
}

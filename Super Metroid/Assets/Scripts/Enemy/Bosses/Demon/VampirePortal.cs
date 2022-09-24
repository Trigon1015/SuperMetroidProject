using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirePortal : MonoBehaviour
{
    
    public GameObject Thunder;
    

    public GameObject vampireSmash;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void Smashing()
    {
        Instantiate(vampireSmash, transform.position, Quaternion.identity);
    }

    public void ThunderE()
    {
        Instantiate(Thunder, new Vector2(transform.position.x, transform.position.y-7),Quaternion.identity);
    }

    
}

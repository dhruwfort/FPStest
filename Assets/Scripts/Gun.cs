using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerShoot();
    }

    void playerShoot()
    {

        if (Input.GetButton("Fire1"))
        {
            Vector3 bulletSpawn1 = transform.position;
          
            Vector3 test = new Vector3(0,0,1);

            RaycastHit hit;

            if (Physics.Raycast(bulletSpawn1, bulletSpawn.transform.forward, out hit))
            {
                if(hit.collider.gameObject.tag == "Targets")
                {
                    Destroy(hit.collider.gameObject);
                }
                
            }
        }
    }
}

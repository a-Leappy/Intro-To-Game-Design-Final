using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float timer, maxTimer;
    // Start is called before the first frame update
    void Start()
    {
        maxTimer = 5;
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }else
        {
            timer = maxTimer;
            SpawnPickup();
            //Do stuff when timer hits 0
        }
        {
            
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        }
    }
    public void SpawnPickup()
    {
        Instantiate(pickupPrefab, transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class pickupBahavior : MonoBehaviour
{
    public float newY;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        newY = Time.time * Mathf.Sin(Time.time);
        transform.position = new Vector3(transform.position.x, transform.position.y + (newY * 0.00005f), transform.position.z);
        Vector3 position = this.gameObject.GetComponent<Renderer>().bounds.center;
        this.gameObject.transform.RotateAround(Vector3.up, 1 * Time.deltaTime);
        //Objects going up and down
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //int rand = UnityEngine.Random.Range(1, 10);
            Destroy(this.gameObject);
            //Dissappears upon impact
        }
    }

}

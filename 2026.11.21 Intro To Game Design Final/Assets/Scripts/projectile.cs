using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Vector3 directionToPlayerVec;
    public float projectileSpeed;
    public bool isTracking;
    void Start()
    {
        Invoke("Suicide", 2);
    }

    void Update()
    {
        if (isTracking)
        {
            directionToPlayerVec.Normalize();
            GetComponent<Rigidbody>().velocity = directionToPlayerVec * projectileSpeed;
        }
        //Make a rigibdoby and set the Projectile Speed
    }
    public void Suicide()
    {
        CancelInvoke();
        Destroy(this.gameObject);
    }
    public void OnCollisionEnter(Collision other)
    {
        Suicide();
    }
}

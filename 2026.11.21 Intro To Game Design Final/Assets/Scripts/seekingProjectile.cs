using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekingProjectile : MonoBehaviour
{
    public GameObject Player;
    public Vector3 directionToPlayerVec;
    public float projectileSpeed;
    public bool isTracking;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        isTracking = true;
        Invoke("DisableTracking", 5);
        Invoke("Suicide", 6);
    }

    void Update()
    {
        if (isTracking)
        {
            directionToPlayerVec = Player.transform.position - transform.position;
            directionToPlayerVec.Normalize();
            GetComponent<Rigidbody>().velocity = directionToPlayerVec * projectileSpeed;
        }
        //Make a rigibdoby and set the Projectile Speed
    }
    public void DisableTracking()
    {
        isTracking = false;
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

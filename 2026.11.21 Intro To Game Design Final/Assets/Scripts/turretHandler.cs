using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretHandler : MonoBehaviour
{
    public GameObject projectile;
    public GameObject Player;
    public Vector3 directionToPlayerVec;
    public float timer, maxTimer;
    public float projectileSpeed;
    void Start()
    {
       timer = maxTimer;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        directionToPlayerVec = Player.transform.position - transform.GetChild(0).transform.position;
        directionToPlayerVec.Normalize();
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = maxTimer;
            PewPew();
        }
    }
    public void PewPew()
    {
        GameObject g = Instantiate(projectile, transform.GetChild(0).transform.position, Quaternion.identity);
        g.GetComponent<Rigidbody>().velocity = directionToPlayerVec * projectileSpeed;
        Collider projectileCollider = g.GetComponent<Collider>();
        Collider treeCollider = GetComponent<Collider>();

        if (projectileCollider != null && treeCollider != null)
        {
            Physics.IgnoreCollision(projectileCollider, treeCollider);
        }

        g.GetComponent<Rigidbody>().velocity = directionToPlayerVec * projectileSpeed;
    }
}
//Need to make an empty cild component

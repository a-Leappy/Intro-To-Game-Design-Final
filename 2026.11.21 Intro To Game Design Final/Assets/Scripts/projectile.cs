
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("Suicide", 2);
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

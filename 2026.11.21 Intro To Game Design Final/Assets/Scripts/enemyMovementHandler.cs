using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyMovementHandler : MonoBehaviour
{
    public GameObject Player;
    public Vector3 directionToPlayerVec;
    public float speed;
    public float distanceToPlayer;
    public float trackingDistance;
    public bool isInRange;
    public bool canJump;
    public float jumpForce;
    public float timer, maxTimer;
    public float knockbackForce;
    public int damage;
    public int enemyAttack;
    public int playerDefense;
    public int ogTrackingDistance;
    public int sneakingTrackingDistance;
    public float attackTimer, attackMaxTimer;
    public bool canAttack;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        timer = maxTimer;
        attackTimer = attackMaxTimer;
        canAttack = true;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);
        if (distanceToPlayer < trackingDistance)
        {
            isInRange = true;
            MoveTowardsPlayer();
        }else
        {
            isInRange = false; 
        }
        if (canJump == false)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            timer = maxTimer;
            canJump = true;
        }
        if (canAttack == false)
        {
            attackTimer -= Time.deltaTime;
        }
        if (attackTimer <= 0)
        {
            attackTimer = attackMaxTimer;
            canAttack = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            trackingDistance = sneakingTrackingDistance;
        }
        else
        {
            trackingDistance = ogTrackingDistance;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, trackingDistance);
    }
    public void MoveTowardsPlayer()
    {
        directionToPlayerVec = Player.transform.position - transform.position;
        directionToPlayerVec.Normalize();
        GetComponent<Rigidbody>().velocity = new Vector3(directionToPlayerVec.x * speed, GetComponent<Rigidbody>().velocity.y, directionToPlayerVec.z * speed);
        if (Player.transform.position.y > transform.position.y && canJump == true)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(directionToPlayerVec.x, jumpForce, directionToPlayerVec.z), ForceMode.Impulse);
            canJump = false;
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        damage = (int)((enemyAttack * enemyAttack) / playerDefense);
        if (damage <= 0)
        {
            damage = 1;
        }
        if (other.gameObject.CompareTag("Player") && canAttack == true)
        {
            Camera.main.GetComponent<cameraShake>().ShakeCamera();
            other.gameObject.GetComponent<Rigidbody>().AddForce(directionToPlayerVec * knockbackForce, ForceMode.Force);
            canAttack = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeBehaviour : MonoBehaviour
{
    public string playerName;
    // Names and stuff
    public int playerHealth;
    // Whole numbers
    public int playerDamage;
    public float playerSpeed;
    // Decimals
    [SerializeField]
    private bool isPlayerDead;
    // True or Flase

    public List<string> listOfBossNames;
    public GameObject Player;
    public Vector3 directionToEnemyVec;
    public Rigidbody rb;
    public enum playerState
    {
        PAUSED,
        PLAYING,
        CUTSCENE,
        DEATH
    }
    public playerState pS;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerHealth <= 0 || isPlayerDead)
        {
            pS = playerState.DEATH;
        }
        //Debug.Log("Hello World");
        //Debug.Log(playerName);
        listOfBossNames.RemoveAt(0);

        if (isPlayerDead == false)
        {
           // Debug.Log("Player is Alive");
        }
        else
        {
           // Debug.Log("Player Died");
        }
        {
            if (pS == playerState.PAUSED)
            {
                //Debug.Log("Paused");
            }
            else if (pS == playerState.CUTSCENE)
            {
                //Debug.Log("Cutscene");
            }
            else if (pS == playerState.DEATH)
            {
                //Debug.Log("Playing");
            }
            else if (pS == playerState.DEATH)
            {
                //Debug.Log("Dead LMAO");
            }
            DebugBosses();
            int temp = Sum(3,4);
        }
        // Update is called once per frame
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector3.forward;
        }
    }
    //DONT PUT MULTIPLE VIODS IN THE SAME BRACKET
    public void DebugBosses()
    {
       for (int i = 0; i < listOfBossNames.Count; i++)
          {
                Debug.Log("Boss Number: " + i + " = " + listOfBossNames[i]);
          }
    }
    public int Sum(int a, int b)
        {
            return a + b;
        }
        //Public void won't return anything
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehavior : MonoBehaviour
{
    public int coinWorth;
    public GameObject gameManager;
    public static event Action<int> coinCall;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        
    }
    public void OnDisable()
    {
        audioManager._Instance.PlaySound("Pickup_Coin");
        coinCall?.Invoke(0);
        gameManager.GetComponent<gameManager>().GetCoin(coinWorth);
    }
}

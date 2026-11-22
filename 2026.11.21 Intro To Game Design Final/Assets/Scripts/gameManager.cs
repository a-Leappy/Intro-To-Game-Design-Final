using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager _Instance;
    public TMPro.TMP_Text healthTXT;
    public TMPro.TMP_Text coinTXT;
    public TMPro.TMP_Text xpTXT;
    public TMPro.TMP_Text defenseTXT;
    public int playerHealth;
    public int playerDefense;
    public int numCoins;
    public float timer, maxTimer;
    public bool isTutorialBossDead;
    public variableHandler vH;
    public enemyMovementHandler enemyMovementHandler;
    public int xp;
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            //game manager is seen in all scenes
        }
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            ResetVariableHandler();
        }
        isTutorialBossDead = false;
        healthTXT.text = "HP: " + playerHealth;
        coinTXT.text = "Coins:" + numCoins;
        xpTXT.text = "XP: " + xp;
        defenseTXT.text = "Def: " + playerDefense;
        numCoins = vH.coins;
        xp = vH.xp;
        playerDefense = vH.defense;
        //playerHealth = vH.health;
    }
    private void Update()
    {

    }
    public void LoseHealth(int dmg)
    {
        playerHealth -= dmg;
        //playerHealth = vH.health
        healthTXT.text = "HP: " + playerHealth;
    }
    public void GetCoin(int num)
    {
        numCoins += num;
        vH.coins = numCoins;
        coinTXT.text = "Coins:" + numCoins;
    }
    public void GainXp(int num)
    {
        xp += num;
        xp = vH.xp;
        xpTXT.text = "XP: " + xp;
    }
    public void DefenseCalculating(int num)
    {
        playerDefense = vH.defense;
        playerDefense = enemyMovementHandler.playerDefense;
        defenseTXT.text = "Def: " + playerDefense;
    }
    public void ExitTutorial()
    {
        if (isTutorialBossDead == true)
        {
            SceneManager.LoadScene("MainHub");
            Debug.Log("Tutorial Complete");
        }
    }
    public void ResetVariableHandler()
    {
        vH.coins = 0;
        vH.xp = 0;
        //vH.health = 10
    }
}

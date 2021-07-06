using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseHandler : MonoBehaviour
{
    public static PurchaseHandler instance;
    public int totalCoins;
    public Text totalCoinsText;

    void Start() {
    	//totalCoinsText = GameObject.FindWithTag("totalCoinsTextStore").GetComponent<Text>();
    	instance = this;
        totalCoins = PlayerPrefs.GetInt("Total Coins");
    }
    void Awake() {
    	/*if (instance == null) {
    		instance = this;
    		//totalCoinsText = GameObject.FindWithTag("totalCoinsTextStore").GetComponent<Text>();
    		DontDestroyOnLoad(gameObject);
    	}*/
    	/*else{
    		Destroy(gameObject);
    	}*/
    }
    
    
    public void useCoins(int amount) {
    	totalCoins-=amount;
    	PlayerPrefs.SetInt("Total Coins",totalCoins);
        PlayerPrefs.Save();
    }

    public bool hasEnoughCoins(int amount) {
        totalCoins = PlayerPrefs.GetInt("Total Coins");
        if (totalCoins >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    	//return(totalCoins>=amount);
    } 

    void Update() {
    	totalCoins = PlayerPrefs.GetInt("Total Coins");
    	/*if (totalCoinsText != null) {
    		totalCoinsText.text = totalCoins.ToString();
    	}*/
        totalCoinsText.text = totalCoins.ToString();
    }
    
}

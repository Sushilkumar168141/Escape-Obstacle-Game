using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCoins : MonoBehaviour
{
    public int totalCoins;
    public Text totalCoinsText;
    void Start() {
    	if (!PlayerPrefs.HasKey("Total Coins")) {
    		PlayerPrefs.SetInt("Total Coins",0);
    	}
    	else {
    		totalCoins = PlayerPrefs.GetInt("Total Coins");
    	}
    	totalCoinsText.text = "Coins : "+PlayerPrefs.GetInt("Total Coins").ToString();
    	Debug.Log("Total Coins : "+totalCoins);
    }

    private void Update()
    {
        totalCoins = PlayerPrefs.GetInt("Total Coins");
        //totalCoinsText.text = "Coins : " + totalCoins.ToString();
        totalCoinsText.text = totalCoins.ToString();
    }
}

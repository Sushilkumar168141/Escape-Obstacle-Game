using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public int TotalCoins;
	public int cost ;
	public int TimesRevived;
	public int remainingCoins;
	public Image timerImage;
	private float timeAmt = 5f;
	private float time;
	private bool start = false;
	public gamaManager gm;
	public GameObject anotherChancePanel;
	public player_movement movement;
	public Text costText;
	public GameObject lessCoinsPanel;
    // Start is called before the first frame update
    void Start()
    {
        time = timeAmt;
        cost = 50;
        //gm = GetComponent<gamaManager>();
        gm = GameObject.FindWithTag("GameController").GetComponent<gamaManager>();
        //startTimer();
    }

    // Update is called once per frame
    void Update()
    {
    	if(start) {
    		if (time <=0 ) {
    			gm.gameEnd();
    			//gm.gameOverUI.SetActive(true);
    		}
	        else if (time > 0) {
				time-=Time.deltaTime;
				timerImage.fillAmount = (time/timeAmt);
				//print("Time : "+time);
			}
		}
		//costText.text = (cost).ToString() + " Coins";
		costText.text = (cost).ToString();
		//print("TimesRevived : "+TimesRevived);
    }

    public void startTimer() {
    	start = true;
    }

    public void stopTimer() {
    	start = false;
    }

    public void onPressTimerOkButton() {
    	TimesRevived++;
    	//print("Times Revived : "+TimesRevived);
    	TotalCoins = PlayerPrefs.GetInt("Total Coins");
    	if (TotalCoins < cost) {
    		lessCoinsPanel.SetActive(true);
    	}
    	else {
	    	remainingCoins = TotalCoins - cost;
	    	TotalCoins = remainingCoins;
	    	PlayerPrefs.SetInt("Total Coins",TotalCoins);
	    	PlayerPrefs.Save();
	    	anotherChancePanel.SetActive(false);
	    	movement.enabled = true;
	    	time = timeAmt;
	    	cost = cost * 2;
	    	print("Cost : "+cost);
	    }
    }

    public void onPressCancelButton() {
    	gm.gameEnd();
    }

    public void onPressLessCoinsOkButton() {
    	lessCoinsPanel.SetActive(false);
    	gm.gameEnd();
    }

}

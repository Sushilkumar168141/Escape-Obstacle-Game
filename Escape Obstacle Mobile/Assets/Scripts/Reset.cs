using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
	public GameObject infoPanel;
    public GameObject ResetAlertPanel;
	const string privateCode = "2jaE7O_5PEKqv43ybU3p4As3UaFxvHEUGmT_NorsubSw";
	const string publicCode = "5f31820beb371809c4be19cf";
	const string webURL = "http://dreamlo.com/lb/";
	private string username;
	private int highScore;
    // Start is called before the first frame update
    void Start()
    {
    	username = PlayerPrefs.GetString("Name");
    	highScore = PlayerPrefs.GetInt("High Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetData() {
    	DeleteUserFromLeaderboard(username);
    	PlayerPrefs.DeleteAll();
        ResetAlertPanel.SetActive(false);
    	Debug.Log("All PlayerPrefs cleared.");
    	Application.Quit();
    	Debug.Log("Application quitted.");
    	//infoPanel.SetActive(true);
    }

    public void DeleteUserFromLeaderboard(string _username) {
    	StartCoroutine(DeleteUser(_username));
    }

    IEnumerator DeleteUser(string _username) {
    	WWW www = new WWW(webURL+privateCode+"/delete/"+WWW.EscapeURL(_username));
    	yield return www;

    	if (string.IsNullOrEmpty(www.error)) {
    		print("User deleted from leaderboard successfully");
    	}

    	else {
    		print("Error deleting user : "+www.error);
    	}
    }

    public void showAlertPanel() {
        ResetAlertPanel.SetActive(true);
    }

    public void hideAlertPanel() {
        ResetAlertPanel.SetActive(false);
    }
}

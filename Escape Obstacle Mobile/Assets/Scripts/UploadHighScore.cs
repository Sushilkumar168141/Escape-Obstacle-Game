using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadHighScore : MonoBehaviour
{
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

    void Awake() {
    	//AddNewHighScore("RAJ",89);
    	//AddNewHighScore("RAJ1",80);
    	//AddNewHighScore("RAJ2",84);
        AddNewHighScore(username, highScore);
    }

    public void AddNewHighScore(string _username, int _highScore) {
    	StartCoroutine(UploadNewHighScore(_username, _highScore));
    }

    IEnumerator UploadNewHighScore(string _username, int _highScore) {
    	WWW www = new WWW(webURL+privateCode+"/add/"+WWW.EscapeURL(_username)+"/"+_highScore);
    	yield return www;

    	if(string.IsNullOrEmpty(www.error)) {
    		print("Upload Successful");
    	}

    	else {
    		print("Error Uploading : "+www.error);
    	}
    }	
}
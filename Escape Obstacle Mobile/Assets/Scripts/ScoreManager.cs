using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	const string privateCode = "2jaE7O_5PEKqv43ybU3p4As3UaFxvHEUGmT_NorsubSw";
	const string publicCode = "5f31820beb371809c4be19cf";
	const string webURL = "http://dreamlo.com/lb/";
	private string username;
	private int highScore;
	public HighScore[] highsScoresList;
	public DisplayHighScore highScoreDisplay;
	public GameObject leaderboardPanel;
	public List<string> usernames = new List<string>();
	public string topPlayerName;
	public CheckTop check;
	public bool isTopPlayer = false;
	//static ScoreManager instance;
    // Start is called before the first frame update
    void Start()
    {
    	if (!PlayerPrefs.HasKey("isTop")) {
    		PlayerPrefs.SetInt("isTop",0);
    	}
        if (!PlayerPrefs.HasKey("Already topped")) {
            PlayerPrefs.SetInt("Already topped",0);
        }
    	PlayerPrefs.Save();
    	username = PlayerPrefs.GetString("Name");
    	highScore = PlayerPrefs.GetInt("High Score");    
    	check = GetComponent<CheckTop>();
    }

    void Awake() {
    	highScoreDisplay = GetComponent<DisplayHighScore>();
    	//instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   	public void showLeaderboardPanel() {
   		DownloadHighScores();
   		leaderboardPanel.SetActive(true);
   	}

   	public void hideLeaderboardPanel() {
   		leaderboardPanel.SetActive(false);
   	}

    public void DownloadHighScores() {
    	StartCoroutine("DownloadHighScoresFromDatabase");
    }

    IEnumerator DownloadHighScoresFromDatabase() {
    	WWW www = new WWW(webURL + publicCode + "/pipe/");
    	yield return www;

    	if (string.IsNullOrEmpty(www.error)) {
    		//print(www.text);
    		FormatHighScores(www.text);
    		highScoreDisplay.OnHighScoresDownloaded(highsScoresList);

    	}

    	else {
    		print("Error Downloading HighScore : "+www.error);
    	}
    }

    void FormatHighScores(string textStream) {
    	string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
    	highsScoresList = new HighScore[entries.Length];

    	for(int i=0;i<entries.Length;i++) {
    		string[] entryInfo = entries[i].Split(new char[] {'|'});
    		string username = entryInfo[0];
    		username = username.Replace("+"," ");
    		usernames.Add(username);
    		int score = int.Parse(entryInfo[1]);
    		highsScoresList[i] = new HighScore(username, score);
    		print(highsScoresList[i].username + " : " +highsScoresList[i].score);
    	}
    	/*foreach(string name in usernames) {
    		print(name);
    	}*/
    	topPlayerName = usernames[0];
    	//print("Top Player : "+topPlayerName);
    	isTopPlayer =  check.checkTop();
    	if(isTopPlayer) {
    		PlayerPrefs.SetInt("isTop",1);
            PlayerPrefs.SetInt("Already topped",1);
    	}
    	else {
    		PlayerPrefs.SetInt("isTop",0);
    	}
    }



}

public struct HighScore {
	public string username;
	public int score;

	public HighScore(string _username, int _score) {
		username = _username;
		score = _score;
	}
}
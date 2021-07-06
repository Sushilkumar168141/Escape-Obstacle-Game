using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTop : MonoBehaviour
{
	public ScoreManager sm;
	public string TopPlayerName;
    public string username;
    public GameObject AchievementPanel;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("ScoreManagerDisplayHighScores").GetComponent<ScoreManager>();
        TopPlayerName = sm.topPlayerName;
        username = PlayerPrefs.GetString("Name");
        //checkTop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() {
    	//TopPlayerName = sm.usernames[0];
        //checkTop();
    }

    public bool checkTop(){
        if (sm.topPlayerName == username) {
            print("Congratulations. You are at the top of the leaderboard");
            return true;
        }
        else {
            return false;
        }
    }

    public void showAchievementPanel() {
        AchievementPanel.SetActive(true);
    }
}

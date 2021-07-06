using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScore : MonoBehaviour
{
    // Start is called before the first frame update
    public Text[] highScoreFields;
    public ScoreManager sm;
    void Start()
    {
        sm = GetComponent<ScoreManager>();
        for (int i=0;i<highScoreFields.Length;i++) {
        	highScoreFields[i].text = (i+1)+". Fetching...";
        }
        StartCoroutine("RefreshHighScores");
    }

    IEnumerator RefreshHighScores() {
    	while (true) {
    		sm.DownloadHighScores();
    		yield return new WaitForSeconds(30f);
    	}
    }

    public void OnHighScoresDownloaded(HighScore[] highScoreList) {
    	for (int i=0;i<highScoreFields.Length;i++) {
    		highScoreFields[i].text = (i+1) + ". ";
    		if(i<highScoreList.Length) {
    			highScoreFields[i].text += highScoreList[i].username + " - " + highScoreList[i].score;
    		}
    	}
    }

    
}

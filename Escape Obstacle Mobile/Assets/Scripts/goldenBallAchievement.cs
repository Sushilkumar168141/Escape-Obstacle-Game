using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldenBallAchievement : MonoBehaviour
{
	public GameObject AchievementPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showAchievementPanel() {
    	AchievementPanel.SetActive(true);
    }

    public void hideAchievementPanel() {
    	AchievementPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
	public GameObject AchievementPanel;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("isTop") == 1) {
        	AchievementPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

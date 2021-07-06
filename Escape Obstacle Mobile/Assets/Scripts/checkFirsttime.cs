using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkFirsttime : MonoBehaviour
{
	public GameObject InfoPanel;
    public Text NameText;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("First Time : "+PlayerPrefs.GetString("First Time"));
        Debug.Log("Name : "+PlayerPrefs.GetString("Name"));

    }

    // Update is called once per frame
    void Update()
    {
        NameText.text = PlayerPrefs.GetString("Name");
    }

    public void Awake() {
    	if(!PlayerPrefs.HasKey("First Time")) {
    		PlayerPrefs.SetString("First Time", "No");
    		InfoPanel.SetActive(true);
    	}
    	else{
    		InfoPanel.SetActive(false);
    	}
    	PlayerPrefs.Save();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
	public GameObject nameInput;
	public GameObject AlertPanel;
	public string name;
    public ScoreManager sm;
    public GameObject NameErrorPanel;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("ScoreManagerDisplayHighScores").GetComponent<ScoreManager>();
        NameErrorPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetNameInput() {
    	try {
    		name = nameInput.GetComponent<Text>().text;
    		if (name.Length != 0) {
        		print(name);
                foreach(string alreadyName in sm.usernames) {
                    if(name == alreadyName) {
                        //print("Name already exist");
                        NameErrorPanel.SetActive(true);
                        return;
                    }
                }
        		if (!PlayerPrefs.HasKey("Name")) {
        			PlayerPrefs.SetString("Name", name.ToString());
                    PlayerPrefs.Save();
        		}
        		gameObject.SetActive(false);
        	}
        	else {
        		AlertPanel.SetActive(true);
        	}
        }
        catch (System.Exception e) {
        	print(e.ToString());
        }
    }
    public void onPressOkButton() {
    	AlertPanel.SetActive(false);
    }

    public void hideNameErrorPanel() {
        NameErrorPanel.SetActive(false);
    }
}

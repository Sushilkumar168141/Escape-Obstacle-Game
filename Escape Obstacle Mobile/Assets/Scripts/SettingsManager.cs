using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SettingsManager : MonoBehaviour
{
	public GameObject ControlPanel;

	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject jumpButton;
	public Touch touch;
	public Vector3 currentPosition;
	public RectTransform leftButtonRT;
    public RectTransform rightButtonRT;
    public RectTransform jumpButtonRT;

	public bool leftButtonClicked = false;
	public bool rightButtonClicked = false;
	public bool jumpButtonClicked = false;

	public Text messageText;

	//[System.Serializable]
	//public Vector3 leftButtonPosition, rightButtonPosition, jumpButtonPosition;

    // Start is called before the first frame update
    void Start()
    {
    	messageText.gameObject.SetActive(false);
    	LoadGame();
        Debug.Log("Left Button Position : "+leftButton.transform.position);
        Debug.Log("right Button Position : "+rightButton.transform.position);
        Debug.Log("Jump Button Position : "+jumpButton.transform.position);
        /*if (!PlayerPrefs.HasKey("Left Button Position X")) {
        	PlayerPrefs.SetFloat("Left Button Position X", leftButton.transform.position.x);
        }*/
        //leftButtonRT = leftButton.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.touchCount > 0) {
        	touch = Input.GetTouch(0);
        	if (touch.phase == TouchPhase.Moved) {
        		currentPosition = touch.position;
        		if (leftButtonClicked) {
        			leftButton.transform.position = currentPosition;
        		}

        		else if (rightButtonClicked) {
        			rightButton.transform.position = currentPosition;
        		}

        		else if (jumpButtonClicked) {
        			jumpButton.transform.position = currentPosition;
        		}
        		Debug.Log(currentPosition);
        	}
        }
    }

    public void onPressControlButton() {
    	ControlPanel.SetActive(true);
    }

    public void ControlManager() {
    	//PlayerPrefs
    }

    public void changeLeftButtonPosition() {
    	leftButtonClicked = true;
    	rightButtonClicked = false;
    	jumpButtonClicked = false;
    }

    public void changeRightButtonPosition() {
    	leftButtonClicked = false;
    	rightButtonClicked = true;
    	jumpButtonClicked = false;
    }

    public void changeJumpButtonPosition() {
    	leftButtonClicked = false;
    	rightButtonClicked = false;
    	jumpButtonClicked = true;
    }

    public void SaveGame() {
    	BinaryFormatter bf = new BinaryFormatter();
    	FileStream file = File.Create(Application.persistentDataPath + "/ButtonsPosition.dat");
    	SaveData data = new SaveData();
    	data.leftButtonPosition.Clear();
    	data.rightButtonPosition.Clear();
    	data.jumpButtonPosition.Clear();
    	/*data.leftButtonPosition[0] = leftButton.transform.position.x;
    	data.leftButtonPosition[1] = leftButton.transform.position.y;
    	data.leftButtonPosition[2] = leftButton.transform.position.z;
    	data.rightButtonPosition[0] = rightButton.transform.position.x;
    	data.rightButtonPosition[1] = rightButton.transform.position.y;
    	data.rightButtonPosition[2] = rightButton.transform.position.z;
    	data.jumpButtonPosition[0] = jumpButton.transform.position.x;
    	data.jumpButtonPosition[1] = jumpButton.transform.position.y;
    	data.jumpButtonPosition[2] = jumpButton.transform.position.z;*/

    	data.leftButtonPosition.Add(leftButton.transform.position.x);
    	data.leftButtonPosition.Add(leftButton.transform.position.y);
    	data.leftButtonPosition.Add(leftButton.transform.position.z);
    	data.rightButtonPosition.Add(rightButton.transform.position.x);
    	data.rightButtonPosition.Add(rightButton.transform.position.y);
    	data.rightButtonPosition.Add(rightButton.transform.position.z);
    	data.jumpButtonPosition.Add(jumpButton.transform.position.x);
    	data.jumpButtonPosition.Add(jumpButton.transform.position.y);
    	data.jumpButtonPosition.Add(jumpButton.transform.position.z);
    	bf.Serialize(file, data);
    	file.Close();
    	Debug.Log("Game Data Saved ");

    	StartCoroutine("ShowMessage");

    }

    IEnumerator ShowMessage() {
    	messageText.text = "Buttons Position changed.";
    	messageText.gameObject.SetActive(true);
    	yield return new WaitForSeconds(1f);
    	messageText.gameObject.SetActive(false);

    }

    public void LoadGame() {
        if (File.Exists(Application.persistentDataPath + "/ButtonsPosition.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ButtonsPosition.dat", FileMode.Open);
            SaveData data = (SaveData)(bf.Deserialize(file));
            leftButton.transform.position = new Vector3(data.leftButtonPosition[0],data.leftButtonPosition[1],data.leftButtonPosition[2]);
            rightButton.transform.position = new Vector3(data.rightButtonPosition[0],data.rightButtonPosition[1],data.rightButtonPosition[2]);
            jumpButton.transform.position = new Vector3(data.jumpButtonPosition[0],data.jumpButtonPosition[1],data.jumpButtonPosition[2]);
            Debug.Log("Game Data Loaded ");
            file.Close();
        }
        else {
        	Debug.Log("There is no saved data! ");
        }
    }

    public void ResetControls() {
        /*if (File.Exists(Application.persistentDataPath + "/ButtonsPosition.dat")) {
            File.Delete(Application.persistentDataPath + "/ButtonsPosition.dat");
            leftButton.transform.position = new Vector3(-290.2f,306f,0f);
            rightButton.transform.position = new Vector3(283f,304f,0f);
            jumpButton.transform.position = new Vector3(-1f,429f,0f);
            Debug.Log("Data Reset Completed");
            //SaveGame();
        }*/
        /*leftButton.transform.position = new Vector3(61.6038f, 64f, 0.0f);
        rightButton.transform.position = new Vector3(238.2537f, 64f, 0.0f);
        jumpButton.transform.position = new Vector3(152.6037f, 101f, 0.0f);*/

        leftButtonRT.anchoredPosition = new Vector3(61.6038f, 103f, 0.0f);
        rightButtonRT.anchoredPosition = new Vector3(238.2537f, 103f, 0.0f);
        jumpButtonRT.anchoredPosition = new Vector3(152.6037f, 140f, 0.0f);

        Debug.Log("Data Reset Completed");
        SaveGame();
    }

    public void onPressControlsBackButton() {
    	ControlPanel.SetActive(false);
    }

    /*public void onPressCancelButton() {
    	ControlPanel.SetActive(false);
    }*/
}

/*[System.Serializable]
public class SaveData {
	public List<float> leftButtonPosition = new List<float>(3) {0f,0f,0f};
	public List<float> rightButtonPosition = new List<float>(3) {0f,0f,0f};
	public List<float> jumpButtonPosition = new List<float>(3){0f,0f,0f};

}*/
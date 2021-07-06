using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
	public Button powerupButton;
	public Button characterButton;
	public GameObject powerupPanel;
	public GameObject charactersPanel;

    // Buttons to view which panel
    public Button ballButton; // For ball panel
    public Button obstacleButton; // For obstaclesPanel
    public GameObject ballPanel;
    public GameObject obstaclesPanel;
    // Start is called before the first frame update
    void Start()
    {
        //powerupButton.Select();
        //powerupButton.onClick.Invoke();

        characterButton.Select();
        characterButton.onClick.Invoke();
    	//charactersPanel.SetActive(false);
        //powerupPanel.SetActive(true);
    }

    void Awake() {
    	//characterButton.Select();
    	//characterButton.onClick.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPressPowerupsButton() {
    	//powerupButton.Select();
    	//charactersPanel.SetActive(false);
    	//powerupPanel.SetActive(true);
    }

    public void onPressCharactersButton() {
    	characterButton.Select();
    	powerupPanel.SetActive(false);
    	charactersPanel.SetActive(true);
        ballButton.Select();
    }

    public void onPressBallButton() {
        obstaclesPanel.SetActive(false);
        ballPanel.SetActive(true);

    }

    public void onPressObstacleButton() {
        ballPanel.SetActive(false);
        obstacleButton.Select();
        obstaclesPanel.SetActive(true);
    }
}

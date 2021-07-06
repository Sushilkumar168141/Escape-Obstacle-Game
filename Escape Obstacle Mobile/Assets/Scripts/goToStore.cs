using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToStore : MonoBehaviour
{
	public GameObject storePanel;
	public Button powerupButton;

	void Start() {

	}


	public void showStorePanel() {
		storePanel.SetActive(true);
		powerupButton.Select();
		powerupButton.onClick.Invoke();
	}

	public void hideStorePabel() {
		storePanel.SetActive(false);
	}
}

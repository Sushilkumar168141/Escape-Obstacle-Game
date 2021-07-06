using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Save : MonoBehaviour {
	void Start() {
		Debug.Log(Application.persistentDataPath);
		SaveData save = new SaveData();
	}
}

[System.Serializable]
public class SaveData {
	public List<float> leftButtonPosition = new List<float>(3) {0f,0f,0f};
	public List<float> rightButtonPosition = new List<float>(3) {0f,0f,0f};
	public List<float> jumpButtonPosition = new List<float>(3){0f,0f,0f};

	public SaveData() {
		this.leftButtonPosition = new List<float> {0f, 0f, 0f};
		this.rightButtonPosition = new List<float> {0f, 0f, 0f};
		this.jumpButtonPosition = new List<float> {0f, 0f, 0f};
	}

}
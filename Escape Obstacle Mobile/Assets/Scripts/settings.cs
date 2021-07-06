using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
	public AudioSource audio;
	public bool music;
	public bool vibration;
	public GameObject SettingsPanel;
	public Toggle soundToggle;
	public Toggle vibrationToggle;

	void Start() {
		//audio = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
		if (!PlayerPrefs.HasKey("Sound")) {
			PlayerPrefs.SetInt("Sound",1);
			music=true;
		}
		else {
			if(PlayerPrefs.GetInt("Sound")==0) {
				music = false;
				PauseMusic(audio);
			}
			else if (PlayerPrefs.GetInt("Sound") == 1) {
				music=true;
				PlayMusic(audio);
			}
		}
		if (!PlayerPrefs.HasKey("Vibration")) {
			PlayerPrefs.SetInt("Vibration",1);
			vibration=true;
		}
		else {
			if(PlayerPrefs.GetInt("Vibration")==0) {
				vibration = false;
				//PauseMusic(audio);
			}
			else if (PlayerPrefs.GetInt("Vibration") == 1) {
				vibration = true;
				//PlayMusic(audio);
			}
		}
		PlayerPrefs.Save();
		soundToggle.isOn = music;
		vibrationToggle.isOn = vibration;
		soundToggle.onValueChanged.AddListener(delegate {
			toggleMusic();
			});

		vibrationToggle.onValueChanged.AddListener(delegate {
			toggleVibration();
			});

		/*if (music) {
			audio = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
			PlayMusic();
		}
		else if (!music) {
			PauseMusic();
		}*/
		Debug.Log("Music : "+PlayerPrefs.GetInt("Sound"));
	}

	void Update() {
		
		//Debug.Log("Music : "+PlayerPrefs.GetInt("Sound"));
	}
    
    public void toggleMusic() {
    	Debug.Log("Inside toggle function");
    	music = !music;	
    	soundToggle.isOn = music;
    	if (PlayerPrefs.GetInt("Sound") == 0) {
			if (music) {
				PlayerPrefs.SetInt("Sound",1);
				PlayMusic(audio);
			}
			/*else if (!music) {
				PauseMusic(audio);
			}*/
		}
		else if (PlayerPrefs.GetInt("Sound") == 1) {
			if (!music) {
				PlayerPrefs.SetInt("Sound",0);
				PauseMusic(audio);
			}
			/*else if (music) {
				PlayMusic(audio);
			}
*/		}
		PlayerPrefs.Save();
		
    }

    public void toggleVibration() {
    	Debug.Log("Inside toggle vibration function");
    	vibration = !vibration;	
    	vibrationToggle.isOn = vibration;
    	if (PlayerPrefs.GetInt("Vibration") == 0) {
			if (vibration) {
				PlayerPrefs.SetInt("Vibration",1);
				//PlayMusic(audio);
			}
			/*else if (!music) {
				PauseMusic(audio);
			}*/
		}
		else if (PlayerPrefs.GetInt("Vibration") == 1) {
			if (!vibration) {
				PlayerPrefs.SetInt("Vibration",0);
				//PauseMusic(audio);
			}
			/*else if (music) {
				PlayMusic(audio);
			}
*/		}
		PlayerPrefs.Save();
		
    }

    public void PlayMusic(AudioSource audio) {
    	audio.Play();
    	return;
    }

    public void PauseMusic(AudioSource audio) {
    	//audio.Pause();
    	audio.Stop();
    	return;
    }

    public void showSettingsPanel() {
    	SettingsPanel.SetActive(true);
    }

    public void hideSettingsPanel() {
    	SettingsPanel.SetActive(false);
    }
}

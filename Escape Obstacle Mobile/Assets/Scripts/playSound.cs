using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playSound : MonoBehaviour
{
	public AudioSource audio;
    public bool music;
    public bool vibration;
    public Toggle vibrationToggle;
    public Toggle soundToggle;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Sound") == 0) {
            music = false;  
            stopSound(); 
        }

        if (PlayerPrefs.GetInt("Sound") == 1) {
            music = true;
            PlaySound();
        }
        soundToggle.isOn = music;

        if (PlayerPrefs.GetInt("Vibration") == 0) {
            vibration = false;  
            //stopSound(); 
        }

        if (PlayerPrefs.GetInt("Vibration") == 1) {
            vibration = true;
            //PlaySound();
        }
        vibrationToggle.isOn = vibration;
    }

    // Update is called once per frame
    void Update()
    {	
	    /*if (PlayerPrefs.GetInt("Vibration") == 0) {
            vibration = false;  
            //stopSound(); 
        }

        if (PlayerPrefs.GetInt("Vibration") == 1) {
            vibration = true;
            //PlaySound();
        }
        vibrationToggle.isOn = vibration;   */
 	}
    public void stopSound() {
        audio.Stop();
    }

    public void PlaySound() {
        audio.Play();
    }

    public void toggelSound() {
        music = soundToggle.isOn ;
        if (music) {
            PlayerPrefs.SetInt("Sound",1);
            PlaySound();
        } 
        else if (!music) {
            PlayerPrefs.SetInt("Sound",0);
            stopSound();
        }
        PlayerPrefs.Save();
    }

    public void toggleVibration() {
        vibration = vibrationToggle.isOn;
        if (vibration) {
            PlayerPrefs.SetInt("Vibration",1);
        }
        else if (!vibration) {
            PlayerPrefs.SetInt("Vibration",0);
        }
        PlayerPrefs.Save();
    }
}

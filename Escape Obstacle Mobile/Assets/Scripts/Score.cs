using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    //public GameManager gameManager; 
    void Start() {
        //scoreText.text = GameObject.Find("GameManager").GetComponent<gamaManager>().score.ToString();
        //scoreText.text = "10";
        //Debug.Log(GetComponent<gamaManager>().score.ToString());
    }
}

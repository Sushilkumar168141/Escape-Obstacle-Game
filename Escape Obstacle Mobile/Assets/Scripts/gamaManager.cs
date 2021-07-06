using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using System;




public class gamaManager : MonoBehaviour
{
    public SpawnEnemy spawnScript;
    public GameObject Player;
    public Text currentScore;
    public Text HighScore;
    public Vector3 startPosition;
    public int enemyCountOnScreen;
    public List<GameObject> enemy;
    public float RestartDelay = 2f;
    public bool gameHasEnded = false;
    public bool  isPaused = false;
    public Text scoreText;
    public Text coinsText;
    //public Text scoreText2;
    public int score=0;
    public GameObject pauseButton;
    private Animator myAnimator;
    public GameObject gameOverUI;
    public GameObject pausePanel;
    public GameObject settingPanel;
    public UploadHighScore sm;
    public string userName;
    public int highScore;
    public float forwardForce = 40f;
    public List<float> speedList;
    public int coinCount;
    public int enemyCount;
    public GameObject oldGround;
    public GameObject Ground;
    public GameObject NewGround;
    public GameObject oldBackGround;
    public GameObject BackGround;
    public GameObject NewBackGround;
    public Timer timer;
    public GameObject anotherChancePanel;
    public GameObject RestartPanel;
    public float gameTime;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
        speedList = new List<float>{5,10,20,30,35};
    	if (!PlayerPrefs.HasKey("High Score")) {
    		PlayerPrefs.SetInt("High Score", 0);
    		PlayerPrefs.Save();
    	}
    	Debug.Log("High Score : "+PlayerPrefs.GetInt("High Score"));
        startPosition = Player.transform.position;
        myAnimator = GetComponent<Animator>();
        pausePanel.SetActive(false);
        sm = GameObject.Find("UploadHighScore").GetComponent<UploadHighScore>();
        anotherChancePanel.SetActive(false);
    }

    public void changePauseMode() {
        isPaused = !isPaused;
        pausePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //gameTime+=Time.deltaTime;
        //Debug.Log("GameTime : "+gameTime);
        //enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (Input.GetKeyDown("escape")) {
            //Debug.Log("Escape key was pressed");
            changePauseMode();
        }
        enemyCountOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //Debug.Log("Enemy count  on screen is : "+enemyCountOnScreen);
        /*if (enemyCountOnScreen<3) {
            spawnScript.spawnEnemy();
        }*/
        if (isPaused) {
            onPause();
            return;
        }
        else if(!isPaused) {
        	onResume();
        }
        //scoreText.text = enemyCount.ToString();
        //scoreText.text = ((int)(Player.transform.position.z)).ToString();
        scoreText.text = ((int)(gameTime*10)).ToString();
        coinsText.text = coinCount.ToString();
        /* if (!isPaused) {
            spawnScript.DestroyObjectDelayed(spawnScript.a);
        } */

        if (Player.transform.position.z >= oldGround.transform.position.z+1000)
        {
            print("Player passes the ground");
            Destroy(oldGround.gameObject);
            oldGround = NewGround;
        }

        if (Player.transform.position.z >= oldBackGround.transform.position.z+2000)
        {
            print("Player passes the background");
            Destroy(oldBackGround.gameObject);
            oldBackGround = NewBackGround;
        }
    }

    // To get a random force of enemy from list of 3 numbers
    public float getRandomSpeed() {
        int index = Random.Range(0,speedList.Count);
        return speedList[index];
    }

    public void gameEnd() {
        //Time.timeScale = 0f;
        //onPause();
        if (gameHasEnded == false) {
            gameHasEnded = true;
            //timer.startTimer();
            stopEnemy();
            //Debug.Log("Forward force : "+forwardForce);
            Debug.Log("Coins collected : "+coinCount);
            PlayerPrefs.SetInt("Total Coins",PlayerPrefs.GetInt("Total Coins")+coinCount);
            Debug.Log("Game Ends");
            //FindObjectOfType<EnemyMovement>().enabled = false;
            //FindObjectOfType<SpawnEnemy>().enabled = false;
            //GameObject.Find("Main Camera").GetComponent<SpawnEnemy>().enabled = false;
            score = int.Parse(scoreText.text);
            Debug.Log(score);
            saveHighScore();	
            currentScore.text = ("Your Score : "+score.ToString());
            HighScore.text = ("Your High Score : "+PlayerPrefs.GetInt("High Score").ToString());
            //scoreText2.text = score.ToString();
            anotherChancePanel.SetActive(false);
            gameOverUI.SetActive(true);
            //isPaused = true;
            //myAnimator.GetComponent<Animator>().enabled = true;
            //Invoke("Restart", RestartDelay);
        }
    }

    public void Restart () {
        RestartPanel.SetActive(true);
    }
    public void onPressRestartOkButton()
    {
        RestartPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void onPressRestartCancelButton() {
        RestartPanel.SetActive(false);
    }

    public void stopEnemy() {
        if (enemy.Count == 0)
        {
            return;
        }
        else {
        foreach (GameObject OneEnemy in enemy)
            {
                if (OneEnemy != null) {
                    Rigidbody rb = OneEnemy.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                }
            }
        }
    }

    public void InstantiateGround() {
        NewGround = Instantiate(Ground) as GameObject;
        NewGround.transform.position = new Vector3(0f,0f,oldGround.transform.position.z+1000);
        
    }

    public void InstantiateBackGround() {
        NewBackGround = Instantiate(BackGround) as GameObject;
        NewBackGround.transform.position = new Vector3(oldBackGround.transform.position.x,oldBackGround.transform.position.y,oldBackGround.transform.position.z+2000);
        NewBackGround.transform.localScale += new Vector3(0f,0f,100f);
    }

    public void saveHighScore() {
    	Debug.Log("score : "+ score);
    	if(PlayerPrefs.GetInt("High Score") < score) {
    		PlayerPrefs.SetInt("High Score", score);
    		PlayerPrefs.Save();
    	}
    	userName = PlayerPrefs.GetString("Name");
    	highScore = PlayerPrefs.GetInt("High Score");
    	sm.AddNewHighScore(userName,highScore);

    }

    public void onPause() {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    	stopEnemy();
    }

    public void onResume() {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
    	pausePanel.SetActive(false);
    	isPaused = false;
    }

    public void onButtonPressMain() {
    	SceneManager.LoadScene("Welcome");
    }

    public void showSettingsPanel() {
    	settingPanel.SetActive(true);
    }

    public void hideSettingsPanel() {
    	settingPanel.SetActive(false);
    }

    

}


/*[System.Serializable]
class SaveData {
    public Vector3 leftButtonPosition;
    public Vector3 rightButtonPosition;
    public Vector3 jumpButtonPosition;
}
*/
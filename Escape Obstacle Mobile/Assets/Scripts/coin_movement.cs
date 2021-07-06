using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_movement : MonoBehaviour
{
    public float forward_force;
    public GameObject coinPrefab;
    public Rigidbody Player;
    private Vector3 screenBounds;
    private gamaManager gm;
    private int x = 0;
    private Vector3 ScaleChange;
    private GameObject a;
    public GameObject lastCoin;
    public SpawnEnemy sm;
    // Start is called before the first frame update
    void Start()
    {
    	gm = GameObject.FindWithTag("GameController").GetComponent<gamaManager>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Debug.Log(screenBounds);
        forward_force = gm.forwardForce;
        //StartCoroutine(CoinsGenerate());
        sm = GetComponent<SpawnEnemy>();
        sm.coin=a;
    }

    // Update is called once per frame
    void Update()
    {
        sm.coin = a;
    }

    public void generateCoin() {
    	//Debug.Log("Coins Generated : " + i);
    	//i++;
        x=0;
        //Destroy(lastCoin);
        /*a = Instantiate(coinPrefab) as GameObject;
        if (lastCoin == null)
        {
        //a.transform.position = new Vector3(Random.Range(-4.0f,4.0f), 1f, Random.Range(Player.position.z+110, Player.position.z+120));;
        a.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), 1f, sm.lastEnemy.transform.position.z + 10f); ;
        //a.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), 1f, Player.position.z + 120); ;

        }
        else
        {
            a.transform.position = new Vector3(lastCoin.transform.position.x, 1f, lastCoin.transform.position.z + 2f); ;
        }
        lastCoin = a;*/
        //ScaleChange = new Vector3(Random.Range(1f,4f),0f,0f);
        //gm.enemy.Add(a);
        //scoreText.text = enemyCount.ToString();
        while (x <= 5)
        {
            a = Instantiate(coinPrefab) as GameObject;
            if (lastCoin == null)
            {
                //a.transform.position = new Vector3(Random.Range(-4.0f,4.0f), 1f, Random.Range(Player.position.z+110, Player.position.z+120));;
                a.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), 1f, sm.lastEnemy.transform.position.z + 5f); ;

            }
            else
            {
                a.transform.position = new Vector3(lastCoin.transform.position.x, 1f, lastCoin.transform.position.z + 5f); ;
            }
            lastCoin = a;
            x++;
        }
        //a.transform.localScale += ScaleChange;
    }

    IEnumerator CoinsGenerate() {
    	while (true) {
	    	yield return new WaitForSeconds(1f);
            Destroy(lastCoin);
            if (!gm.gameHasEnded) {
                if (!gm.isPaused) {
                    Time.timeScale = 1f;
                    generateCoin();
                }
                else {
                    Time.timeScale = 0f;
                }
            }
            else {
                Time.timeScale = 0f;
            }

            /*if (!gm.isPaused && !gm.gameHasEnded) {
                generateCoin();
            }*/
            //yield return new WaitForSeconds(5f);
	    }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    public gamaManager gm;
    public GameObject EnemyPrefab;
    public Transform Player;
    public GameObject a;
    public GameObject lastEnemy;
    public float respawnTime = 2f;
    public Text scoreText;
    private int enemyCount = 0;
    private Vector3 screenBounds;
    public float forward_force;
    public Vector3 ScaleChange = new Vector3(0,0,0);
    public GameObject coin;
    public coin_movement cm;
    private int i;
    //public float forwardForce = 100f;
    // Start is called before the first frame update
    void Start()
    {
        i=0;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Debug.Log(screenBounds);
        //StartCoroutine(EnemyWave());
        spawnEnemy();
        //forward_force = gm.forwardForce;

    }
    void Update() {
        //a.GetComponent<Rigidbody>().AddForce(0,0,-25*Time.deltaTime, ForceMode.VelocityChange);
        // Debug.Log("Player z position : "+Player.position.z);
        // Debug.Log("last Enemy z position : "+lastEnemy.transform.position.z);
        // Debug.Log("last enemy - Player position : "+(lastEnemy.transform.position.z - Player.position.z));
        if (lastEnemy != null ) {
            if (Player.position.z  >= (lastEnemy.transform.position.z - 90f)) {
                if (!gm.gameHasEnded) {
                    spawnEnemy();
                    //i++;
                    if (i>=3) {
                        cm.generateCoin();
                        Destroy(cm.lastCoin);
                        i=0;
                    }

                }
            }
        }
        
    }

    public void spawnEnemy() {
        //enemyCount++;
        /*if (gm.forwardForce < 20) {
            gm.forwardForce+=0.1f;
        }
        else {
            gm.forwardForce+=0.05f;
        }*/
        i++;
        ScaleChange = new Vector3(Random.Range(1f,4f),0f,0f);
        a = Instantiate(EnemyPrefab) as GameObject;
        //lastEnemy = a;
        if (a != null) {
            gm.enemy.Add(a);
            //scoreText.text = enemyCount.ToString();
            a.transform.position = new Vector3(Random.Range(-4.0f,4.0f), 0.5f, Random.Range(Player.position.z+120, Player.position.z+130));
            /*if ((a.transform.position.x == (coin.transform.position.x+1f)) || (a.transform.position.x == (coin.transform.position.x-1f))) {
                print("Enemy at coin's position. Enemy position changed.");
                if (coin.transform.position.x > 0) {
                    a.transform.position = new Vector3 (Random.Range(-4.0f,0f), a.transform.position.y, a.transform.position.z);
                }
                else if (coin.transform.position.x <=0) {
                    a.transform.position = new Vector3 (Random.Range(0f,4f), a.transform.position.y, a.transform.position.z);
                }
            }*/
            a.transform.localScale += ScaleChange;
            lastEnemy = a;
        }
        // if (lastEnemy!=null) {
        //     lastEnemy = a;
        // }
        //Debug.Log("Forward Force : "+gm.forwardForce);
        //a.GetComponent<Rigidbody>().velocity = new Vector3(0,0,-forward_force*Time.deltaTime);
        //Debug.Log(respawnTime);

        //a.GetComponent<Rigidbody>().AddForce(0,0,-forwardForce*Time.deltaTime,ForceMode.VelocityChange);
        /* if(a.transform.position.z < Player.position.z-10) {
            Destroy(a);
        } */
        // Debug.Log(a.transform.position);
        // Debug.Log(screenBounds);   
        // DestroyObjectDelayed(a);
        //Debug.Log(a.transform.position);

    }
/* 
    public void DestroyObjectDelayed(GameObject a) {
        //Debug.Log("Enemy Destroyed");
        enemyCount+=1;
        scoreText.text = enemyCount.ToString();
        //Destroy(a,15);
        if (a.transform.position.z <= -1)
        {
            Destroy(a);
        }
        //gm.enemy.Remove(a);
    }
 */
    /*IEnumerator EnemyWave() {
        while(true) {
            // if (gm.forwardForce <= 15) { 
            //     respawnTime = Random.Range(1.0f,2f);
            // }
            // else if (gm.forwardForce > 15) {
            //     respawnTime = Random.Range(0.5f,1.0f);
            // }
            //Debug.Log("Respawn time : "+respawnTime);
            yield return new WaitForSeconds(respawnTime);
            if (gm.enemyCountOnScreen >3) {

                if (!gm.isPaused && !gm.gameHasEnded) {
                    spawnEnemy();
                }
                
            }
            else if (gm.enemyCountOnScreen <=3) {
                spawnEnemy();
            }
            // yield return new WaitForSeconds(respawnTime);
            // if (!gm.isPaused && !gm.gameHasEnded) {
            //     spawnEnemy();
            // }
                
        }
    }*/
}

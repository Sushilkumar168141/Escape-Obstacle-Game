using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Player;
	public float forward_force;
    public Vector3 currentVelocity;
    public gamaManager gm;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(increaseSpeedTimer());
        gm = GameObject.FindWithTag("GameController").GetComponent<gamaManager>();
        Player = GameObject.FindWithTag("Player");
        //forward_force = gm.getRandomSpeed();
        //Debug.Log("Random speed : "+forward_force);
        //forward_force = gm.forwardForce;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy.GetComponent<Rigidbody>().AddForce(0, 0,  -forward_force * Time.deltaTime, ForceMode.VelocityChange);

        //currentVelocity = Enemy.GetComponent<Rigidbody>().velocity;
        if (gameObject.transform.position.z <= Player.transform.position.z-5)  {
            Destroy(gameObject);
            gm.forwardForce += 0.1f;
            gm.enemyCount++;
        }
        /*if (gm.isPaused) {
            //Enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }*/
        //Enemy.GetComponent<Rigidbody>().velocity = currentVelocity;
    }

}

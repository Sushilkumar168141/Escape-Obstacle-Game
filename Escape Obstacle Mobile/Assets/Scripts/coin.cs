using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
	public GameObject coinInstance;
	public gamaManager gm;
    public GameObject Player;
	public float forward_force;
    private Vector3 currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<gamaManager>();
        Player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
    	//forward_force = gm.forwardForce;
    	//coinInstance.GetComponent<Rigidbody>().AddForce(0,0,-forward_force * Time.deltaTime, ForceMode.VelocityChange);
        //currentVelocity = coinInstance.GetComponent<Rigidbody>().velocity;
        if (gameObject.transform.position.z <= Player.transform.position.z-9) {
            if (gameObject != null) {
                Destroy(gameObject);
            }
        }
        /*if (gm.isPaused) {
            coinInstance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }*/
        //coinInstance.GetComponent<Rigidbody>().velocity = currentVelocity;    
    }
}

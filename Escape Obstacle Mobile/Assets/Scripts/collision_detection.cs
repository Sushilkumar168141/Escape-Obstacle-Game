using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class collision_detection : MonoBehaviour {
	public player_movement movement;
	public  Transform Player;
	public Material collisionMaterial;
	public GameObject anotherChancePanel;
	public Timer timer;
	public bool vibrate = true;
	/*public Image timerImage;
	private float timeAmt = 5f;
	private float time;*/
	void Start() {
		anotherChancePanel.SetActive(false);
		if (PlayerPrefs.GetInt("Vibration") == 0) {
			vibrate = false;
		}
		else if (PlayerPrefs.GetInt("Vibration") == 1) {
			vibrate = true;
		}

		//timer = GameObject.FindWithTag("anotherChance").GetComponent<Timer>();
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		//Debug.Log("we hit something");
		//Debug.Log(collisionInfo.collider.name);
		if (collisionInfo.collider.tag == "Enemy") 
		{
			if (!movement.shieldActivated)
			{
				Destroy(collisionInfo.collider.gameObject, 1f);
				Debug.Log("we hit an obstacle");
				if (vibrate)
				{
					Handheld.Vibrate();
				}
				movement.enabled = false;
				Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
				collisionInfo.collider.gameObject.GetComponent<Renderer>().material = collisionMaterial;
				anotherChancePanel.SetActive(true);
				timer.startTimer();
				//Time.timeScale = 0f;
				FindObjectOfType<gamaManager>().gameTime += 0;
				//FindObjectOfType<gamaManager>().gameEnd();
			}
            else if (movement.shieldActivated)
            {
                Destroy(collisionInfo.collider.gameObject);
            }

        }

	}

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
			if (movement.shieldActivated)
            {
				Destroy(other.gameObject);
            }
        }
    }*/

    private void Update() {
		if (Player.position.y < 0) {
			Debug.Log("Fallen From Edge");
			movement.enabled = false;
			anotherChancePanel.SetActive(true);
			timer.startTimer();
			Player.position = new Vector3(0f, 0.5f, Player.position.z - 20f);
			Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
			FindObjectOfType<gamaManager>().gameTime+=0;
			//FindObjectOfType<gamaManager>().gameEnd();
		}	

		if (PlayerPrefs.GetInt("Vibration") == 0) {
			vibrate = false;
		}
		else if (PlayerPrefs.GetInt("Vibration") == 1) {
			vibrate = true;
		}
	}
}
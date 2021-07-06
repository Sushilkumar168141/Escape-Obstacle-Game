using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class player_movement : MonoBehaviour {
	public Rigidbody rb;
	public Transform target;
	public float forward_force = 40f;
	public float sideways_force = 40f;
	private  float horizontalMove = 0f;
	private float verticalMove = 0f;
	public float jumpForce = 25.0f;
	public Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
	private bool isGrounded = false;
	private bool isJumping = false;
	private bool space = false;
	private bool upArrow = false;
	private bool jumpAllowed = false;
	public Vector3 currentVelocity;
	public gamaManager gm;
	private Touch touch;
	private Vector3 touchPosition;
	private Vector3 startTouchPosition;
	private Vector3 endTouchPosition;
	private float minSwipeDistance = 5f;
	private float swipeDistance;
	public GameObject jumpButton;
	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject Ground;
	public GameObject BackGround;
	//public GameObject NewGround;
	public bool left = false;
	public bool right = false;
	public bool down = false;

	public float lerpTime = 1f;
	public float currentLerpTime = 0f;

	public GameObject powerupTimerPanel;
	public GameObject magnetTimerPanel;

	public GameObject shieldTimerPanel;

	public bool magnetEnabled = false;
	public Image magnetTimerImage;
	public float magnetTimer;
	public float magnetTotalTimer = 20f;
	
	//public float magnetTimer;

	public magnetInstantiation mg;

	public bool shieldActivated = false;
	public float shieldTimer;
	public Image shieldTimerImage;
	public float shieldTotalTimer = 20f;
	//public FixedJoystick joystick;
	//private float v;
	// Use this for initialization
	void Start () 
	{
		shieldTimer = shieldTotalTimer;
		magnetTimer = magnetTotalTimer;
		magnetEnabled = false;
		shieldActivated = false;
		//magnetTimer = 10f;
		//h = GetAxis("Horizontal");
		//v = GetAxisRow("Vertical");
		Debug.Log("Left Button Position : "+leftButton.transform.position);
        Debug.Log("right Button Position : "+rightButton.transform.position);
        Debug.Log("Jump Button Position : "+jumpButton.transform.position);
		LoadGame();
		Debug.Log("Left Button Position : "+leftButton.transform.position);
        Debug.Log("right Button Position : "+rightButton.transform.position);
        Debug.Log("Jump Button Position : "+jumpButton.transform.position);
		gm = GameObject.FindWithTag("GameController").GetComponent<gamaManager>();


		// Option to set sideways Force
		/*if (!PlayerPrefs.HasKey("Sideways Force")) {
            PlayerPrefs.SetInt("Sideways Force",30);
        }
        else {
        	sideways_force = PlayerPrefs.GetInt("Sideways Force");
        }*/
		//forward_force = gm.forwardForce;
	}

	// Update is called once per frame
	void Update()
	{
		gm.gameTime += Time.deltaTime;

		// Set sideways force of player
		sideways_force = gm.forwardForce * 1.6f;


		float step = sideways_force * Time.deltaTime;
		print("Horizontal Move : " + horizontalMove);
		forward_force = gm.forwardForce;
		rb.AddForce(0f, 0f, forward_force * Time.deltaTime, ForceMode.VelocityChange);
		//horizontalMove = joystick.Horizontal;
		//verticalMove = joystick.Vertical;
		currentVelocity = rb.velocity;
		if (gm.isPaused)
		{
			rb.velocity = Vector3.zero;
			//rb.transform.position = currenPosition;
			rb.useGravity = false;
			return;
		}
		rb.velocity = currentVelocity;
		rb.useGravity = true;

		Ground = gm.NewGround;
		if (rb.position.z > Ground.transform.position.z)
		{
			gm.InstantiateGround();
			//gm.InstantiateBackGround();

		}

		BackGround = gm.NewBackGround;
		if (rb.position.z > BackGround.transform.position.z)
		{
			gm.InstantiateBackGround();
			//gm.InstantiateBackGround();

		}

		if (gm.gameHasEnded)
		{
			rb.velocity = Vector3.zero;
		}

        if (magnetEnabled)
        {
			magnetTimerPanel.SetActive(true);
			if (magnetTimer <= 0f)
			{
				magnetTimer = 20f;
				Destroy(mg.newMagnet.gameObject);
				magnetEnabled = false;
			}
			else
			{
				//mg.newMagnet.transform.position = rb.position;
				magnetTimer -= Time.deltaTime;
				magnetTimerImage.fillAmount = (magnetTimer / magnetTotalTimer);
			}
			//mg.newMagnet.transform.position = rb.transform.position;
		}
		if (!magnetEnabled)
		{
			magnetTimerPanel.SetActive(false);
		}

		if (shieldActivated)
		{
			shieldTimerPanel.SetActive(true);
			if (shieldTimer >= 0f)
			{
				shieldTimer -= Time.deltaTime;
				shieldTimerImage.fillAmount = (shieldTimer / shieldTotalTimer);
			}
			else
			{
				shieldTimer = 20f;
				shieldActivated = false;
			}
		}
		if (!shieldActivated)
		{
			shieldTimerPanel.SetActive(false);
		}

		if (!magnetEnabled && !shieldActivated)
        {
			powerupTimerPanel.SetActive(false);
        }

		if (gm.gameHasEnded)
        {
			powerupTimerPanel.SetActive(false);
			magnetTimerPanel.SetActive(false);
			shieldTimerPanel.SetActive(false);
        }
	}
		//
		/*if (rb.velocity.y < 0) {
			Physics.gravity = new Vector3(0.0f, -19.6f, 0.0f);
		}
		else {
			Physics.gravity = new Vector3(0.0f, -9.8f, 0.0f);	
		}*/

		//horizontalMove = Input.GetAxisRaw("Horizontal") * sideways_force;
		// space = Input.GetKey(KeyCode.Space);
		// upArrow = Input.GetKey(KeyCode.UpArrow);
		// if ((space || upArrow) && isGrounded) {
		// 	jumpAllowed = true;
		// }
		// else {
		// 	jumpAllowed = false;
		// }
		
		// Controls for mobile devices
		/*if (Input.touchCount>0) {
			touch = Input.GetTouch(0);
			//touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10.0f));
			//Debug.Log(touchPosition);
			if (touch.phase == TouchPhase.Began) {
				startTouchPosition = touch.position;
				print("Initial position : "+startTouchPosition);
				
			}
			if (touch.phase == TouchPhase.Moved) {
				//rb.position += new Vector3((touch.position.x - startTouchPosition.x)/180,0f,0f);
				print("Moving");
			}
			if (touch.phase == TouchPhase.Ended) {
				currentLerpTime+=Time.deltaTime;
				endTouchPosition = touch.position;
				print("Final Position : "+endTouchPosition);
				swipeDistance = (endTouchPosition.x - startTouchPosition.x)/25;
				print("Swipe Distance : "+swipeDistance);
				target.transform.position += new Vector3(swipeDistance,0f,0f);
				if(swipeDistance < 0f) {
					horizontalMove = -1f;
				}
				else if (swipeDistance >0f) {
					horizontalMove = 1f;
				}
				//rb.AddForce(horizontalMove*sideways_force*Time.deltaTime,0,0,ForceMode.VelocityChange);
				if (currentLerpTime >= lerpTime) {
					currentLerpTime = lerpTime;
				}

				
				
				
				rb.transform.position += new Vector3(swipeDistance,0f,0f);
				//rb.transform.Translate(new Vector3(swipeDistance*sideways_force*Time.deltaTime,0f,0f));
			}


			

			/*if (touchPosition.x > 0) {
				horizontalMove = 1f;
			}
			else if (touchPosition.x <0){
				horizontalMove = -1f;
			}*/
		//}
		/*float perc = currentLerpTime/lerpTime;
		if (rb.transform.position.x != target.transform.position.x) {
			rb.transform.position = Vector3.Lerp(rb.transform.position,target.transform.position, perc);
		}*/

		/*if (rb.position.x != target.transform.position.x) {
			rb.AddForce(horizontalMove*sideways_force*Time.deltaTime,0,0,ForceMode.VelocityChange);
		}
		horizontalMove=0f;*/
		/*else {
			horizontalMove = 0f;
		}*/


		// Check for swiping action
		/*if (Input.touchCount>0 && touch.phase == TouchPhase.Began) {
			startTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10.0f));
		}
		if (Input.touchCount > 0 && touch.phase == TouchPhase.Ended) {
			endTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10.0f));
			swipeDistance = (endTouchPosition.y - startTouchPosition.y);
			if ((swipeDistance > minSwipeDistance) && isGrounded) {
				print("swiped up");
				jumpAllowed = true;
				
			}
		}
		else {
			jumpAllowed = false;
			startTouchPosition = Vector3.zero;
			endTouchPosition = Vector3.zero;
			swipeDistance = 0f;
		}
*/


		//Debug.Log(currentVelocity);
		//Debug.Log(isGrounded.ToString());

	private void OnCollisionStay(Collision collider) {
		if(collider.gameObject.tag == "Ground") {
			isGrounded = true;
			//isJumping = false;
			//down = true;
			//jumpButton.SetActive(true);
		}
	}

	/*public void collectCoins(Collider other)
    {
		//Destroy(other.gameObject);
		gm.coinCount++;
	}*/
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("coin")) {
			if (!magnetEnabled)
			{
				//collectCoins(other);
				Destroy(other.gameObject);
				gm.coinCount++;
			}
		}

		/*if(magnetEnabled)
        {
			Destroy(other.gameObject);
			gm.coinCount++;
		}*/
		
		if (other.gameObject.tag == "magnet")
        {
			if (other.GetType() == typeof(BoxCollider))
			{
				powerupTimerPanel.SetActive(true);
				//mg.newMagnet.gameObject.GetComponent<Renderer>().enabled = false;
				Destroy(other.gameObject);
				magnetEnabled = true;
			}
			//Destroy(other.gameObject);
        }

		if (other.gameObject.CompareTag("shield"))
        {
			powerupTimerPanel.SetActive(true);
			shieldActivated = true;
			Destroy(other.gameObject);
        }
 	}

	private void FixedUpdate() {
		/*rb.AddForce(horizontalMove*sideways_force*Time.deltaTime,0,0,ForceMode.VelocityChange);
		if (jumpAllowed) {
			jumpPlayer();
		}*/

		// Arrow buttons
		if (left) {
			rb.AddForce(-1*sideways_force*Time.deltaTime,0,0,ForceMode.VelocityChange);	
		}
		if (right) {
			rb.AddForce(1*sideways_force*Time.deltaTime,0,0,ForceMode.VelocityChange);
		}
		if (isJumping) {
			if(isGrounded) {
				rb.AddForce(jump * jumpForce, ForceMode.Impulse);
				isJumping = true;
				isGrounded = false;
				down = false;
			}
			
		}
		print("Is Grounded : "+isGrounded);
		print("Is jumping : "+isJumping);
		isJumping = false;
		isGrounded = false;
		//down = false;

		if (down) {
			//if (rb.transform.position.y>0.5) {
			if (!isGrounded) {
				print("Going down");
				rb.AddForce(-jump * jumpForce, ForceMode.Impulse);
				down = false;
			}
			else if (isGrounded) {
				print("Not jumping");
			}
			//down = false;
		}

		// Joystick Input 
		//rb.AddForce(horizontalMove*sideways_force*Time.deltaTime,0,0,ForceMode.VelocityChange);
		/*if(isGrounded && verticalMove > 0) {
			rb.AddForce(verticalMove*jump*jumpForce*Time.deltaTime, ForceMode.Impulse);
			isGrounded = false;
			//isJumping = true;
			//down = false;
		}
		if (!isGrounded && verticalMove < 0) {
			rb.AddForce(verticalMove*jump*jumpForce*Time.deltaTime, ForceMode.Impulse);
		}*/

		//isJumping = false;
		//isGrounded = false;
	}	

	public void moveLeft() {
		//horizontalMove = -1f
		left = true;
	}

	public void moveRight() {
		right = true;
	}

	public void jumpPlayer() {
		isJumping = true;
		//jumpButton.SetActive(false);
	}

	public void moveDown() {
		down = true;	
	}

	public void stopMoveLeft() {
		left = false;
	}

	public void stopMoveRight() {
		right = false;
	}

	/*private void jumpPlayer() {
		if (jumpAllowed) {
			rb.AddForce(jump * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}
	}*/

	public void LoadGame() {
        if (File.Exists(Application.persistentDataPath + "/ButtonsPosition.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ButtonsPosition.dat", FileMode.Open);
            SaveData data = (SaveData)(bf.Deserialize(file));
            leftButton.transform.position = new Vector3(data.leftButtonPosition[0],data.leftButtonPosition[1],data.leftButtonPosition[2]);
            rightButton.transform.position = new Vector3(data.rightButtonPosition[0],data.rightButtonPosition[1],data.rightButtonPosition[2]);
            jumpButton.transform.position = new Vector3(data.jumpButtonPosition[0],data.jumpButtonPosition[1],data.jumpButtonPosition[2]);
            Debug.Log("Game Data Loaded ");
            file.Close();
        }
        else {
        	Debug.Log("There is no saved data! ");
        }
    }
}

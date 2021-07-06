using UnityEngine;
using System.Collections;

public class follow_player : MonoBehaviour {
	public Transform Player;
	public Vector3 offset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = Player.position + offset;
		transform.LookAt(Player);
	}
}

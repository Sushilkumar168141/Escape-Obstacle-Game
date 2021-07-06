using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObstacleColor : MonoBehaviour
{
	public Material goldenObstacle;
	public Material normalObstacle;

    public List<Material> materialsList = new List<Material>();

    public int EquippeditemIndex;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Obstacle Equipped"))
        {
            PlayerPrefs.SetInt("Obstacle Equipped", 8);
        }
        /*if(PlayerPrefs.GetInt("Already topped") == 1) {
        	ChangeObstacleToGolden();
        }*/
        //else if (PlayerPrefs.GetInt("Already topped") == 0) {
            EquippeditemIndex = PlayerPrefs.GetInt("Obstacle Equipped");
            changeMaterialToChoice(EquippeditemIndex);
            //ChangeObstacleToNormal();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void ChangeObstacleToGolden() {
    	gameObject.GetComponent<Renderer>().material = goldenObstacle;
    }

    public void ChangeObstacleToNormal() {
    	gameObject.GetComponent<Renderer>().material = normalObstacle;
    }

    public void changeMaterialToChoice(int index)
    {
        gameObject.GetComponent<Renderer>().material = materialsList[index];
    }

}

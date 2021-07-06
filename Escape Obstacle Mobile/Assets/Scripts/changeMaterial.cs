using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMaterial : MonoBehaviour
{
	public Material goldenPlayer;
	public Material redPlayer;

    public List<Material> materialsList = new List<Material>();
    //public int len;

    public int EquippedItemIndex;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Ball Equipped"))
        {
            PlayerPrefs.SetInt("Ball Equipped", 0);
        }
        /*if(PlayerPrefs.GetInt("isTop") == 1) {
        	ChangeMaterialToGolden();
        }*/
        //else if (PlayerPrefs.GetInt("isTop") == 0) {
        EquippedItemIndex = PlayerPrefs.GetInt("Ball Equipped");
        //ChangeMaterialToRed();
        changeMaterialToChoice(EquippedItemIndex);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void ChangeMaterialToGolden() {
    	gameObject.GetComponent<Renderer>().material = goldenPlayer;
    }

    public void ChangeMaterialToRed() {
    	gameObject.GetComponent<Renderer>().material = redPlayer;
    }

    public void changeMaterialToChoice(int index)
    {
        gameObject.GetComponent<Renderer>().material = materialsList[index];
    }

}

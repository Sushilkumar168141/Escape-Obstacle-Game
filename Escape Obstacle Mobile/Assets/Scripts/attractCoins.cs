using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attractCoins : MonoBehaviour
{
    GameObject coins;
    GameObject player;
    player_movement pm;
    [SerializeField]
    gamaManager gm;
    float magnetTimer;
    float magnetTotalTimer = 20f;
    public Image magnetTimerImage;
    /*public GameObject powerupTimerPanel;
    public GameObject magnetTimerPanel;*/
    // Start is called before the first frame update
    void Start()
    {
        magnetTimer = magnetTotalTimer;
        gm = GameObject.FindWithTag("GameController").GetComponent<gamaManager>();
        pm = GameObject.FindWithTag("Player").GetComponent<player_movement>();
        magnetTimerImage = pm.magnetTimerImage;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (pm.magnetEnabled)
        {
            if (magnetTimer <= 0f)
            {
                magnetTimer = 10f;
                Destroy(gameObject);
                pm.magnetEnabled = false;
            }
            else
            {
                //mg.newMagnet.transform.position = rb.position;
                magnetTimer -= Time.deltaTime;
                magnetTimerImage.fillAmount = (magnetTimer / magnetTotalTimer);
            }
        }*/
    }


    /*private void OnTriggerStay(Collider other)
    {
        if (magnetTimer >= 0f)
        {
            if (other.gameObject.CompareTag("coin"))
            {
                gm.coinCount++;
                other.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, pm.rb.gameObject.transform.position, 5f * Time.deltaTime);
                Destroy(other.gameObject);
            }
        }
    }*/
}

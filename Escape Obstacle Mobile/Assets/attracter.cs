using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attracter : MonoBehaviour
{
    float magnetTimer;
    float magnetTotalTimer = 20f;
    public Image magnetTimerImage;

    public Rigidbody player;
    public player_movement pm;
    public magnetInstantiation mg;
    public gamaManager gm;
    // Start is called before the first frame update
    void Start()
    {
        /*magnetTimer = magnetTotalTimer;
        magnetTimerImage = pm.magnetTimerImage;*/
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position;
        /*if (pm.magnetEnabled)
        {
            if (magnetTimer <= 0f)
            {
                magnetTimer = 20f;
                Destroy(mg.newMagnet.gameObject);
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

    private void OnTriggerStay(Collider other)
    {
        if (pm.magnetEnabled)
        {
            if (other.gameObject.CompareTag("coin"))
            {
                gm.coinCount++;
                Vector3.MoveTowards(other.gameObject.transform.position, player.gameObject.transform.position, 5f * Time.deltaTime);
                Destroy(other.gameObject);
            }
        }
    }
}

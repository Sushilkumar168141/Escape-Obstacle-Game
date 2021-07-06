using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetInstantiation : MonoBehaviour
{
    [SerializeField]
    private GameObject magnet;
    public GameObject newMagnet;
    private gamaManager gm;
    [SerializeField]
    private Rigidbody player;
    private Vector3 RandomPosition;
    public float magnetInstantiationTimer = 10f;
    private bool magnetInstantiated = false;
    public player_movement pm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<gamaManager>();
        RandomPosition = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(player.transform.position.z + 100f, player.transform.position.z + 200f));
        //newMagnet = Instantiate(magnet, RandomPosition, Quaternion.identity);
        magnetInstantiationTimer = Random.Range(10f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (magnetInstantiationTimer <= 0f)
        {
            if (newMagnet == null && !pm.shieldActivated) ;
            {
                RandomPosition = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(player.transform.position.z + 100f, player.transform.position.z + 200f));
                newMagnet = Instantiate(magnet, RandomPosition, Quaternion.identity);
                //magnetInstantiationTimer = 60f;
                magnetInstantiationTimer = Random.Range(30f, 120f);

            }
        }
        else
        {
            magnetInstantiationTimer -= Time.deltaTime;
        }

        if (newMagnet.transform.position.z < player.transform.position.z - 50f)
        {
            Destroy(newMagnet.gameObject);

        }

    }

    
}

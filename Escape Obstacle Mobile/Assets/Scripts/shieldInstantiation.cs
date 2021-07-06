using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldInstantiation : MonoBehaviour
{
    [SerializeField]
    private GameObject shield;
    public GameObject newshield;
    private gamaManager gm;
    [SerializeField]
    private Rigidbody player;
    private Vector3 RandomPosition;
    public float shieldInstantiationTime = 10f;
    private bool shieldInstantiated = false;
    public player_movement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<gamaManager>();
        //RandomPosition = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(player.transform.position.z + 100f, player.transform.position.z + 200f));
        //newshield = Instantiate(shield, RandomPosition, Quaternion.identity);
        shieldInstantiationTime = Random.Range(10f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldInstantiationTime <= 0f )
        {
            if (newshield == null && !playerMovement.shieldActivated);
            {
                RandomPosition = new Vector3(Random.Range(-4f, 4f), 0.5f, Random.Range(player.transform.position.z + 100f, player.transform.position.z + 200f));
                newshield = Instantiate(shield, RandomPosition, Quaternion.identity);
                //shieldInstantiationTime = 60f;
                shieldInstantiationTime = Random.Range(30f, 120f);
            }
        }
        else
        {   
            shieldInstantiationTime -= Time.deltaTime;
        }

        if (newshield.transform.position.z < player.transform.position.z - 50f)
        {
            Destroy(newshield.gameObject);

        }

    }
}

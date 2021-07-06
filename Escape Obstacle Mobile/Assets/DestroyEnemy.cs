using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public Rigidbody player;
    public player_movement pm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pm.shieldActivated)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                //other.transform.position = Vector3.MoveTowards(other.gameObject.transform.position, player.gameObject.transform.position, 5f * Time.deltaTime);
                Destroy(other.gameObject);
            }
        }
    }
}

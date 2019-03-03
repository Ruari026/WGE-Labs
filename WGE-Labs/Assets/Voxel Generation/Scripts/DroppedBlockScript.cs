using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedBlockScript : MonoBehaviour
{
    public int blockType;

    private Rigidbody theRB;
    public float floatForce;
    /*
    ==================================================
    initializing block
    ==================================================
    */
    void Start ()
    {
        theRB = GetComponent<Rigidbody>();
    }

    public void SetBlockType(int newBlockType)
    {
        this.blockType = newBlockType;
    }

    // Update is called once per frame
    void Update ()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(this.transform.position, 2);
        for (int i = 0; i < nearbyColliders.Length; i++)
        {
            if (nearbyColliders[i].tag == "Player")
            {
                GameObject player = nearbyColliders[i].gameObject;
                float distance = Vector3.Distance(this.transform.position, player.transform.position);

                if (distance <= 1)
                {
                    //Checking If The Block Is Close Enough To The Player To Be Added To The Player's Inventory
                    Destroy(this.gameObject);
                }
                else if (distance <= 2)
                {
                    //Block Floats To The Player
                    Vector3 floatDirection = (player.transform.position - this.transform.position).normalized;
                    theRB.AddForce(floatDirection * floatForce);
                }
            }
        }
	}
}

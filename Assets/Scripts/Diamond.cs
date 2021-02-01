using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            other.GetComponent<Player>().DiamondCollision();

            GameObject.Destroy(this.gameObject);
        }
    }
}

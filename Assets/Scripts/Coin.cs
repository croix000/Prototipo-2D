﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {

            other.GetComponent<Player>().CrystalCollision();

            GameObject.Destroy(this.gameObject);
        }
    }
}
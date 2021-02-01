using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgChanger : MonoBehaviour
{
    void Start()
    {
        if (this.GetComponent<Camera>() != null)
            this.GetComponent<Camera>().backgroundColor = GameObject.FindObjectOfType<DungeonManager>().GetBGColor();
        if (this.GetComponent<Image>() != null)
            this.GetComponent<Image>().color = GameObject.FindObjectOfType<DungeonManager>().GetBGColor();
        if (this.GetComponent<SpriteRenderer>() != null)
            this.GetComponent<SpriteRenderer>().color = GameObject.FindObjectOfType<DungeonManager>().GetBGColor();
    }
}

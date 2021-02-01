using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLock : MonoBehaviour
{
    [SerializeField] Color activeColor;
    [SerializeField] Color disabledColor;
    [SerializeField] SpriteRenderer[] crystals;

    [SerializeField] Player player;

    int crystalsNeeded = 3;
    int currentCrystal;
    [SerializeField] GameObject PortalUnlocked;
    // Start is called before the first frame update
    void Start()
    {
       
        player = FindObjectOfType<Player>();
        player.onCrystalCollected += ReduceCrystalsNeeded;
        foreach (SpriteRenderer renderer in crystals) {
            renderer.color = disabledColor;
        }
    }


    void ReduceCrystalsNeeded()
    {
        crystals[crystalsNeeded-1].color = activeColor;
        crystalsNeeded--;
        if (crystalsNeeded <= 0)
            PortalUnlocked.SetActive(true);

    }

}

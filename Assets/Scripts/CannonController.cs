using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CannonType { U,D,L,R,UD,UL,UR,DL,DR,LR,ULR,UDR,UDL,DLR,UDLR }




public class CannonController : MonoBehaviour
{
    [SerializeField] CannonType cannonType;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int turnsToShoot;
    [SerializeField] Color attackColor;
    [SerializeField] Color idleColor;
    int turnCounter;
    Vector3[] spawnCoordinates;
    Direction[] spawnDirections;
    TurnManager turnManager;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {

        turnManager = GameObject.FindObjectOfType<TurnManager>();
        turnManager.onEnemiesTurn += ExecuteTurn;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnCoordinates = DirectionUtils.GetBulletCoordinates(cannonType);
        spawnDirections = DirectionUtils.GetBulletDirection(cannonType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExecuteTurn() {
        turnCounter++;
        if (turnCounter + 1 == turnsToShoot)
            HandleWindup();
        if (turnCounter == turnsToShoot)
            Shoot();
        
    }

    void HandleWindup() {
        spriteRenderer.color = attackColor;

    }

    void Shoot() {
        turnCounter = 0;
        spriteRenderer.color = idleColor;

        for (int i = 0; i < spawnCoordinates.Length; i++)
        {
            GameObject go = BulletObjectPool.SharedInstance.GetPooledObject();
            if(go != null)
            {
                go.transform.position = transform.position + spawnCoordinates[i];
                go.GetComponent<BulletController>().setDirection(spawnDirections[i]);
                DirectionUtils.RotateBullet(spawnDirections[i], go);
                go.SetActive(true);
            }
        }
    }


}

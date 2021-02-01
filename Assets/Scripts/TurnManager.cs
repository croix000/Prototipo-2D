using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    [SerializeField] Player player;


    public delegate void BulletsTurn();
    public event BulletsTurn onBulletsTurn;


    public delegate void EnemiesTurn();
    public event EnemiesTurn onEnemiesTurn;

    void Start()
    {

        player.onTurnExecuted += TurnLogic;
    }

    void TurnLogic() {

        //Handle Projectiles first
        //onBulletsTurn();
        //Handle Enemies then 
        onEnemiesTurn();

    }
}

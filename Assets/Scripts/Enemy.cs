using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EnemyState { Idle, Attack}

public class Enemy : MonoBehaviour
{
    Transform target;
    [SerializeField] float distanceThreshold=7;
    EnemyState currentState;
    TurnManager turnManager;

    HoldingPointAI holdingPoint;
    EnemyTimer enemyTimer;
    [SerializeField] int health;
    [SerializeField] GameObject sprite;
    [SerializeField] GameObject holdingPointGO;
    [SerializeField] GameObject mapSprite;
    [SerializeField] GameObject timer;
    [SerializeField] GameObject deadSprite;

    EnemyState GetCurrentState() {

        return currentState;
    }
  
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        holdingPoint = GetComponentInChildren<HoldingPointAI>();
        enemyTimer = GetComponentInChildren<EnemyTimer>();
        turnManager = GameObject.FindObjectOfType<TurnManager>();
        turnManager.onEnemiesTurn += ExecuteTurn;
    }

    void ExecuteTurn()
    {
        if (health > 0)
        {
            CalculateDistance();
            if (currentState == EnemyState.Attack)
            {
                holdingPoint.HandleAiming();
                enemyTimer.AdvanceFrame();
            }

        }
    }


    void CalculateDistance() {

        if (Vector3.Distance(this.transform.position, target.position) < distanceThreshold)
        {
            currentState = EnemyState.Attack;
        }
        else
        {

            currentState = EnemyState.Idle;
        }
    }

    public void Damage() {

        health--;
        if (health <= 0) {
            sprite.SetActive(false);
            holdingPointGO.SetActive(false);
            mapSprite.SetActive(false);
            timer.SetActive(false);
            deadSprite.SetActive(true);
        }
    }
}

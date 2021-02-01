using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { Enemy, Player }

public class BulletController : MonoBehaviour
{
    [SerializeField] BulletType bulletType;

    Vector3 movePoint;
    TurnManager turnManager;
    Direction direction;
    Mover mover;
    int lifeTime=20;
    int turnsCounter;
    // Start is called before the first frame update
    void Start()
    {

        mover = GetComponent<Mover>();
        turnManager = GameObject.FindObjectOfType<TurnManager>();
        turnManager.onEnemiesTurn += ExecuteTurn;
    }

    void ExecuteTurn()
    {
       
            mover.MoveTo(transform.position + DirectionUtils.GetVector3(direction));
            turnsCounter++;
            if (turnsCounter >= lifeTime)
            {
                turnsCounter = 0;
                this.gameObject.SetActive(false);
            }
        
       
    }

    public void setDirection(Direction dir) {


        this.direction = dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {



        turnsCounter = 0;
        if (other.tag == "Player" && bulletType.Equals(BulletType.Enemy) && other.gameObject.transform.position.x == transform.position.x && other.gameObject.transform.position.y == transform.position.y)
        {
            other.GetComponent<Player>().Damage();
        }

        if (other.tag == "Enemy" && bulletType.Equals(BulletType.Player) && other.gameObject.transform.position.x == transform.position.x && other.gameObject.transform.position.y == transform.position.y)
        {
            other.GetComponent<Enemy>().Damage();
        }

        this.gameObject.SetActive(false);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        turnsCounter = 0;
        this.gameObject.SetActive(false);
    }
}

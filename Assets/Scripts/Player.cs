using UnityEngine;

public class Player : MonoBehaviour
{

    Vector3 movePoint;
    Transform sprite;
    LayerMask obstacleMask;
    bool isMoving;

    float horizontal = 0f;
    float vertical = 0f;
    Vector3 lastDirection;

    Mover mover;

    public delegate void CrystalCollected();
    public event CrystalCollected onCrystalCollected;
    public delegate void DiamondCollected();
    public event DiamondCollected onDiamondCollected;
    public delegate void ShootExecuted();
    public event ShootExecuted onShootExecuted;


    public delegate void TurnExecuted();
    public event TurnExecuted onTurnExecuted;

    public delegate void DamageReceived();
    public event DamageReceived onDamageReceived;

    public delegate void PlayerDeath();
    public event PlayerDeath onPlayerDeath;
    int health = 2;

    public int GetHealth(){
        return health;
    }
    void Start()
    {

        sprite = GetComponentInChildren<SpriteRenderer>().transform;
        obstacleMask = LayerMask.GetMask("Wall");
        movePoint= transform.position;
        mover = GetComponent<Mover>();
        health = PlayerPrefs.GetInt("Health", 2);
    }


    private void Update()
    {
      

        horizontal = Mathf.Clamp(Input.GetAxisRaw("Horizontal") , -1, 1);
        vertical = Mathf.Clamp(Input.GetAxisRaw("Vertical") , -1, 1);

        
        if (Vector3.Distance(transform.position, movePoint) <= 0.05f && !isMoving)
        {
            Vector3 hitSize = Vector3.one * 0.5f;
            if (Mathf.Abs(horizontal) == 1)
            {

                if (!Physics2D.OverlapBox(movePoint + new Vector3(horizontal, 0f, 0f), hitSize, 0, obstacleMask))
                {
                    isMoving = true;
                    movePoint += new Vector3(horizontal, 0f, 0f);
                   // sprite.localScale = new Vector2(flipX * horizontal, sprite.localScale.y);
                    SetLastDirection(new Vector3(horizontal, 0f, 0f));
                    mover.MoveTo(movePoint);
                    EndTurn();
                }
            }
            else if (Mathf.Abs(vertical) == 1)
            {
                if (!Physics2D.OverlapBox(movePoint + new Vector3(0f, vertical, 0f), hitSize, 0, obstacleMask))
                {
                    isMoving = true;
                    movePoint += new Vector3(0f, vertical, 0f);
                    SetLastDirection(new Vector3(0f, vertical, 0f));
                    mover.MoveTo(movePoint);
                    EndTurn();
                }
            }


        }
        if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) == 0)
        {
            isMoving = false;

        }
        //}
    }

    public void SetLastDirection(Vector3 dir)
    {
        this.lastDirection = dir;

    }
    public Vector3 GetLastDirection()
    {
        return lastDirection;
    }


    public void CrystalCollision() {

        onCrystalCollected();
    }

    public void DiamondCollision()
    {

        onDiamondCollected();
    }
    public void EndTurn()
    {

        onTurnExecuted();
    }

    public void Shot()
    {
        EndTurn();
        onShootExecuted();
    }

    public void Damage() {
        health--;
        if (health == 0)
            onPlayerDeath();
        onDamageReceived();
    }
}


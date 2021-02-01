using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BatController : MonoBehaviour
{

    [SerializeField] MoveType moveType;
    Mover mover;
    public int direction = 1;
    Transform sprite;
    [SerializeField] Transform movePoint;
    LayerMask obstacleMask;
    float flipX;

    TurnManager turnManager;
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().transform;
        mover = GetComponent<Mover>();
        obstacleMask = LayerMask.GetMask("Wall");
        flipX = sprite.localScale.x;
        movePoint.parent = null;
        turnManager = GameObject.FindObjectOfType<TurnManager>();
        turnManager.onEnemiesTurn += CalculateCollision;


    }



    void CalculateCollision() {
       
      
            Vector3 hitSize = Vector3.one * 0.5f;
            if (moveType.Equals(MoveType.Horizontal))
            {
                int coord = 1 * direction;
                if (!Physics2D.OverlapBox(movePoint.position + new Vector3(coord, 0f, 0f), hitSize, 0, obstacleMask))
                {
                    movePoint.position += new Vector3(coord, 0f, 0f);
               //    sprite.localScale = new Vector2(flipX * coord, sprite.localScale.y);
                    mover.MoveTo(movePoint.position);
                }
                else {

                    direction *= -1;
                }
            }
            else
            {
                int coord = 1 * direction;
                if (!Physics2D.OverlapBox(movePoint.position + new Vector3(0f, coord, 0f), hitSize, 0, obstacleMask))
                {
                    movePoint.position += new Vector3(0f, coord, 0f);
                    mover.MoveTo(movePoint.position);
                }
                else
                {

                    direction *= -1;
                }
            }


        
    }
}

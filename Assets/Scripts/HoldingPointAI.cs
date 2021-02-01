using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingPointAI : MonoBehaviour
{
    [SerializeField] SpriteRenderer Sprite;
    Transform target;
    Vector3 closestPos;
    float angle;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponentInParent<Enemy>();
    }


    public void HandleAiming()
    {

        closestPos = DirectionUtils.CalculateClosestDirection(target.position, transform.position);

        Vector3 aimDirection = (closestPos - transform.position).normalized;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;



        transform.eulerAngles = new Vector3(0, 0, angle);


        Vector3 localScale = Vector3.one;
        Vector3 playerLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1;
            playerLocalScale.x = -1;
        }
        else
        {

            localScale.y = 1;
            playerLocalScale.x = 1;
        }
        transform.localScale = localScale;
        Sprite.transform.localScale = playerLocalScale;

    }


    public void Shoot() {


        GameObject go = BulletObjectPool.SharedInstance.GetPooledObject();
        if (go != null)
        {
            go.transform.position = enemy.gameObject.transform.position + DirectionUtils.GetBulletCoordinates(DirectionUtils.AngleToDirection(angle));
            go.GetComponent<BulletController>().setDirection(DirectionUtils.AngleToDirection(angle));
            DirectionUtils.RotateBullet(DirectionUtils.AngleToDirection(angle), go);
            go.SetActive(true);
        }
    }

}

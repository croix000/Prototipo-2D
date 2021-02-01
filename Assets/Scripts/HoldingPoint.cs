using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingPoint : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSprite;
    bool isCharacterFlipON;
    Transform target;
    Player player;
    AudioSource shotSFX;
    [SerializeField]  ParticleSystem muzzleFlash;
    [SerializeField] GameObject shellSpawnPoint;
    Vector3 closestPos;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {

        shotSFX = GetComponent<AudioSource>() ;
        target = GameObject.FindGameObjectWithTag("Crossair").transform;
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShooting();
    }


    private void HandleAiming() {



        Vector3 p = Input.mousePosition;
        p.z = 10;
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(p);


        Vector3 closestPos = DirectionUtils.CalculateClosestDirection(target.position, transform.position);


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
        shellSpawnPoint.transform.localScale = localScale;
        playerSprite.transform.localScale = playerLocalScale;

    }


    private void HandleShooting() {

        if (Input.GetMouseButtonDown(0)) {
            SpawnBullet();
            SoundManager.Instance.PlaySFX(shotSFX);
            player.Shot();
            muzzleFlash.Play();
        }
    }

    void SpawnBullet() {

       GameObject go = BulletPlayerObjectPool.SharedInstance.GetPooledObject();
        if (go != null)
        {
            go.transform.position = player.gameObject.transform.position + DirectionUtils.GetBulletCoordinates(DirectionUtils.AngleToDirection(angle));  
            go.GetComponent<BulletController>().setDirection(DirectionUtils.AngleToDirection(angle));
            DirectionUtils.RotateBullet(DirectionUtils.AngleToDirection(angle), go);
            go.SetActive(true);
        }
       
    }
}

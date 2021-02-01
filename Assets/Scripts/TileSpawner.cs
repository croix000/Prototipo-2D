using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{

    [SerializeField] bool neutralFloor;
    public bool ShowEditor;
    DungeonManager dungeonManager;
    // Start is called before the first frame update

    private void Awake()
    {
        dungeonManager = FindObjectOfType<DungeonManager>();
        GameObject goFloor;
        if (!neutralFloor)
        {
            goFloor = Instantiate(dungeonManager.GetFloorPrefab(), transform.position, Quaternion.identity) as GameObject;
            goFloor.name = "Floor";
            goFloor.transform.SetParent(dungeonManager.transform);


            //goFloor.GetComponent<SpriteRenderer>().color = dungeonManager.GetFloorColor();
        }
        else
        {

            goFloor = Instantiate(dungeonManager.GetNeutralFloorPrefab(), transform.position, Quaternion.identity) as GameObject;
            goFloor.name = "Floor";
            goFloor.transform.SetParent(dungeonManager.transform);

        }
        if (transform.position.x > dungeonManager.maxX)
        {
            dungeonManager.maxX = transform.position.x;
        }
        if (transform.position.x < dungeonManager.minX)
        {
            dungeonManager.minX = transform.position.x;
        }
        if (transform.position.y > dungeonManager.maxY)
        {
            dungeonManager.maxY = transform.position.y;
        }
        if (transform.position.y < dungeonManager.minY)
        {
            dungeonManager.minY = transform.position.y;
        }


    }
    void Start()
    {
        LayerMask envMask = LayerMask.GetMask("Wall", "Floor");
        Vector2 hitSize = Vector2.one * 0.8f;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {

                Vector2 targetPos = new Vector2(transform.position.x + x, transform.position.y + y);

                Collider2D hit = Physics2D.OverlapBox(targetPos, hitSize, 0, envMask);

                if (!hit)
                {

                   /* if (!dungeonManager.isLevelWallTileRuled())
                    {

                        GameObject goWall = Instantiate(dungeonManager.GetWall(), targetPos, Quaternion.identity) as GameObject;
                        goWall.name = dungeonManager.wallPrefab.name;
                        goWall.transform.SetParent(dungeonManager.transform);

                    }
                    else
                    {*/
                        dungeonManager.placeWallRuleTile(targetPos);

                   // }

                    //goWall.GetComponent<SpriteRenderer>().color = dungeonManager.GetWallColor();
                }



            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnDrawGizmos()
    {
        if (ShowEditor)
        {


            Gizmos.color = Color.white;

            Gizmos.DrawCube(transform.position, Vector3.one);

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {

                    Gizmos.DrawWireCube(new Vector3(transform.position.x + x, transform.position.y + y, 0), Vector3.one * 0.9f);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


[System.Serializable]
public struct ColorPalette
{
    [SerializeField] string name;
    public Color FloorColor;
    public Color WallColor;
    public Color bgColor;
};

[System.Serializable]
public struct EnemyPrefab
{
    [SerializeField] string name;
    public float probabilty;
    public GameObject prefab;
};

[System.Serializable]
public struct Zones
{

    [SerializeField] string name;
    public bool ruleTileWall;
    public GameObject[] floorPrefabs;
    public GameObject[] decorPrefabs;
    public GameObject[] wallPrefabs;
    public Tile wallTile;
    [SerializeField] public ColorPalette ZoneColor;
    public AudioClip music;
};
public enum DungeonType { None, Caverns, Rooms }

public class DungeonManager : MonoBehaviour
{
    [Range(25, 1000)] [SerializeField] int totalFloorCount;
    [Range(0, 100)] [SerializeField] int itemSpawnRate;
    [Range(0, 100)] [SerializeField] int enemySpawnRate;

    [SerializeField] DungeonType dungeonType;
    [HideInInspector] public float minX, maxX, minY, maxY;
    float safeDistance = 3f;

    [SerializeField] Tilemap wallTileMap;
    [SerializeField] EnemyPrefab[] enemyPrefab;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject exitPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject crystalPrefab;
    int crystalsCreated;
    [SerializeField] GameObject[] randomItems;
    float[] OrcsCumulative;
    LayerMask floorMask;
    LayerMask wallMask;

    [SerializeField] Zones[] zones;

    List<Vector3> floorList = new List<Vector3>();
    List<Vector3> wallList = new List<Vector3>();
    
    [SerializeField] GameObject miniMapCam;
    private void Start()
    {

        floorMask = LayerMask.GetMask("Floor");
        wallMask = LayerMask.GetMask("Wall");

        switch (dungeonType)
        {
            case DungeonType.Caverns:
                RandomWalker();
                break;
            case DungeonType.Rooms:
                RoomWalker();
                break;

        }
        MakeCumulative();
        SoundManager.Instance.PlayGameMusic(0);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {

            PlayerPrefs.SetInt("Health", 2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void MakeCumulative()  
    {
        float current = 0;
        int itemCount = enemyPrefab.Length;
        OrcsCumulative = new float[itemCount];
        for (int i = 0; i < itemCount; i++)
        {
            current += enemyPrefab[i].probabilty;
            OrcsCumulative[i] = current;
        }

        if (current > 1.0f)
        {
            Debug.Log(" Probabilities exceed 100%");
        }

    }
    GameObject GetRandomOrc()
    {
        float rnd = Random.Range(0, 1.0f);
        int itemCount = OrcsCumulative.Length;

        for (int i = 0; i < itemCount; i++)
        {
            if (rnd <= OrcsCumulative[i])
            {
                return enemyPrefab[i].prefab;
            }
        }

        return null;
    }




    void RandomWalker()
    {
        Vector3 currentPos = Vector3.zero;
        //set tile at currentPos
        floorList.Add(currentPos);

        while (floorList.Count < totalFloorCount)
        {
            currentPos += GetRandomDirection();

            if (!InFloorList(currentPos))
                floorList.Add(currentPos);
        }
        SpawnFloor();
        StartCoroutine(DelayProgress());
    }
    void BuildMenu()
    {
        StartCoroutine(DelayBuild());
    }


    void RoomWalker()
    {
        Vector3 currentPos = Vector3.zero;
        //set tile at currentPos
        floorList.Add(currentPos);

        while (floorList.Count < totalFloorCount)
        {

            Vector3 walkDirection = GetRandomDirection();

            int walkLength = Random.Range(9, 18);

            for (int i = 0; i < walkLength; i++)
            {


                currentPos += walkDirection;
                if (!InFloorList(currentPos))
                    floorList.Add(currentPos);

                for (int w = -1; w < 1; w++)
                {
                    for (int h = -1; h < 1; h++)
                    {
                        Vector3 offset = new Vector3(w, h, 0);
                        if (!InFloorList(currentPos + offset))
                            floorList.Add(currentPos + offset);
                    }
                }

            }
            //create a room at the end of the long walk 


            int width = Random.Range(4, 6);
            int height =Random.Range(4, 6);

            for (int w = -width; w < width; w++)
            {
                for (int h = -height; h < height; h++)
                {
                    Vector3 offset = new Vector3(w, h, 0);
                    if (!InFloorList(currentPos + offset))
                        floorList.Add(currentPos + offset);
                }
            }
        }

        SpawnFloor();
        StartCoroutine(DelayProgress());
    }



    
    void SpawnFloor()
    {

        for (int i = 0; i < floorList.Count; i++)
        {
            GameObject goTile = Instantiate(tilePrefab, floorList[i], Quaternion.identity) as GameObject;
            goTile.name = tilePrefab.name;
            goTile.transform.SetParent(transform);
        }
    }
    IEnumerator DelayBuild()
    {
        while (FindObjectsOfType<TileSpawner>().Length > 0)
        {
            yield return null;
        }
        wallTileMap.RefreshAllTiles();

    }
    IEnumerator DelayProgress()
    {




        while (FindObjectsOfType<TileSpawner>().Length > 0)
        {
            yield return null;
        }
        wallTileMap.RefreshAllTiles();

        ExitDoor();
        Vector2 hitSize = Vector2.one * 0.8f;
        for (int x = (int)minX - 2; x <= maxX + 2; x++)
        {
            for (int y = (int)minY - 2; y <= maxY + 2; y++)
            {
                Collider2D hitFloor = Physics2D.OverlapBox(new Vector2(x, y), hitSize, 0, floorMask);

                if (hitFloor)
                {
                    if (!Equals(hitFloor.transform.position, floorList[floorList.Count - 1]))
                    {

                        Collider2D hitTop = Physics2D.OverlapBox(new Vector2(x, y + 1), hitSize, 0, wallMask);
                        Collider2D hitRight = Physics2D.OverlapBox(new Vector2(x + 1, y), hitSize, 0, wallMask);
                        Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(x - 1, y), hitSize, 0, wallMask);
                        Collider2D hitDown = Physics2D.OverlapBox(new Vector2(x, y - 1), hitSize, 0, wallMask);
                        RandomEnemies(hitFloor, hitTop, hitDown, hitLeft, hitRight);


                    }
                }
            }

            while (crystalsCreated < 3)
            {
                int randX = Random.Range((int)minX, (int)maxX);
                int randY = Random.Range((int)minY, (int)maxY);
                Collider2D hitFloor = Physics2D.OverlapBox(new Vector2(randX, randY), hitSize, 0, floorMask);
                if (hitFloor)
                {
                    if (!Equals(hitFloor.transform.position, floorList[floorList.Count - 1]))
                    {
                        Collider2D hitTop = Physics2D.OverlapBox(new Vector2(randX, randY + 1), hitSize, 0, wallMask);
                        Collider2D hitRight = Physics2D.OverlapBox(new Vector2(randX + 1, randY), hitSize, 0, wallMask);
                        Collider2D hitLeft = Physics2D.OverlapBox(new Vector2(randX - 1, randY), hitSize, 0, wallMask);
                        Collider2D hitDown = Physics2D.OverlapBox(new Vector2(randX, randY - 1), hitSize, 0, wallMask);
                        RandomCrystals(hitFloor, hitTop, hitDown, hitLeft, hitRight);
                    }
                }
            }
            miniMapCam.transform.position = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, -60);
        }


        void ExitDoor()
        {



            Vector3 doorPos = FindFurthestTile(); ;

            GameObject goDoor = Instantiate(exitPrefab, doorPos, Quaternion.identity) as GameObject;
            goDoor.name = exitPrefab.name;
            goDoor.transform.SetParent(transform);

        }

        Vector3 FindFurthestTile()
        {

            float distance = 0f;
            Vector3 result = Vector3.zero;
            foreach (Vector3 pos in floorList)
            {

                if (Vector3.Distance(pos, Vector3.zero) > distance)
                {
                    distance = Vector3.Distance(pos, Vector3.zero);
                    result = pos;
                }

            }
            return result;

        }

        void RandomCrystals(Collider2D hitFloor, Collider2D hitTop, Collider2D hitDown, Collider2D hitLeft, Collider2D hitRight)
        {
            if ((hitTop || hitDown || hitLeft || hitRight) && !(hitTop && hitDown) && !(hitLeft && hitRight))
            {
                int roll = Random.Range(1, 101);
                if (roll <= itemSpawnRate)
                {
                    GameObject goItem = Instantiate(crystalPrefab, hitFloor.transform.position, Quaternion.identity) as GameObject;
                    goItem.transform.SetParent(hitFloor.transform);
                    crystalsCreated++;
                }
            }
        }

        void RandomEnemies(Collider2D hitFloor, Collider2D hitTop, Collider2D hitDown, Collider2D hitLeft, Collider2D hitRight)
        {
            if (!hitTop && !hitDown && !hitLeft && !hitRight)
            {


                int roll = Random.Range(1, 101);
                if (checkIsSafeForPlayer(hitFloor.transform.position) && roll <= enemySpawnRate)
                {


                        GameObject go = GetRandomOrc();
                        if (go != null)
                            GameObject.Instantiate(go, hitFloor.transform.position, Quaternion.identity);

                }
            }
        }

    }

    bool checkIsSafeForPlayer(Vector3 pos)
    {
        return Vector3.Distance(pos, Vector3.zero) > safeDistance;
    }

    public GameObject GetFloorPrefab()
    {

        return zones[0].floorPrefabs[Random.Range(0, zones[0].floorPrefabs.Length)];

    }

    public GameObject GetNeutralFloorPrefab()
    {
        
        return zones[0].floorPrefabs[0];
    }
    public Vector3 GetRandomDirection()
    {


        switch (Random.Range(1, 5))
        {
            case 1:
                return Vector3.up;
            case 2:
                return Vector3.right;
            case 3:
                return Vector3.down;
            case 4:
                return Vector3.left;

        }
        return Vector3.zero;

    }

    bool InFloorList(Vector3 currentPos)
    {


        for (int i = 0; i < floorList.Count; i++)
        {

            if (Vector3.Equals(currentPos, floorList[i]))
                return true;

        }
        return false;
    }


    public Color GetWallColor()
    {
       
            return zones[0].ZoneColor.WallColor;

    }

    public GameObject GetWall()
    {
            return zones[0].wallPrefabs[Random.Range(0, zones[0].wallPrefabs.Length)];
 

    }

    public Color GetBGColor()
    {
       
            return zones[0].ZoneColor.bgColor;

    }


    public void placeWallRuleTile(Vector3 position)
    {

        Vector3Int currentCell = wallTileMap.WorldToCell(position);
     
        wallTileMap.SetTile(currentCell, zones[0].wallTile);
        wallList.Add(position);
    }

    public bool isLevelWallTileRuled()
    {
      
        return zones[0].ruleTileWall;
    }

    private void refreshWallTile()
    {

        Vector3Int currentCell = wallTileMap.WorldToCell(wallList[wallList.Count - 1]);
        wallTileMap.RefreshTile(currentCell);
        wallList.RemoveAt(wallList.Count - 1);


    }
}

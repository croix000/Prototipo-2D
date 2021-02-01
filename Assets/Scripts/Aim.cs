using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    [SerializeField] GameObject aimGrid;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Cursor.visible = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {

            if (!aimGrid.activeInHierarchy)
            {

                aimGrid.SetActive(true);
                spriteRenderer.enabled = true;
            }
            Vector3 p = Input.mousePosition;
            p.z = 10;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            pos = new Vector3(Mathf.Floor(pos.x), Mathf.Floor(pos.y), 10);


            Vector3 aimDirection = (pos - player.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;


            if (AngleIsValid(angle))
            {
                transform.position = pos;
                aimDirection = (pos - transform.position).normalized;
                angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;


                transform.eulerAngles = new Vector3(0, 0, angle);
            }

        }
        else {
            if (aimGrid.activeInHierarchy)
            {

                aimGrid.SetActive(false);
                spriteRenderer.enabled = false;
            }


        }



    }


    bool AngleIsValid(float angle) {

        return angle ==90 || angle == 180 || angle == 135 || angle == 45 || angle == 0 ||
              angle ==-90 || angle == -180 || angle == -135 || angle == -45;
    }

} 

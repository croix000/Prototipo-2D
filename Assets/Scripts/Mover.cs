using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType { None, Horizontal, Vertical }
public class Mover : MonoBehaviour
{

    private float moveSpeed = 5f;
    public void MoveTo(Vector3 target)
    {

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target, moveSpeed);
        transform.position = smoothedPosition;

    }
}

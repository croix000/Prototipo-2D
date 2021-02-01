using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { U, D, L, R, UL, UR, DL, DR }


public static class DirectionUtils 
{

    public static Vector3 GetVector3(Direction dir) {

        switch (dir)
        {
            case Direction.U:
                return Vector3.up;
            case Direction.D:
                return Vector3.down;
            case Direction.L:
                return Vector3.left;
            case Direction.R:
                return Vector3.right;
            case Direction.UL:
                return Vector3.up+Vector3.left;
            case Direction.UR:
                return Vector3.up+Vector3.right;
            case Direction.DL:
                return Vector3.down+Vector3.left;
            case Direction.DR:
                return Vector3.down+Vector3.right;
            default:
                return Vector3.zero;
        }

    }


    public static Vector3[] GetBulletCoordinates(CannonType cannonType) {
        Vector3[] result;
        switch (cannonType)
        {
            case CannonType.U:
                result = new Vector3[1];
                result[0] = new Vector3(0, 1, 0);
                break;
            case CannonType.D:
                result = new Vector3[1];
                result[0] = new Vector3(0, -1, 0);
                break;
            case CannonType.L:
                result = new Vector3[1];
                result[0] = new Vector3(-1, 0, 0);
                break;
            case CannonType.R:
                result = new Vector3[1];
                result[0] = new Vector3(1, 0, 0);
                break;
            case CannonType.UD:
                result = new Vector3[2];
                result[0] = new Vector3(0, 1, 0);
                result[1] = new Vector3(0, -1, 0);
                break;
            case CannonType.UL:
                result = new Vector3[2];
                result[0] = new Vector3(0, 1, 0);
                result[1] = new Vector3(-1, 0, 0);
                break;
            case CannonType.UR:
                result = new Vector3[2];
                result[0] = new Vector3(0, 1, 0);
                result[1] = new Vector3(1, 0, 0);
                break;
            case CannonType.DL:
                result = new Vector3[2];
                result[0] = new Vector3(0, -1, 0);
                result[1] = new Vector3(-1, 0, 0);
                break;
            case CannonType.DR:
                result = new Vector3[2];
                result[0] = new Vector3(0, -1, 0);
                result[1] = new Vector3(1, 0, 0);
                break;
            case CannonType.LR:
                result = new Vector3[2];
                result[0] = new Vector3(-1, 0, 0);
                result[1] = new Vector3(1, 0, 0);
                break;
            case CannonType.ULR:
                result = new Vector3[3];
                result[0] = new Vector3(0, 1, 0);
                result[1] = new Vector3(-1, 0, 0);
                result[2] = new Vector3(1, 0, 0);
                break;
            case CannonType.UDR:
                result = new Vector3[3];
                result[0] = new Vector3(0, 1, 0);
                result[1] = new Vector3(0, -1, 0);
                result[2] = new Vector3(1, 0, 0);
                break;
            case CannonType.UDL:
                result = new Vector3[3];
                result[0] = new Vector3(0, 1, 0);
                result[1] = new Vector3(0, -1, 0);
                result[2] = new Vector3(-1, 0, 0);
                break;
            case CannonType.DLR:
                result = new Vector3[3];
                result[0] = new Vector3(0, -1, 0);
                result[1] = new Vector3(-1, 0, 0);
                result[2] = new Vector3(1, 0, 0);
                break;
            case CannonType.UDLR:
                result = new Vector3[4];
                result[0] = new Vector3(0, 1, 0);
                result[1] = new Vector3(0 -1, 0);
                result[2] = new Vector3(-1, 0, 0);
                result[3] = new Vector3(1, 0, 0);
                break;
            default:
                result = new Vector3[1];
                result[0] = new Vector3(0, 0, 0);
                break;
        }

        return result;
    }

    public static Vector3 GetBulletCoordinates(Direction dir)
    {
        Vector3 result;
        switch (dir)
        {
            case Direction.U:
                result = new Vector3(0, 1, 0);
                break;
            case Direction.D:
                result = new Vector3(0, -1, 0);
                break;
            case Direction.L:
                result= new Vector3(-1, 0, 0);
                break;
            case Direction.R:
                result = new Vector3(1, 0, 0);
                break;
            case Direction.UL:
                result = new Vector3(-1, 1, 0);
                break;
            case Direction.UR:
                result= new Vector3(1, 1, 0);
                break;
            case Direction.DL:
                result= new Vector3(-1, -1, 0);
                break;
            case Direction.DR:
                result = new Vector3(1, -1, 0);
                break;
            default:
                result = new Vector3(0, 0, 0);
                break;
        }

        return result;
    }
    public static Direction[] GetBulletDirection(CannonType cannonType)
    {
        Direction[] result;
        switch (cannonType)
        {
            case CannonType.U:
                result = new Direction[1];
                result[0] = Direction.U;
                break;
            case CannonType.D:
                result = new Direction[1];
                result[0] = Direction.D;
                break;
            case CannonType.L:
                result = new Direction[1];
                result[0] = Direction.L;
                break;
            case CannonType.R:
                result = new Direction[1];
                result[0] = Direction.R;
                break;
            case CannonType.UD:
                result = new Direction[2];
                result[0] = Direction.U;
                result[1] = Direction.D;
                break;
            case CannonType.UL:
                result = new Direction[2];
                result[0] = Direction.U;
                result[1] = Direction.L;
                break;
            case CannonType.UR:
                result = new Direction[2];
                result[0] = Direction.U;
                result[1] = Direction.R;
                break;
            case CannonType.DL:
                result = new Direction[2];
                result[0] = Direction.D;
                result[1] = Direction.L;
                break;
            case CannonType.DR:
                result = new Direction[2];
                result[0] = Direction.D;
                result[1] = Direction.R;
                break;
            case CannonType.LR:
                result = new Direction[2];
                result[0] = Direction.L;
                result[1] = Direction.R;
                break;
            case CannonType.ULR:
                result = new Direction[3];
                result[0] = Direction.U;
                result[1] = Direction.L;
                result[2] = Direction.R;
                break;
            case CannonType.UDR:
                result = new Direction[3];
                result[0] = Direction.U;
                result[1] = Direction.D;
                result[2] = Direction.R;
                break;
            case CannonType.UDL:
                result = new Direction[3];
                result[0] = Direction.U;
                result[1] = Direction.D;
                result[2] = Direction.L;
                break;
            case CannonType.DLR:
                result = new Direction[3];
                result[0] = Direction.D;
                result[1] = Direction.L;
                result[2] = Direction.R;
                break;
            case CannonType.UDLR:
                result = new Direction[4];
                result[0] = Direction.U;
                result[0] = Direction.D;
                result[2] = Direction.L;
                result[3] = Direction.R;
                break;
            default:
                result = new Direction[1];
                result[0] = Direction.U;
                break;
        }

        return result;
    }

    public static void RotateBullet(Direction dir ,GameObject bullet) {
        switch (dir)
        {
            case Direction.U:
                bullet.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.D:
                bullet.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.L:
                bullet.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.R:
                bullet.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.UL:
                bullet.transform.rotation = Quaternion.Euler(0, 0, -135);
                break;
            case Direction.UR:
                bullet.transform.rotation = Quaternion.Euler(0, 0, 135);
                break;
            case Direction.DL:
                bullet.transform.rotation = Quaternion.Euler(0, 0, -45);
                break;
            case Direction.DR:
                bullet.transform.rotation = Quaternion.Euler(0, 0, 45);
                break;
            default:
                bullet.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }


    }

    public static Direction AngleToDirection(float angle)
    {
        switch (angle)
        {
            case 180:
                return Direction.L;
            case 0:
                return Direction.R;
            case -90:
                return Direction.D;
            case 90:
                return Direction.U;
            case -135:
                return Direction.DL;
            case 135:
                return Direction.UL;
            case -45:
                return Direction.DR;
            case 45:
                return Direction.UR;
            default:
                return Direction.U;
        }


    }


    public static Vector3 CalculateClosestDirection(Vector3 target, Vector3 position)
    {
        float minDistance = 1000f;
        Vector3 closestPosition = Vector3.zero;
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            Vector3 result;
            switch (dir)
            {
                case Direction.U:
                    result= Vector3.up;
                    break;
                case Direction.D:
                    result = Vector3.down;
                    break;
                case Direction.L:
                    result = Vector3.left;
                    break;
                case Direction.R:
                    result = Vector3.right;
                    break;
                case Direction.UL:
                    result = Vector3.up + Vector3.left;
                    break;
                case Direction.UR:
                    result = Vector3.up + Vector3.right;
                    break;
                case Direction.DL:
                    result = Vector3.down + Vector3.left;
                    break;
                case Direction.DR:
                    result = Vector3.down + Vector3.right;
                    break;
                default:
                    result = Vector3.zero;
                    break;
            }
            float distance = Vector3.Distance(target, result+position);
            if (distance< minDistance) {
                minDistance = distance;
                closestPosition = result + position;

            }

        }
        return closestPosition;

    }

    

    public static bool CheckTargetIsHorizontal(Vector3 target, Vector3 position) {

        return target.x == position.x;

    }

    public static bool CheckTargetIsVertical(Vector3 target, Vector3 position)
    {

        return target.y == position.y;

    }
}

using UnityEngine;
using System.Collections;

public enum EDirection
{
    None = 0,
    Up = 1,
    Down = 2,
    Left = 3,
    Right = 4
}

public class GameCamera : Singleton<GameCamera> {

    private Camera cam;
    public Camera Camera
    {
        get { return this.cam; }
    }

    private Bounds bounds;
    public Bounds Bounds
    {
        get { return this.bounds; }
    }

    void Awake()
    {
        this.cam = Camera.main;
    }

    void Start()
    {
        float screenAspect = ((float)Screen.width) / (float)Screen.height;
        float cameraHeight = this.cam.orthographicSize * 2.0f;

        bounds = new Bounds(this.cam.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
    }

    // Check if the bounds of the target is completelly in of camera bounds
    public bool InOfBounds(Bounds targetBounds, EDirection direction)
    {
        switch (direction)
        {
            case EDirection.Down:
                if (targetBounds.min.y > this.bounds.min.y)
                {
                    return true;
                }
                break;
            case EDirection.Up:
                if (targetBounds.max.y < this.bounds.max.y)
                {
                    return true;
                }
                break;
            case EDirection.Left:
                if (targetBounds.min.x > this.bounds.min.x)
                {
                    return true;
                }
                break;
            case EDirection.Right:
                if (targetBounds.max.x < this.bounds.max.x)
                {
                    return true;
                }
                break;
            default:
                Debug.LogWarning("OutOfBounds with no defined direction: " + direction);
                break;
        }
        return false;
    }

    public bool IsInside(Vector3 point)
    {
        return this.bounds.Contains(point);
    }

    // Check if the bounds of the target is completelly out of camera bounds
    public bool OutOfBounds(Bounds targetBounds, EDirection direction)
    {
        switch(direction)
        {
            case EDirection.Down:
                if (targetBounds.max.y < this.bounds.min.y)
                {
                    return true;
                }
                break;
            case EDirection.Up:
                if (targetBounds.min.y > this.bounds.max.y)
                {
                    return true;
                }
                break;
            case EDirection.Left:
                if (targetBounds.max.x < this.bounds.min.x)
                {
                    return true;
                }
                break;
            case EDirection.Right:
                if (targetBounds.min.x > this.bounds.max.x)
                {
                    return true;
                }
                break;
            default:
                Debug.LogWarning("OutOfBounds with no defined direction: "+direction);
                break;
        }
        return false;
    }
}

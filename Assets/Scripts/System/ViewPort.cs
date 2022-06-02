using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPort : Singleton<ViewPort>
{
    
    float minX;
    float maxX;
    float minY;
    float maxY;

    void Start()
    {
        Camera mainCamera = Camera.main;
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0.15f));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector2(1f, 0.65f));

        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;
    }

    public Vector3 PlayerMoveablePostion(Vector3 playerPosition)
    {
        playerPosition.y = Mathf.Clamp(playerPosition.y, minY, maxY);
        return playerPosition;
    }
}

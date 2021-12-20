using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const float tileSizeWidth = 48;
    const float tileSizeHeight = 48;

    RectTransform rectTransform;
    
    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();

    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();


    public Vector2Int GetGridPosition( Vector2 mousePosition )
    {

        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeWidth);

        return tileGridPosition;

    }
}
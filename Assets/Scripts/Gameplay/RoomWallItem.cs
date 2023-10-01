using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWallItem : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> listSp = new List<Sprite>();

    private Vector2Int posID;

    public void Init(Vector2Int posID)
    {
        this.posID = posID;
        ResetView();
    }

    public void ResetView()
    {
        if (posID.x == 1 && posID.y == 1)
        {
            sr.sprite = listSp[1];
        }
        else
        {
            sr.sprite = listSp[0];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFloorItem : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> listSp = new List<Sprite>();

    private Vector2Int posID;

    public void Init(Vector2Int posID)
    {
        ResetView();
        this.posID = posID;
    }

    public void ResetView()
    {
        int ran = Random.Range(0, listSp.Count);
        sr.sprite = listSp[ran];
    }

    public Vector2Int GetPosID()
    {
        return posID;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFloorItem : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> listSp = new List<Sprite>();

    public void Init()
    {
        ResetView();
    }

    public void ResetView()
    {
        int ran = Random.Range(0, listSp.Count);
        sr.sprite = listSp[ran];
    }

}

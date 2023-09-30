using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMgr : MonoBehaviour
{
    //Data
    private int roomHeight = 3;
    private int roomWidth = 3;
    [Header("RoomTileView")]
    public Transform tfFloor;
    public GameObject pfFloor;
    private List<RoomFloorItem> listFloorView = new List<RoomFloorItem>();
    [Header("RoomItemView")]
    public Transform tfItem;
    public GameObject pfItem;

    public void Init()
    {
        ResetRoomView();
    }


    public void ResetRoomView()
    {
        //Clear Floor
        PublicTool.ClearChildItem(tfFloor);
        listFloorView.Clear();
        for (int i = 0; i < roomHeight; i++)
        {
            for(int j = 0; j < roomWidth; j++)
            {
                GameObject objFloor = GameObject.Instantiate(pfFloor, tfFloor);
                RoomFloorItem itemFloor = objFloor.GetComponent<RoomFloorItem>();
                listFloorView.Add(itemFloor);
                //Position
                itemFloor.transform.localPosition = new Vector2(i * GameGlobal.tileSize, j * GameGlobal.tileSize);
            }
        }
    }



}

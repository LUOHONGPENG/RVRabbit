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
    public Transform tfFurni;
    public GameObject pfFurni;

    public void Init()
    {
        ResetRoomView();

        PublicTool.ClearChildItem(tfFurni);
        CreateFurniture(1001, new Vector2Int(0, 0));
        CreateFurniture(1002, new Vector2Int(1, 0));
    }


    public void ResetRoomView()
    {
        //Clear and Reset Floor
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

    public void CreateFurniture(int ID,Vector2Int pos)
    {
        GameObject objFurni = GameObject.Instantiate(pfFurni, tfFurni);
        RoomFurniItem itemFurni = objFurni.GetComponent<RoomFurniItem>();
        objFurni.transform.localPosition = new Vector2(pos.x * GameGlobal.tileSize, pos.y * GameGlobal.tileSize);
        itemFurni.Init(ID);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomMgr : MonoBehaviour
{
    //Data
    private int roomWidth = 3;
    private int roomHeight = 3;
    [Header("RoomTileView")]
    public Transform tfFloor;
    public GameObject pfFloor;
    private List<RoomFloorItem> listFloorView = new List<RoomFloorItem>();
    [Header("RoomItemView")]
    public Transform tfFurni;
    public GameObject pfFurni;
    private List<RoomFurniItem> listFurniView = new List<RoomFurniItem>();
    public Dictionary<Vector2Int, int> dicFurniOccupy = new Dictionary<Vector2Int, int>();

    private int furniKey = 0;


    public void Init()
    {
        roomWidth = 2;
        roomHeight = 3;
        InitRoomOccupy();
        ResetRoomView();
        InitFurniture();
    }

    #region RoomOccupy
    public void InitRoomOccupy()
    {
        dicFurniOccupy.Clear();
        for(int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < roomHeight; j++)
            {
                Vector2Int key = new Vector2Int(i, j);
                dicFurniOccupy.Add(key, -1);
            }
        }
    }

    public void RefreshRoomOccupy()
    {
        for (int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < roomHeight; j++)
            {
                Vector2Int key = new Vector2Int(i, j);
                if (!dicFurniOccupy.ContainsKey(key))
                {
                    dicFurniOccupy.Add(key, -1);
                }
                else
                {
                    dicFurniOccupy[key] = -1;
                }
            }
        }

        foreach(RoomFurniItem furni in listFurniView)
        {
            Vector2Int furniPos = furni.GetPosID();
            Vector2Int size = furni.GetSize();

            if (furniPos.x < 0 || furniPos.y < 0)
            {
                continue;
            }

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Vector2Int curPos = furniPos + new Vector2Int(i, j);
                    if (dicFurniOccupy.ContainsKey(curPos))
                    {
                        dicFurniOccupy[curPos] = furni.GetKeyID();
                    }
                }
            }
        }
    }

    public bool CheckOccupy(int moveInKeyID, Vector2Int posID,Vector2Int size)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector2Int curKey = posID + new Vector2Int(i, j);
                if (dicFurniOccupy.ContainsKey(curKey))
                {
                    int roomKey = dicFurniOccupy[curKey];
                    if (roomKey >= 0 && roomKey != moveInKeyID)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }

    #endregion

    #region RoomView
    public void ResetRoomView()
    {
        //Clear and Reset Floor
        PublicTool.ClearChildItem(tfFloor);
        listFloorView.Clear();
        for (int i = 0; i < roomWidth; i++)
        {
            for(int j = 0; j < roomHeight; j++)
            {
                GameObject objFloor = GameObject.Instantiate(pfFloor, tfFloor);
                RoomFloorItem itemFloor = objFloor.GetComponent<RoomFloorItem>();
                itemFloor.Init(new Vector2Int(i,j));
                listFloorView.Add(itemFloor);
                //Position
                itemFloor.transform.localPosition = new Vector2(i * GameGlobal.tileSize, j * GameGlobal.tileSize);
            }
        }
    }
    #endregion

    public void InitFurniture()
    {
        PublicTool.ClearChildItem(tfFurni);
        listFurniView.Clear();
        CreateFurniture(1001, new Vector2Int(0, 0));
        CreateFurniture(1002, new Vector2Int(1, 0));
        CreateFurniture(1003, new Vector2Int(0, 2));
        CreateFurniture(2001, new Vector2Int(1, 1));
        //CreateFurniture(2002, new Vector2Int(1, 2));
        //CreateFurniture(2003, new Vector2Int(2, 0));

    }


    public void CreateFurniture(int typeID,Vector2Int posID)
    {
        GameObject objFurni = GameObject.Instantiate(pfFurni, tfFurni);
        RoomFurniItem itemFurni = objFurni.GetComponent<RoomFurniItem>();
        itemFurni.Init(furniKey,typeID);
        furniKey++;
        itemFurni.SetPosID(posID);
        listFurniView.Add(itemFurni);

        RefreshRoomOccupy();
    }


}

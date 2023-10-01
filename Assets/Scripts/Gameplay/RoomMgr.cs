using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomMgr : MonoBehaviour
{
    //Data
    private int roomWidth = 3;
    private int roomHeight = 3;

    [Header("Character")]

    public Animator aniRabbit;
    public Transform tfCow;
    public Transform tfChicken;


    [Header("RoomWallView")]
    public Transform tfWall;
    public GameObject pfWall;

    public Transform tfTail;

    [Header("RoomFloorView")]
    public Transform tfFloor;
    public GameObject pfFloor;
    private List<RoomFloorItem> listFloorView = new List<RoomFloorItem>();
    [Header("RoomItemView")]
    public Transform tfFurni;
    public GameObject pfFurni;
    private List<RoomFurniItem> listFurniView = new List<RoomFurniItem>();
    public Dictionary<int, RoomFurniItem> dicFurniView = new Dictionary<int, RoomFurniItem>();
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

    #region Character

    public void ShowChicken()
    {
        tfChicken.DOLocalMoveY(2.5f, 0.5f);
    }

    public void ShowCow()
    {
        tfCow.DOLocalMoveY(2.5f, 0.5f);
    }

    public void HideCharacter()
    {
        tfChicken.DOLocalMoveY(1, 0.5f);
        tfCow.DOLocalMoveY(1, 0.5f);

    }

    #endregion




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

            if (!furni.CheckValid())
            {
                continue;
            }

            List<Vector2Int> listOccupyPos = furni.GetOccupyList();
            for(int i = 0; i < listOccupyPos.Count; i++)
            {
                if (dicFurniOccupy.ContainsKey(listOccupyPos[i]))
                {
                    dicFurniOccupy[listOccupyPos[i]] = furni.GetKeyID();
                }
            }
/*            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Vector2Int curPos = furniPos + new Vector2Int(i, j);
                    if (dicFurniOccupy.ContainsKey(curPos))
                    {
                        dicFurniOccupy[curPos] = furni.GetKeyID();
                    }
                }
            }*/
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

        //Clear and Reset Wall
        PublicTool.ClearChildItem(tfWall);

        for (int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject objWall = GameObject.Instantiate(pfWall, tfWall);
                RoomWallItem itemWall = objWall.GetComponent<RoomWallItem>();
                itemWall.Init(new Vector2Int(i, j));
                //Position
                itemWall.transform.localPosition = new Vector2(i * GameGlobal.tileSize, (j+3) * GameGlobal.tileSize);
            }
        }

        tfTail.localPosition = new Vector2(2.59f + (roomWidth - 2) * GameGlobal.tileSize, 1.92f);
    }
    #endregion



    #region Furniture Generate
    public void InitFurniture()
    {
        PublicTool.ClearChildItem(tfFurni);
        listFurniView.Clear();
        dicFurniView.Clear();
        CreateFurniture(1001, new Vector2Int(-2, -2));
        CreateFurniture(1002, new Vector2Int(-1, -1));
        CreateFurniture(1003, new Vector2Int(0, -1));
        CreateFurniture(2001, new Vector2Int(3, -1));
        //CreateFurniture(2002, new Vector2Int(-2, -2));
        //CreateFurniture(2003, new Vector2Int(-2, -2));
        //CreateFurniture(2004, new Vector2Int(-2, -2));
        //CreateFurniture(2005, new Vector2Int(-2, -2));

        //CreateFurniture(2003, new Vector2Int(2, 0));

    }

    public void CreateFurniture(int typeID,Vector2Int posID)
    {
        GameObject objFurni = GameObject.Instantiate(pfFurni, tfFurni);
        RoomFurniItem itemFurni = objFurni.GetComponent<RoomFurniItem>();
        itemFurni.Init(furniKey,typeID);
        furniKey++;
        itemFurni.SetPosID(posID,true);
        listFurniView.Add(itemFurni);
        dicFurniView.Add(itemFurni.GetKeyID(), itemFurni);
        RefreshRoomOccupy();
    }

    public List<int> GetNearByFurniture(int targetKey)
    {
        List<int> listNearby = new List<int>();

        if (!dicFurniView.ContainsKey(targetKey))
        {
            return listNearby;
        }
        RoomFurniItem furniItem = dicFurniView[targetKey];
        if (!furniItem.CheckValid())
        {
            return listNearby;
        }

        List<Vector2Int> listTargetOccupy = furniItem.GetOccupyList();
        for(int i = 0; i < listTargetOccupy.Count; i++)
        {
            Vector2Int pos = listTargetOccupy[i];
            List<Vector2Int> listCheck = new List<Vector2Int>();
            listCheck.Add(pos + new Vector2Int(0, 1));
            listCheck.Add(pos + new Vector2Int(0, -1));
            listCheck.Add(pos + new Vector2Int(1, 0));
            listCheck.Add(pos + new Vector2Int(-1, 0));
            for(int j = 0;j < listCheck.Count; j++)
            {
                if (dicFurniOccupy.ContainsKey(listCheck[j]) &&
                    !listNearby.Contains(dicFurniOccupy[listCheck[j]]))
                {
                    listNearby.Add(dicFurniOccupy[listCheck[j]]);
                }
            }
        }
        return listNearby;
    }

    #endregion

    public List<int> GetExistItemID()
    {
        List<int> listExist = new List<int>();

        foreach(RoomFurniItem furni in listFurniView)
        {
            listExist.Add(furni.GetOriginalID());
        }

        return listExist;
    }


    #region FinishCalculation

    public void FinishCalcu()
    {
        foreach(RoomFurniItem furni in listFurniView)
        {
            if (furni.CheckValid())
            {
                List<int> listNearby = GetNearByFurniture(furni.GetKeyID());
                List<int> listNearbyTypeOriginID = new List<int>();
                //Level
                int tempLevel;
                for(int i = 0; i < listNearby.Count; i++)
                {
                    int checkFurniKey = listNearby[i];
                    if (dicFurniView.ContainsKey(checkFurniKey))
                    {
                        //Level
                        RoomFurniItem checkFurni = dicFurniView[checkFurniKey];
                        listNearbyTypeOriginID.Add(checkFurni.GetOriginalID());
                    }
                }
                //CheckTransformCondition

                foreach(ComboExcelItem item in GameMgr.Instance.comboData.items)
                {
                    if(furni.GetOriginalID() == item.originalID)
                    {
                        bool isCombo = true;
                        for(int i = 0; i < item.checkID.Count; i++)
                        {
                            if (!listNearbyTypeOriginID.Contains(item.checkID[i]))
                            {
                                isCombo = false;
                            }
                        }
                        if (isCombo)
                        {
                            furni.SetCurTypeID(item.changeID);
                        }
                        else
                        {
                            furni.SetCurTypeID(furni.GetOriginalID());
                        }
                    }
                }

                //Level
                tempLevel = 0;
                for (int i = 0; i < listNearby.Count; i++)
                {
                    int checkFurniKey = listNearby[i];
                    if (dicFurniView.ContainsKey(checkFurniKey))
                    {
                        //Level
                        RoomFurniItem checkFurni = dicFurniView[checkFurniKey];
                        tempLevel += checkFurni.GetFurniData().GetSupportEffect(furni.GetFurniData().furnitureType);
                    }
                }
                furni.Level = tempLevel;
                //CheckTransformCondition
            }
            else
            {
                furni.SetCurTypeID(furni.GetOriginalID());
            }
        }
    }


    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RoomFurniItem : MonoBehaviour
{
    public SpriteRenderer spFurni;
    public BoxCollider2D colFurni;

    private int keyID;
    private int originalTypeID;
    private int curTypeID;
    private Vector2Int posID;

    public void Init(int keyID,int typeID)
    {
        this.keyID = keyID;
        this.originalTypeID = typeID;
        this.curTypeID = originalTypeID;

        //Init Sprite
        spFurni.sprite = Resources.Load("Sprite/Furniture/" + GetFurniData().iconUrl, typeof(Sprite)) as Sprite;
        spFurni.transform.localPosition = new Vector2((GetFurniData().width - 1) * GameGlobal.tileSize / 2 + GetFurniData().xoffset,
            (GetFurniData().height - 1) * GameGlobal.tileSize / 2 + GetFurniData().yoffset);

        //Init Collider
        colFurni.size = new Vector2(GetFurniData().width * GameGlobal.tileSize, GetFurniData().height * GameGlobal.tileSize);
        colFurni.offset = new Vector2((GetFurniData().width - 1) * GameGlobal.tileSize / 2,
            (GetFurniData().height - 1) * GameGlobal.tileSize / 2);
    }



    #region TypeInfo
    public int GetKeyID()
    {
        return keyID;
    }

    public int GetOriginalID()
    {
        return originalTypeID;
    }

    private FurnitureExcelItem GetFurniData()
    {
        return GameMgr.Instance.ReadFurnitureData(curTypeID);
    }
    #endregion

    #region PositionInfo
    public void SetPosID(Vector2Int posID) 
    {
        this.posID = posID;
        if (posID.x >= 0 && posID.y >= 0)
        {
            this.transform.localPosition = new Vector2(posID.x * GameGlobal.tileSize, posID.y * GameGlobal.tileSize);
            spFurni.sortingOrder = 99 - posID.y;
        }
        else
        {
            spFurni.sortingOrder = 99;
        }
    }

    public void BackPos()
    {
        if (posID.x >= 0 && posID.y >= 0)
        {
            this.transform.localPosition = new Vector2(posID.x * GameGlobal.tileSize, posID.y * GameGlobal.tileSize);
        }
        else
        {
            this.transform.localPosition = new Vector2(-1, -1);
        }
    }


    public Vector2Int GetPosID()
    {
        return posID;
    }

    public Vector2Int GetSize()
    {
        return new(GetFurniData().width, GetFurniData().height);
    }

    #endregion
}

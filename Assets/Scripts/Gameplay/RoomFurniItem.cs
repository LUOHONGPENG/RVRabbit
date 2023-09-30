using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RoomFurniItem : MonoBehaviour
{
    public SpriteRenderer spFurni;
    public BoxCollider2D colFurni;

    private int originalID;
    private int curID;
    private Vector2Int posID;

    public void Init(int ID)
    {
        this.originalID = ID;
        this.curID = originalID;

        //Init Sprite
        spFurni.sprite = Resources.Load("Sprite/Furniture/" + GetFurniData().iconUrl, typeof(Sprite)) as Sprite;
        spFurni.transform.localPosition = new Vector2((GetFurniData().width - 1) * GameGlobal.tileSize / 2 + GetFurniData().xoffset,
            (GetFurniData().height - 1) * GameGlobal.tileSize / 2 + GetFurniData().yoffset);

        //Init Collider
        colFurni.size = new Vector2(GetFurniData().width * GameGlobal.tileSize, GetFurniData().height * GameGlobal.tileSize);
        colFurni.offset = new Vector2((GetFurniData().width - 1) * GameGlobal.tileSize / 2,
            (GetFurniData().height - 1) * GameGlobal.tileSize / 2);
    }

    private FurnitureExcelItem GetFurniData()
    {
        return GameMgr.Instance.ReadFurnitureData(curID);
    }

    public int GetOriginalID()
    {
        return originalID;
    }

    public void SetPosID(Vector2Int posID) 
    {
        this.posID = posID;
        if(posID.x >= 0 && posID.y >= 0)
        {
           this.transform.localPosition = new Vector2(posID.x * GameGlobal.tileSize, posID.y * GameGlobal.tileSize);
        }
    }
}

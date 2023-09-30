using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class RoomFurniItem : MonoBehaviour
{
    public SpriteRenderer spFurni;
    public BoxCollider2D colFurni;

    private int originalID;
    private int curID;

    public void Init(int ID)
    {
        this.originalID = ID;
        this.curID = originalID;

        spFurni.sprite = Resources.Load("Sprite/Furniture/" + GetFurniData().iconUrl, typeof(Sprite)) as Sprite;
        spFurni.transform.localPosition = new Vector2((GetFurniData().width - 1) * GameGlobal.tileSize / 2 + GetFurniData().xoffset,
            (GetFurniData().height - 1) * GameGlobal.tileSize / 2 + GetFurniData().yoffset);

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
}

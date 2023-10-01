using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using DG.Tweening;

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
        RefreshSprite();
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

    public void SetCurTypeID(int type)
    {
        curTypeID = type;
        RefreshSprite();
    }

    public void RefreshSprite()
    {
        spFurni.sprite = Resources.Load("Sprite/Furniture/" + GetFurniData().iconUrl, typeof(Sprite)) as Sprite;
    }

    public FurnitureExcelItem GetFurniData()
    {
        return GameMgr.Instance.ReadFurnitureData(curTypeID);
    }
    #endregion

    #region PositionInfo
    public void SetPosID(Vector2Int posID,bool isInit) 
    {
        this.posID = posID;
        if (CheckValid())
        {
            this.transform.localPosition = new Vector2(posID.x * GameGlobal.tileSize, posID.y * GameGlobal.tileSize);
            spFurni.sortingOrder = 99 - posID.y;
            spFurni.DOFade(1, 0);
        }
        else
        {
            if (isInit)
            {
                this.transform.localPosition = new Vector2(posID.x * GameGlobal.tileSize, posID.y * GameGlobal.tileSize);
            }
            spFurni.sortingOrder = 99;
            spFurni.DOFade(0.75F, 0);
        }
    }

    public void BackPos()
    {
        if (CheckValid())
        {
            this.transform.localPosition = new Vector2(posID.x * GameGlobal.tileSize, posID.y * GameGlobal.tileSize);
        }
        else
        {
            this.transform.DOLocalMove(new Vector2(-1 + Random.Range(-1f, 0.5f), -1 + Random.Range(-1f, 0.5f)), 0.5F);
            //this.transform.localPosition = new Vector2(-1 + Random.Range(-1f,0.5f), -1 + Random.Range(-1f, 0.5f));
        }
    }


    public Vector2Int GetPosID()
    {
        return posID;
    }

    public bool CheckValid()
    {
        if (posID.x >= 0 && posID.y >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2Int GetSize()
    {
        return new(GetFurniData().width, GetFurniData().height);
    }

    public List<Vector2Int> GetOccupyList()
    {
        List<Vector2Int> listOccupy = new List<Vector2Int>();
        for(int i = 0; i < GetSize().x; i++)
        {
            for(int j = 0; j < GetSize().y; j++)
            {
                Vector2Int key = new Vector2Int(i, j);
                listOccupy.Add(GetPosID() + key);
            }
        }
        return listOccupy;
    }

    #endregion

    #region LevelInfo

    private int level = 0;

    public int Level
    {
        get
        {
            if (CheckValid())
            {
                return level;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            level = value;
        }
    }

    #endregion

    #region ClickDeal

    public void ClickDeal()
    {
        FurnitureExcelItem furniItem = GetFurniData();
        if (furniItem.furnitureType== FurniType.Other || furniItem.furnitureType == FurniType.Support)
        {
            return;
        }

        if(furniItem.furnitureType == FurniType.Service && GameMgr.Instance.consumerType == ComsumerType.None)
        {
            return;
        }


        if (EnergyChange < 0 && GameMgr.Instance.countEnergy + EnergyChange < 0)
        {
            return;
        }

        GameMgr.Instance.countCoin += CoinChange;
        GameMgr.Instance.countTask += TaskChange;
        GameMgr.Instance.ChangeEnergy(EnergyChange);

        GameMgr.Instance.InvokeAction(furniItem.id);
    }



    public float LevelDelta
    {
        get
        {
            float levelDelta = 1f;
            if (Level > 0)
            {
                levelDelta = GameGlobal.levelAdd[Level - 1];
            }
            else if (Level < 0)
            {
                levelDelta = GameGlobal.levelSub[-Level - 1];
            }
            return levelDelta;
        }
    }


    public int CoinChange
    {
        get
        {
            FurnitureExcelItem furniItem = GetFurniData();
            switch (GetFurniData().furnitureType)
            {
                case FurniType.Rest:
                    return furniItem.coinDelta;
                case FurniType.Service:
                    return Mathf.RoundToInt(furniItem.coinDelta * LevelDelta);
                case FurniType.Work:
                    return furniItem.coinDelta;
            }
            return 0;
        }
    }

    public int EnergyChange
    {
        get
        {
            FurnitureExcelItem furniItem = GetFurniData();
            switch (GetFurniData().furnitureType)
            {
                case FurniType.Rest:
                    return Mathf.RoundToInt(furniItem.energyDelta * LevelDelta);
                case FurniType.Service:
                    return furniItem.energyDelta;
                case FurniType.Work:
                    return furniItem.energyDelta;
            }
            return 0;
        }
    }
    public int TaskChange
    {
        get
        {
            FurnitureExcelItem furniItem = GetFurniData();
            switch (GetFurniData().furnitureType)
            {
                case FurniType.Rest:
                    return Mathf.RoundToInt(furniItem.specialDelta * LevelDelta);
                case FurniType.Service:
                    return furniItem.specialDelta;
                case FurniType.Work:
                    return Mathf.RoundToInt(furniItem.specialDelta * LevelDelta);
            }
            return 0;
        }
    }

    #endregion


}

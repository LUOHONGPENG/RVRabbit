using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr : MonoSingleton<GameMgr>
{
    public RoomMgr roomMgr;
    [HideInInspector]
    public FurnitureExcelData furnitureData;

    public void Start()
    {
        furnitureData = ExcelManager.Instance.GetExcelData<FurnitureExcelData, FurnitureExcelItem>();
        Debug.Log(furnitureData.GetExcelItem(1001).width);
        roomMgr.Init();
    }
}

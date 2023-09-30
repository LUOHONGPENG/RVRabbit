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
        //Excel Data
        furnitureData = ExcelManager.Instance.GetExcelData<FurnitureExcelData, FurnitureExcelItem>();
        Debug.Log(furnitureData.GetExcelItem(1001).width);

        StartCoroutine(InputMgr.Instance.IE_Init());

        roomMgr.Init();
    }
}

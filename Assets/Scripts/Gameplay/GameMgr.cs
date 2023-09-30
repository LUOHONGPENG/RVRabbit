using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr : MonoSingleton<GameMgr>
{
    public RoomMgr roomMgr;


    

    public void Start()
    {
        InitExcel();

        StartCoroutine(InputMgr.Instance.IE_Init());

        roomMgr.Init();
    }
}


public partial class GameMgr 
{
    [HideInInspector]
    public FurnitureExcelData furnitureData;

    private void InitExcel()
    {
        //Excel Data
        furnitureData = ExcelManager.Instance.GetExcelData<FurnitureExcelData, FurnitureExcelItem>();
    }

    public FurnitureExcelItem ReadFurnitureData(int ID)
    {
        return furnitureData.GetExcelItem(ID);
    }


}
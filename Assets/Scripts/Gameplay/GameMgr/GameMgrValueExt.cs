using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr
{
    public int countCoin = 0;
    public int countEnergy = 10;
    public int maxEnergy = 10;
    public int countTime = 0;

    public void ChangeEnergy(int delta)
    {
        if(countEnergy + delta > maxEnergy)
        {
            countEnergy = maxEnergy;
        }
        else
        {
            countEnergy = countEnergy + delta;
        }
    }


    public InteractType interactType = InteractType.Move;

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
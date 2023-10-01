using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr
{
    public int countCoin = 10;
    public int countTask = 0;
    public int countEnergy = 10;
    public int maxEnergy = 10;
    public int countTime = 0;

    public int countSin = 0;
    public int countSex = 0;
    public int countKnife = 0;
    public int countSacrifice = 0;

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


}


public partial class GameMgr
{
    [HideInInspector]
    public FurnitureExcelData furnitureData;

    public ComboExcelData comboData;

    private void InitExcel()
    {
        //Excel Data
        furnitureData = ExcelManager.Instance.GetExcelData<FurnitureExcelData, FurnitureExcelItem>();
        comboData = ExcelManager.Instance.GetExcelData<ComboExcelData, ComboExcelItem>();
    }

    public FurnitureExcelItem ReadFurnitureData(int ID)
    {
        return furnitureData.GetExcelItem(ID);
    }

    
}
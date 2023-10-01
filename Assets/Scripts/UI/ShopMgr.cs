using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMgr : MonoBehaviour
{
    public GameObject objShop;

    public Transform tfShop;
    public GameObject pfShop;


    public void Show()
    {
        objShop.SetActive(true);

        RefreshShop();
    }

    public void RefreshShop()
    {
        PublicTool.ClearChildItem(tfShop);

        List<int> listExist = GameMgr.Instance.roomMgr.GetExistItemID();

        FurnitureExcelItem[] arrayFurni = GameMgr.Instance.furnitureData.items;

        for (int i = 0; i < arrayFurni.Length; i++)
        {
            FurnitureExcelItem furni = arrayFurni[i];
            if (!listExist.Contains(furni.id) && furni.canBuy)
            {
                GameObject objShop = GameObject.Instantiate(pfShop, tfShop);
                ShopUI itemShop = objShop.GetComponent<ShopUI>();
                itemShop.Init(furni,this);
            }
        }
    }

    public void Hide()
    {
        objShop.SetActive(false);
        PublicTool.ClearChildItem(tfShop);

    }


}

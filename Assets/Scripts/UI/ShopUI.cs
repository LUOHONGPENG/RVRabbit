using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Image imgShopUI;
    public Button btnShop;
    public Text codePrice;

    private FurnitureExcelItem furni;

    public void Init(FurnitureExcelItem furni,ShopMgr parent)
    {
        this.furni = furni;
        codePrice.text = furni.price.ToString();
        imgShopUI.sprite = Resources.Load("Sprite/Furniture/" + furni.iconUrl, typeof(Sprite)) as Sprite;

        btnShop.onClick.RemoveAllListeners();
        btnShop.onClick.AddListener(delegate ()
        {
            if(GameMgr.Instance.countCoin>= furni.price)
            {
                GameMgr.Instance.countCoin -= furni.price;
                GameMgr.Instance.roomMgr.CreateFurniture(furni.id, new Vector2Int(-2, -2));
            }
            parent.RefreshShop();
        });
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    [Header("BasicAttribute")]
    public Text codeCoin;
    public Text codeEnergy;
    public Text codeTask;
    public Text codeTime;

    public UITipMgr uiTipMgr;
    public ShopMgr shopMgr;

    [Header("MoveFurniture")]
    public Button btnMoveFurni;
    public Text txMoveFurni;
    public Text txMoveTip;

    [Header("Shop")]
    public Button btnShop;
    public Text txShop;
    public Text txShopTip;


    private bool isInit = false;

    public void Init()
    {
        isInit = true;

        btnMoveFurni.onClick.RemoveAllListeners();
        btnMoveFurni.onClick.AddListener(delegate ()
        {
            switch (GameMgr.Instance.interactType)
            {
                case InteractType.Action:
                    GameMgr.Instance.interactType = InteractType.Move;
                    break;
                case InteractType.Move:
                    GameMgr.Instance.interactType = InteractType.Action;
                    GameMgr.Instance.roomMgr.FinishCalcu();
                    break;
            }
        });

        btnShop.onClick.RemoveAllListeners();
        btnShop.onClick.AddListener(delegate ()
        {
            switch (GameMgr.Instance.interactType)
            {
                case InteractType.Action:
                    GameMgr.Instance.interactType = InteractType.Shop;
                    shopMgr.Show();
                    break;
                case InteractType.Shop:
                    GameMgr.Instance.interactType = InteractType.Action;
                    shopMgr.Hide();
                    GameMgr.Instance.roomMgr.FinishCalcu();
                    break;
            }
        });

        uiTipMgr.Init();
    }

    private void Update()
    {
        if (!isInit)
        {
            return;
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        codeCoin.text = GameMgr.Instance.countCoin.ToString();
        codeEnergy.text = string.Format("{0}/{1}", GameMgr.Instance.countEnergy, GameMgr.Instance.maxEnergy);
        codeTask.text = string.Format("{0}/{1}", GameMgr.Instance.countTask, 100);

        switch (GameMgr.Instance.interactType)
        {
            case InteractType.Move:
                txMoveFurni.text = "Finish Move";
                txShop.text = "Shop";
                btnMoveFurni.interactable = true;
                btnShop.interactable = false;
                txMoveTip.gameObject.SetActive(true);
                txShopTip.gameObject.SetActive(false);
                break;
            case InteractType.Action:
                txMoveFurni.text = "Move Furniture";
                txShop.text = "Shop";
                btnMoveFurni.interactable = true;
                btnShop.interactable = true;
                txMoveTip.gameObject.SetActive(false);
                txShopTip.gameObject.SetActive(false);
                break;
            case InteractType.Wait:
                txMoveFurni.text = "Move Furniture";
                txShop.text = "Shop";
                btnMoveFurni.interactable = false;
                btnShop.interactable = false;
                txMoveTip.gameObject.SetActive(false);
                txShopTip.gameObject.SetActive(false);
                break;
            case InteractType.Shop:
                txMoveFurni.text = "Move Furniture";
                txShop.text = "Back";
                btnMoveFurni.interactable = false;
                btnShop.interactable = true;
                txMoveTip.gameObject.SetActive(false);
                txShopTip.gameObject.SetActive(true);
                break;
        }
    }

    
}

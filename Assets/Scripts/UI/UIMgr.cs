using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    [Header("BasicAttribute")]
    public Text codeCoin;

    [Header("MoveFurniture")]
    public Button btnMoveFurni;
    public Text txMoveFurni;

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
                    break;
            }
        });
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

        switch (GameMgr.Instance.interactType)
        {
            case InteractType.Move:
                txMoveFurni.text = "Finish";
                btnMoveFurni.interactable = true;
                break;
            case InteractType.Action:
                txMoveFurni.text = "Move Furniture";
                btnMoveFurni.interactable = true;
                break;
            case InteractType.Wait:
                txMoveFurni.text = "Move Furniture";
                btnMoveFurni.interactable = false;
                break;
        }
    }

    
}

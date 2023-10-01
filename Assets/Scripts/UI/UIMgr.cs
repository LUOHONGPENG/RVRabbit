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
                    GameMgr.Instance.roomMgr.FinishCalcu();
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
        codeEnergy.text = string.Format("{0}/{1}", GameMgr.Instance.countEnergy, GameMgr.Instance.maxEnergy);
        codeTask.text = string.Format("{0}/{1}", GameMgr.Instance.countTask, 100);
        codeTime.text = string.Format("{0}/{1}", GameMgr.Instance.countTime, 40);


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

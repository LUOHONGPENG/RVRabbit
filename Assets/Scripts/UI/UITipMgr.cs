using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ShowTipStruct
{
    public string name;
    public int level;
    public string desc;
    public string type;

    public int coin;
    public int energy;
    public int task;

    public ShowTipStruct(string name, int level, string desc, string type,int coin,int energy,int task)
    {
        this.name = name;
        this.level = level;
        this.desc = desc;
        this.type = type;
        this.coin = coin;
        this.energy = energy;
        this.task = task;
    }
}



public class UITipMgr : MonoBehaviour
{
    public GameObject objBg;

    public Text txName;
    public Text txDesc;
    public Text txType;

    public Text codeCoin;
    public Text codeEnergy;
    public Text codeGame;

    private bool isInit = false;

    public void Init()
    {
        isInit = true;
    }


    public void OnEnable()
    {
        EventCenter.Instance.AddEventListener("ShowTip", ShowTipEvent);
        EventCenter.Instance.AddEventListener("HideTip", HideTipEvent);

    }

    public void OnDisable()
    {
        EventCenter.Instance.AddEventListener("ShowTip", ShowTipEvent);
        EventCenter.Instance.AddEventListener("HideTip", HideTipEvent);
    }

    private void HideTipEvent(object arg0)
    {
        if (isInit)
        {
            objBg.SetActive(false);

        }
    }

    private void ShowTipEvent(object arg0)
    {
        if (!isInit)
        {
            return;
        }

        ShowTipStruct tipInfo = (ShowTipStruct)arg0;

        if (tipInfo.level == 0)
        {
            txName.text = tipInfo.name;
        }
        else if(tipInfo.level>0)
        {
            txName.text = string.Format("{0}+{1}", tipInfo.name,tipInfo.level);
        }
        else
        {
            txName.text = string.Format("{0}-{1}", tipInfo.name, Mathf.Abs(tipInfo.level));
        }

        txDesc.text = tipInfo.desc;
        txType.text = tipInfo.type;

        codeCoin.text = tipInfo.coin.ToString();
        codeEnergy.text = tipInfo.energy.ToString();
        codeGame.text = tipInfo.task.ToString();

    }



}

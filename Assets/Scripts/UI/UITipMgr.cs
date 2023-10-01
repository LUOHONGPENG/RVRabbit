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

    public string extraSupport;

    public Vector2 pos;

    public ShowTipStruct(string name, int level, string desc, string type,int coin,int energy,int task,string extraSupport,Vector2 pos)
    {
        this.name = name;
        this.level = level;
        this.desc = desc;
        this.type = type;
        this.coin = coin;
        this.energy = energy;
        this.task = task;
        this.extraSupport = extraSupport;
        this.pos = pos;
    }
}



public class UITipMgr : MonoBehaviour
{
    public GameObject objBg;

    public Text txName;
    public Text txDesc;
    public Text txExtraDesc;
    public Text txType;

    public Image imgCoin;
    public Image imgEnergy;
    public Image imgGame;

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
        EventCenter.Instance.RemoveEventListener("ShowTip", ShowTipEvent);
        EventCenter.Instance.RemoveEventListener("HideTip", HideTipEvent);
    }

    private void HideTipEvent(object arg0)
    {
        if (!isInit)
        {
            return;
        }

        objBg.SetActive(false);

    }

    private void ShowTipEvent(object arg0)
    {
        if (!isInit)
        {
            return;
        }

        objBg.SetActive(true);

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

        if (tipInfo.coin == 0)
        {
            imgCoin.gameObject.SetActive(false);
        }
        else
        {
            imgCoin.gameObject.SetActive(true);
            if (tipInfo.coin > 0)
            {
                codeCoin.text = "+" + tipInfo.coin.ToString();
            }
            else
            {
                codeCoin.text = tipInfo.coin.ToString();
            }
        }

        if (tipInfo.energy == 0)
        {
            imgEnergy.gameObject.SetActive(false);
        }
        else
        {
            imgEnergy.gameObject.SetActive(true);
            if (tipInfo.energy > 0)
            {
                codeEnergy.text = "+" + tipInfo.energy.ToString();
            }
            else
            {
                codeEnergy.text = tipInfo.energy.ToString();
            }
        }

        if (tipInfo.task == 0)
        {
            imgGame.gameObject.SetActive(false);
        }
        else
        {
            imgGame.gameObject.SetActive(true);
            if (tipInfo.task > 0)
            {
                codeGame.text = "+" + tipInfo.task.ToString();
            }
            else
            {
                codeGame.text = tipInfo.task.ToString();
            }
        }

        txExtraDesc.text = tipInfo.extraSupport;

        objBg.transform.position = tipInfo.pos;
    }



}

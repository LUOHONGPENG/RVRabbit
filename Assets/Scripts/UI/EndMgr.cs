using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMgr : MonoBehaviour
{
    public GameObject objPopup;

    public Image imgEnding;
    public Text codeEnd;


    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener("Ending", EndEvent);
    }

    private void EndEvent(object arg0)
    {
        int num = (int)arg0;

        objPopup.SetActive(true);

        switch (num)
        {
            case 0:
                codeEnd.text = "You are arrested. Evil Rabbit!";
                break;
            case 1:
                codeEnd.text = "Your indie game become your tool to spread your cult! ";
                break;
            case 2:
                codeEnd.text = "You sell your body to finish your indie game!";
                break;
            case 3:
                codeEnd.text = "You finish your indie game but hurt your friend!";
                break;
            case 4:
                codeEnd.text = "You finish your indie game!";
                break;
        }

    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("Ending", EndEvent);
    }
}

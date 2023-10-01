using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr : MonoSingleton<GameMgr>
{
    public Camera mapCamera;
    public RoomMgr roomMgr;
    public UIMgr uiMgr;

    public void Start()
    {
        InitExcel();

        StartCoroutine(InputMgr.Instance.IE_Init());

        roomMgr.Init();

        uiMgr.Init();
    }

    public InteractType interactType = InteractType.Move;


    public void InvokeAction(int typeID)
    {
        StartCoroutine(IE_Action(typeID));
        //Check End
    }

    public IEnumerator IE_Action(int typeID)
    {
        interactType = InteractType.Wait;
        yield return new WaitForSeconds(2f);
        CheckExtraEffect(typeID);
        interactType = InteractType.Action;
    }

    public void CheckExtraEffect(int typeID)
    {

    }
}



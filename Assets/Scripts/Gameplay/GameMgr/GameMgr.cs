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
    public ComsumerType consumerType = ComsumerType.None;

    public void InvokeAction(int typeID)
    {
        StartCoroutine(IE_Action(typeID));
        //Check End
    }

    public IEnumerator IE_Action(int typeID)
    {
        interactType = InteractType.Wait;
        yield return new WaitForSeconds(0.5f);
        roomMgr.HideCharacter();
        yield return new WaitForSeconds(0.5f);
        //Character
        int ran = Random.Range(0, 3);
        consumerType = (ComsumerType)ran;
        switch (consumerType)
        {
            case ComsumerType.Cow:
                roomMgr.ShowCow();
                break;
            case ComsumerType.Chicken:
                roomMgr.ShowChicken();
                break;
        }


        CheckExtraEffect(typeID);
        interactType = InteractType.Action;
    }

    public void CheckExtraEffect(int typeID)
    {

    }
}



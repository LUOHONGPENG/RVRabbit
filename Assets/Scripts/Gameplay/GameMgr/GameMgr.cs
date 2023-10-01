using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr : MonoSingleton<GameMgr>
{
    public Camera mapCamera;
    public RoomMgr roomMgr;
    public UIMgr uiMgr;
    public SoundMgr soundMgr;
    public void Start()
    {
        InitExcel();

        StartCoroutine(InputMgr.Instance.IE_Init());

        roomMgr.Init();

        uiMgr.Init();
        soundMgr.Init();

        countCoin = 10;
        countEnergy = 10;
        interactType = InteractType.Shop;
    }

    public InteractType interactType = InteractType.Shop;
    public ComsumerType consumerType = ComsumerType.None;

    public void InvokeAction(int typeID)
    {
        StartCoroutine(IE_Action(typeID));
        //Check End
    }

    public IEnumerator IE_Action(int typeID)
    {
        interactType = InteractType.Wait;
        CheckSound(typeID);
        yield return new WaitForSeconds(0.5f);
        roomMgr.HideCharacter();
        yield return new WaitForSeconds(0.5f);
        //Character
        int ran = Random.Range(0, 5);
        if (ran < 2)
        {
            consumerType = ComsumerType.Cow;
        }
        else if(ran<4)
        {
            consumerType = ComsumerType.Chicken;

        }
        else
        {
            consumerType = ComsumerType.None;
        }

        switch (consumerType)
        {
            case ComsumerType.Cow:
                roomMgr.ShowCow();
                PublicTool.PlaySound(SoundType.Hello2);
                break;
            case ComsumerType.Chicken:
                roomMgr.ShowChicken();
                PublicTool.PlaySound(SoundType.Hello1);

                break;
        }


        CheckExtraEffect(typeID);
        if (countTask >= 50 || countSin>=10)
        {
            InvokeEnding();
            yield break ;
        }
        interactType = InteractType.Action;
    }

    public void CheckSound(int typeID)
    {
        switch (typeID)
        {
            case 2004:
                if (consumerType == ComsumerType.Cow)
                {
                    PublicTool.PlaySound(SoundType.Cow);
                }
                else
                {
                    PublicTool.PlaySound(SoundType.Chicken);
                }
                break;
            case 9001:
                PublicTool.PlaySound(SoundType.Sex);
                break;
            case 9002:
                if (consumerType == ComsumerType.Cow)
                {
                    PublicTool.PlaySound(SoundType.Cow);
                }
                else
                {
                    PublicTool.PlaySound(SoundType.Chicken);
                }
                break;
            default:

                PublicTool.PlaySound(SoundType.Bought);
                break;
        }
    }

    public void CheckExtraEffect(int typeID)
    {
        switch (typeID)
        {
            case 2004:
                countSin += 2;
                countKnife++;
                break;
            case 9001:
                countSin += 1;
                countSex++;
                break;
            case 9002:
                countSin += 5;
                countSacrifice++;
                break;
        }
    }

    public void InvokeEnding()
    {
        if (countSin >= 10)
        {
            EventCenter.Instance.EventTrigger("Ending", 0);

            //You are arrested.
        }
        else if (countSacrifice > 0)
        {
            EventCenter.Instance.EventTrigger("Ending", 1);

        }
        else if (countSex > 0)
        {
            EventCenter.Instance.EventTrigger("Ending", 2);
        }
        else if (countKnife > 0)
        {
            EventCenter.Instance.EventTrigger("Ending", 3);
        }
        else
        {
            EventCenter.Instance.EventTrigger("Ending", 4);
        }

    }
}



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
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMgr : MonoSingleton<GameMgr>
{
    public RoomMgr roomMgr;

    public void Start()
    {
        roomMgr.Init();
    }
}

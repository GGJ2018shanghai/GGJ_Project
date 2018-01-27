using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

public class GameInstance : SystemBase<GameInstance, GameInstanceData>
{
    [RuntimeInitializeOnLoadMethod]
    static void InitializeGameInstance()
    {
        if (Instance.Data.enableInputManager)
        {
            Debug.Log("[On Initialize] " + InputManager.Instance.GetType().ToString());
            InputManager.Instance.transform.SetParent(Instance.transform);
        }
        Debug.Log("[On Initialize] " + GameLogicManager.Instance.GetType().ToString());
        GameLogicManager.Instance.transform.SetParent(Instance.transform);
    }
}
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "HNA/Game Instance")]
public class GameInstanceData : ScriptableObject
{
    public bool enableInputManager = true;

    static public void SendMessage(string msgType, object data, float delay = 0.0f)
    {
        MessageDispatcher.SendMessage(GameInstance.Instance, msgType, data, delay);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Jam/Game Logic Data")]
public class GameLogicManagerData : ScriptableObject
{
    public float hp;
    public float size;

    //速度相关的所有变量
    public float speed;
    public float topDashTime;
    public float botDashTime;
}

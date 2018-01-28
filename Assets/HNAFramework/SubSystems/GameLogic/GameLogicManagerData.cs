using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Jam/Game Logic Data")]
public class GameLogicManagerData : ScriptableObject
{
    public float hp;
    public int ap;
    public float size;


    public float speed;
    public float botDashTime;
    public float topDashTime;
}

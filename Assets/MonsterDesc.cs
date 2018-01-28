using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDesc : MonoBehaviour {

    void Awake()
    {
        DestroiedInfoDic = new Dictionary<string, int>();
        DestroiedInfoDic.Add(name, 0);
    }

    public void GetKilled()
    {
        DestroiedInfoDic[name]++;
    }

    public string name = "test_monster";
    public float HpModify = 0;
    public int ap = 0;

    static public void Reset()
    {
        if (DestroiedInfoDic != null) DestroiedInfoDic.Clear();
        DestroiedInfoDic = new Dictionary<string, int>();
    }
    static public Dictionary<string, int> DestroiedInfoDic;
}

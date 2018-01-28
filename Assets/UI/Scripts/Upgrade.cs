using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour {
    
    public string upgradeName;
    public int lv;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickUpgrade(bool isOn)
    {
        int needMoney = 100 * lv;
        if (UIManager.instance.CostMoney(needMoney))
        {
            lv++;
            UIManager.instance.UpdateLv(gameObject);
            switch (upgradeName)
            {
                case "血量":
                    
                    break;
                case "速度":

                    break;
                case "回血":

                    break;
                case "掉落":

                    break;
            }
        }
        else
        {
            UIManager.instance.ShakeMoneyText();
        }
        
    }

    
}

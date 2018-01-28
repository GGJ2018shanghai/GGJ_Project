using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour {

    public GameObject sliderObj, moneyTextObj;


	// Use this for initialization
	void Start () {
        //Screen.SetResolution(1920, 1080, true); 	
	}
	
	// Update is called once per frame
	void Update () {
        sliderObj.GetComponent<Slider>().value = 0.5f;
        moneyTextObj.GetComponent<Text>().text = "111";
	}
}

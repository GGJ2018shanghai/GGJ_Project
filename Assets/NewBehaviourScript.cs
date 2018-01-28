using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour {
    public DG.Tweening.DOTweenAnimation test;
    // Use this for initialization
    void Start () {
        transform.DOLocalMoveX(2, 1);
	}
	
	// Update is called once per frame
	void Update () {
	}

    

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterFace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public interface IState {
    void Execute(AIController ac);
}
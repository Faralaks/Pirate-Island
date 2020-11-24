using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_for_all : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        print("Name: " + name + "  " + "Tag: " + tag);

    }

}

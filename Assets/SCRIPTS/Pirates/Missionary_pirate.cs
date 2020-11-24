using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missionary_pirate : MonoBehaviour {
    public static bool wild = false;


    void Start () {
        

    }

	void Update () {

    }
	

	void OnMouseDown () {
        if (Map.line[Map.steps % 4] == tag) {
            Map.child = transform;
            Map.select = 1;


        }
				







	}
		

		
		
	

}

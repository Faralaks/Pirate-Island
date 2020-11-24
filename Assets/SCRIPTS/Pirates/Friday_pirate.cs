using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friday_pirate : MonoBehaviour {


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

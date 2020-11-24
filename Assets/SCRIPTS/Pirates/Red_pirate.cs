using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_pirate : MonoBehaviour {

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

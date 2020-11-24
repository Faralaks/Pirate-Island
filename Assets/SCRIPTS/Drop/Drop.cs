using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {


    void Start() {

    }

    void Update() {

    }

    void OnMouseDown()  {
        if (Map.EarthQuake == false) {
            Map.IsDropSelect = true;
            Map.DropSelect = gameObject;
        }

    }
}

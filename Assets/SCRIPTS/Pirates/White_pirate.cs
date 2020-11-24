using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White_pirate : MonoBehaviour {
    GameObject NewShowObj;

    void Start () {

    }

    void Update () {

    }
	

	void OnMouseDown () {
        if (Map.line[Map.steps % 4] == tag && Map.EarthQuake == false || true) {
            Map.child = transform;
            Map.select = 1;
            if ((transform.childCount == 0 || Map.DropSelect.name == "Car" || transform.Find("Car") != null) && (Map.IsDropSelect && Map.DropSelect.transform.parent.name == transform.parent.name)) {
                Map.IsDropSelect = false;

                Map.DropSelect.transform.SetParent(transform);
                //NewShowObj = Instantiate(Map.ShowObj[Map.DropSelect.name]);
                //switch (transform.childCount) {
                //    case 1:
                //        NewShowObj.transform.SetParent(transform);
                //        NewShowObj.transform.localPosition = new Vector3(0f, 3f, 0f);
                //        break;
                //}

                // NewShowObj.transf

            }

        }
				
        
	}
		
}

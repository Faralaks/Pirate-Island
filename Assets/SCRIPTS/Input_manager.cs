using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_manager : MonoBehaviour {

    float[] MainCamPos = { 14f, 109f, 1f };
    public bool CamControl;
    public float[] MainCamSpeed;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(MainCamPos[0], MainCamPos[1], MainCamPos[2]);
    }
	
	// Update is called once per frame
	void Update () {
        if (CamControl) {
            switch (Map.steps % 4)  {
                case 0:
                    if (MainCamPos[0] < -2) { MainCamPos[0] = -2; }
                    if (MainCamPos[0] > 31) { MainCamPos[0] = 31; }
                    if (MainCamPos[2] < -17) { MainCamPos[2] = -17; }
                    if (MainCamPos[2] > 28) { MainCamPos[2] = 28; }
                    if (MainCamPos[1] < 102) { MainCamPos[1] = 102; }
                    if (MainCamPos[1] > 114) { MainCamPos[1] = 114; }
                    break;

            }


        }



        if (Input.GetAxis("HorizontalX") != 0 || Input.GetAxis("Zoom") != 0 || Input.GetAxis("HorizontalZ") != 0) {
            MainCamPos[0] = MainCamPos[0] + MainCamSpeed[0] * Input.GetAxis("HorizontalX");
            MainCamPos[1] = MainCamPos[1] + MainCamSpeed[2] * Input.GetAxis("Zoom");
            MainCamPos[2] = MainCamPos[2] + MainCamSpeed[1] * Input.GetAxis("HorizontalZ");

            transform.position = new Vector3(MainCamPos[0], MainCamPos[1], MainCamPos[2]);

            

        }


    }
}

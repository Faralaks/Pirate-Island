using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public static Dictionary<string, string[]> PirateOnCells = new Dictionary<string, string[]>(3);
    public static Dictionary<string, string> PirateCanGo = new Dictionary<string, string>();
    public static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    public static Dictionary<string, Vector3> ShipStayPoints = new Dictionary<string, Vector3>();
    public static Dictionary<string, GameObject> DropObj = new Dictionary<string, GameObject>();
    public static Dictionary<string, GameObject> ShowObj = new Dictionary<string, GameObject>();


    string[] cells = {
        "water1", "balloon1", "balloon1", "ben1", "biggold1", "boat1", "bottle1", "bottle1", "bottle1", "bottle2", "water1", "bottle2", "bottle3",
        "cannibal1", "cave1", "cave2", "cave3", "cave4", "crocodile1", "crocodile1", "crocodile1", "crocodile1", "deafthickets1", "deafthickets1",
        "deafthickets1", "desert1", "desert1", "desert1", "desert1", "drug1", "drug1", "missionary1", "fortwithgirl1", "fort1", "fort1",
        "friday1", "gold1", "gold1", "gold1", "gold1", "gold1", "gold2", "gold2", "gold2", "gold2", "gold2",
        "gold3", "gold3", "gold3", "gold4", "gold4", "gold5", "horses1", "horses1",
        "land1", "land3", "plane1", "land1", "land2", "earthquake1",
        "jungle1", "jungle1", "jungle1",
        "jungle1", "jungle1", "land2", "land2", "land2", "land2", "kernels1", "kernels1", "lake1", "lake1", "lake1", "lake1", "lake1", "lake1", "land1",

        "land3", "land3", "land4", "land4", "land4", "land4", "lighthouse1", "land1", "mountains1", "mountains1", "pitfall1",
        "pitfall1", "pitfall1",  "rum1", "rum1", "rum1", "rum1", "swamp1", "wheelbarrow1", "",
        "", "", "", "", "", "", "", "", "", "", "",
        "", "water1", "", "", "",  "arrow6", "arrow6", "arrow6", "arrow7", "arrow7", "arrow7", "water1"
    };

    public Sprite TempCloseSprite; 
    public static Sprite CloseSprite;
    System.Random rnd = new System.Random();
    string[,] map = new string[15, 15];
    string temp;
    int from;
    int to;
    int[] WaterPlaces = new int[] { 0, 1, 13, 14 };
    int count1;
    int count2;
    public int RandomCount;
    public static int steps = 0;
    public static int a = 0;
    public static int select = 0;
    public static bool IsDropSelect = false;
    public static GameObject DropSelect;

    public static string cell;
    public static Transform child;
    public static bool plane = false;
    public static bool LightHouse = false;
    public static bool EarthQuake = false;
    public static string[] line = new string[] { "White", "Red", "White", "Red" };//"Black", "Yellow" };
    public static int[] MoveVector = new int[2];

    void Start() {
        CloseSprite = TempCloseSprite;
        child = GameObject.Find("White_pirate1").transform;

        ShipStayPoints.Add("1", new Vector3(-0.7f, -0.7f, -0.6f));
        ShipStayPoints.Add("2", new Vector3(-0.7f, 0.7f, -0.6f));
        ShipStayPoints.Add("3", new Vector3(0.7f, 0.7f, -0.6f));
        ShipStayPoints.Add("4", new Vector3(-0.7f, 0f, -0.6f));
        ShipStayPoints.Add("5", new Vector3(0f, 0.7f, -0.6f));

        // --------- White
        PirateOnCells.Add("White_pirate1", new string[] { "7_1", "1" });
        PirateOnCells.Add("White_pirate2", new string[] { "7_1", "2" });
        PirateOnCells.Add("White_pirate3", new string[] { "7_1", "3" });
        PirateCanGo.Add("White_pirate1", "7_2");
        PirateCanGo.Add("White_pirate2", "7_2");
        PirateCanGo.Add("White_pirate3", "7_2");
        // --------- Red
        PirateOnCells.Add("Red_pirate1", new string[] { "13_7", "1" });
        PirateOnCells.Add("Red_pirate2", new string[] { "13_7", "2" });
        PirateOnCells.Add("Red_pirate3", new string[] { "13_7", "3" });
        PirateCanGo.Add("Red_pirate1", "12_7");
        PirateCanGo.Add("Red_pirate2", "12_7");
        PirateCanGo.Add("Red_pirate3", "12_7");
        // --------- Black
        PirateOnCells.Add("Black_pirate1", new string[] { "7_13", "1" });
        PirateOnCells.Add("Black_pirate2", new string[] { "7_13", "2" });
        PirateOnCells.Add("Black_pirate3", new string[] { "7_13", "3" });
        PirateCanGo.Add("Black_pirate1", "7_12");
        PirateCanGo.Add("Black_pirate2", "7_12");
        PirateCanGo.Add("Black_pirate3", "7_12");
        // --------- Yellow
        PirateOnCells.Add("Yellow_pirate1", new string[] { "1_7", "1" });
        PirateOnCells.Add("Yellow_pirate2", new string[] { "1_7", "2" });
        PirateOnCells.Add("Yellow_pirate3", new string[] { "1_7", "3" });
        PirateCanGo.Add("Yellow_pirate1","2_7");
        PirateCanGo.Add("Yellow_pirate2", "2_7");
        PirateCanGo.Add("Yellow_pirate3", "2_7");
        // --------- Другие
        PirateOnCells.Add("Ben", new string[] { "999_999", "4" });
        PirateOnCells.Add("Missionary", new string[] { "999_999", "5" });
        PirateOnCells.Add("Friday", new string[] { "999_999", "5" });
        PirateCanGo.Add("Ben","999_999");
        PirateCanGo.Add("Missionary", "999_999");
        PirateCanGo.Add("Friday", "999_999");
        // --------- Модельки объектов
        DropObj.Add("big", GameObject.Find("big"));
        DropObj.Add("car", GameObject.Find("car"));
        DropObj.Add("boat", GameObject.Find("boat"));
        DropObj.Add("gold", GameObject.Find("gold"));
        DropObj.Add("kernel", GameObject.Find("kernel"));
        // --------- Всплывающие модельки обхектов
        ShowObj.Add("big", GameObject.Find("ShowBig"));
        ShowObj.Add("car", GameObject.Find("ShowCar"));
        ShowObj.Add("boat", GameObject.Find("ShowBoat"));
        ShowObj.Add("gold", GameObject.Find("ShowGold"));
        ShowObj.Add("kernel", GameObject.Find("ShowKernel"));




        foreach (int i in WaterPlaces) {
            for (int ii = 0; ii < 15; ii++) {
                map[i, ii] = "water1";
            }
        }
        foreach (int ii in WaterPlaces) {
            for (int i = 0; i < 15; i++) {
                map[i, ii] = "water1";
            }
        }


        for (int i = 96; i < 98; i++) { cells[i] = "gun" + Convert.ToString(rnd.Next(1, 4)); }
        for (int i = 98; i < 101; i++) { cells[i] = "arrow1" + Convert.ToString(rnd.Next(1, 4)); }
        for (int i = 101; i < 104; i++) { cells[i] = "arrow2" + Convert.ToString(rnd.Next(1, 4)); }
        for (int i = 104; i < 107; i++) { cells[i] = "arrow3" + Convert.ToString(rnd.Next(1, 2)); }
        for (int i = 107; i < 110; i++) { cells[i] = "arrow4" + Convert.ToString(rnd.Next(1, 2)); }
        for (int i = 111; i < 114; i++) { cells[i] = "arrow5" + Convert.ToString(rnd.Next(1, 4)); }



        for (int i = 0; i < RandomCount; i++) {
            from = rnd.Next(0, cells.Length);
            to = rnd.Next(0, cells.Length);
            if (cells[from] == "water1" || cells[to] == "water1") { continue; }
            temp = cells[to];
            cells[to] = cells[from];
            cells[from] = temp;
        }

        count1 = 2;
        count2 = 2;
        foreach (string i in cells) {
            map[count2, count1] = i;
            count1++;
            if (count1 == 13) {count1 = 2;  count2++; }
        }

        map[7,1] = "whiteship1";
        map[13,7] = "redship1";
        map[7, 13] = "blackship1";
        map[1, 7] = "yellowship1";


        count1 = 0;
        count2 = 0;
        foreach (string i in map) {
            GameObject land = Instantiate(GameObject.Find(i));
            land.transform.position = new Vector3(count2*2, 100, count1*2);

            land.name = Convert.ToString(count2) + "_" + Convert.ToString(count1);
            sprites.Add(land.name, land.GetComponent<SpriteRenderer>().sprite);
            // land.AddComponent<Script_for_all>();
            count1++;
            if (count1 == 15) {count1 = 0;  count2++; }

        }







    }
	
	// Update is called once per frame
	void Update () {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friday : MonoBehaviour {

    bool closed = true;
    Transform pirate;
    Vector3 TempVector;
    string TempName;
    Sprite OpenSprite;
    public static Transform fri;
    int[] CellNum;
    Dictionary<string, Vector3> StayPoints = new Dictionary<string, Vector3>(9);


    void Start()  {
        if (name.IndexOf("_") != -1) {
            // Добавление координат в которые должен ставится пират
            StayPoints.Add("1", new Vector3(-0.7f, -0.7f, -0.6f));
            StayPoints.Add("2", new Vector3(-0.7f, 0.7f, -0.6f));
            StayPoints.Add("3", new Vector3(0.7f, 0.7f, -0.6f));
            StayPoints.Add("4", new Vector3(-0.7f, 0f, -0.6f));
            StayPoints.Add("5", new Vector3(0f, 0.7f, -0.6f));
            OpenSprite = GetComponent<SpriteRenderer>().sprite;
            GetComponent<SpriteRenderer>().sprite = Map.CloseSprite;
            fri = GameObject.Find("Friday").transform;

        }
    }

    void OnMouseDown() {
        // Проверка на возможность пойти на эту клетку
        if (Map.EarthQuake == false && Map.LightHouse == false && (Map.child.childCount > 0 && closed) == false && (Map.plane || Map.PirateCanGo[Map.child.name].IndexOf(name) != -1) && Map.select == 1) {
            int ChildCount = Map.child.childCount;

            // Действия если клетка была закрыта
            if (closed) {
                closed = false;
                GetComponent<SpriteRenderer>().sprite = OpenSprite;
                fri.SetParent(transform);
                fri.localPosition = StayPoints[Map.PirateOnCells[fri.name][1]];
                Map.PirateOnCells[fri.name][0] = name;
                CellNum = new int[] { System.Convert.ToInt32(name.Split(new char[] { '_' })[0]), System.Convert.ToInt32(name.Split(new char[] { '_' })[1]) };
                Map.PirateCanGo[fri.name] = string.Join("+", new string[] { string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1]) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1] + 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0]), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0]), System.Convert.ToString(CellNum[1] + 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1]) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1] + 1) }),
                });
                fri.tag = Map.child.tag;

            }


            // Проверка на наличие других пиратов
            foreach (string i in Map.PirateOnCells.Keys) {
                // Убийство врагов
                if (i.IndexOf(Map.child.tag) != -1) { continue; }
                if (Map.PirateOnCells[i][0] == name) {
                    pirate = GameObject.Find(i).transform;
                    switch (i) {
                        case "Ben":
                            if (Ben.BenGun.tag == Map.child.tag) { break; }
                            else { goto default; }
                        case "Friday":
                            if (Map.child.name == "Missionary" && pirate.name == "Friday") {
                                GameObject.Find("Friday").SetActive(false);
                                GameObject.Find("Missionary").SetActive(false);
                                break;
                            }
                            Friday.fri.tag = Map.child.tag;
                            break;
                        case "Missionary":
                            if (Map.child.name == "Friday" && pirate.name == "Missionary")  {
                                GameObject.Find("Friday").SetActive(false);
                                GameObject.Find("Missionary").SetActive(false);
                                break;
                            }
                            if (Missionary_pirate.wild) { goto default; }
                            else { break; }
                        default:
                            for (int ii = 0; ii != ChildCount; ii++) { // Возврат всего дропа обратн
                                Map.child.GetChild(ii).SetParent(GameObject.Find(Map.PirateOnCells[Map.child.name][0]).transform);
                            }
                            if (Map.child.name == "Friday") { Friday.fri.tag = pirate.tag; break; }
                            if (Map.child.name == "Missionary" && Missionary_pirate.wild == false) { break; }
                            pirate.SetParent(GameObject.FindGameObjectWithTag(pirate.tag + "_ship").transform);
                            pirate.localPosition = Map.ShipStayPoints[Map.PirateOnCells[pirate.name][1]];
                            break;
                    }

                   
                }
            }

            // Перерасположение дропа на клетке
            ChildCount = Map.child.childCount;
            for (int i = 0; i != ChildCount; i++) {
                Map.child.GetChild(i).SetParent(transform);
            }
            for (int i = 0; i != transform.childCount; i++) {
                if (transform.GetChild(i).tag == "Drop") {
                    transform.GetChild(i).localPosition = new Vector3(0f, 0f, -0.05f * i - 0.1750005f);
                }
            }




            // Перенос Пирата на эту клетку
            Map.child.SetParent(transform);
            Map.child.localPosition = StayPoints[Map.PirateOnCells[Map.child.name][1]];
            CellNum = new int[] { System.Convert.ToInt32(name.Split(new char[] { '_' })[0]), System.Convert.ToInt32(name.Split(new char[] { '_' })[1]) };
            // Расчет возможных ходов с этой клетки
            Map.PirateOnCells[Map.child.name][0] = name;
            Map.PirateCanGo[Map.child.name] = string.Join("+", new string[] { string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1]) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1] + 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0]), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0]), System.Convert.ToString(CellNum[1] + 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1]) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1] + 1) }),
            });



            Map.steps++;
            Map.select = 0;
            // Удаление всех показанных над головой пирата элементов
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Show")) {
                i.SetActive(false);
            }

        }
        // Если пират смотрит на клетку с мояка
        if (Map.LightHouse && closed && Map.EarthQuake == false) {
            closed = false;
            GetComponent<SpriteRenderer>().sprite = OpenSprite;
            // LightHouse.OpenCount++;
            // if (LightHouse.OpenCount > 5){Map.LightHouse == false}
            fri.SetParent(transform);
            fri.localPosition = StayPoints[Map.PirateOnCells[fri.name][1]];
            Map.PirateOnCells[fri.name][0] = name;
            CellNum = new int[] { System.Convert.ToInt32(name.Split(new char[] { '_' })[0]), System.Convert.ToInt32(name.Split(new char[] { '_' })[1]) };
            Map.PirateCanGo[fri.name] = string.Join("+", new string[] { string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1]) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1] + 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0]), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0]), System.Convert.ToString(CellNum[1] + 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1]) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1] + 1) }),
            });
        }
        // Землетрясение
        if (Map.EarthQuake && transform.childCount == 0)  {
            if (EarthQuake.Land_Selected == false) {
                EarthQuake.Land_Selected = true;
                EarthQuake.SelectObj = transform;
                transform.position = new Vector3(transform.position[0], transform.position[1] + 1f, transform.position[2]);
            }
            else  {
                EarthQuake.Land_Selected = false;
                if (EarthQuake.SelectObj == transform) {
                    transform.position = new Vector3(transform.position[0], transform.position[1] - 1f, transform.position[2]);
                }
                else {
                    TempName = EarthQuake.SelectObj.name;
                    TempVector = new Vector3(EarthQuake.SelectObj.position[0], EarthQuake.SelectObj.position[1] - 1f, EarthQuake.SelectObj.position[2]);
                    EarthQuake.SelectObj.position = transform.position;
                    EarthQuake.SelectObj.name = name;
                    transform.position = TempVector;
                    name = TempName;
                    Map.EarthQuake = false;

                }
            }
        }

    }

}

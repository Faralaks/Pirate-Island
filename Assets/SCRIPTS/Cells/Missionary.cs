using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missionary : MonoBehaviour {

    // Закрыта ли клетка
    bool closed = true;
    // Масив названий клеток куда может пойти игрок
    string[] CanGo;
    // Переменная для пирата которого убиваю на этой клетке
    Transform pirate;
    // Словарь с координатами куда ставить пирата
    Dictionary<string, Vector3> StayPoints = new Dictionary<string, Vector3>();
    // Обхект Бена Гана
    public static Transform mis;


    void Start() {
        if (name.IndexOf("_") != -1) {
            // Добавление координат в которые должен ставится пират
            StayPoints.Add("1", new Vector3(-0.7f, -0.7f, -0.6f));
            StayPoints.Add("2", new Vector3(-0.7f, 0.7f, -0.6f));
            StayPoints.Add("3", new Vector3(0.7f, 0.7f, -0.6f));
            StayPoints.Add("4", new Vector3(-0.7f, 0f, -0.6f));
            StayPoints.Add("5", new Vector3(0f, 0.7f, -0.6f));

            // Расчет возможных ходов с этой клетки
            int[] CellNum = { System.Convert.ToInt32(name.Split(new char[] { '_' })[0]), System.Convert.ToInt32(name.Split(new char[] { '_' })[1]) };
            CanGo = new string[] { string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1]) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] - 1), System.Convert.ToString(CellNum[1] + 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0]), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0]), System.Convert.ToString(CellNum[1] + 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1] - 1) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1]) }),
                    string.Join("_", new string[] { System.Convert.ToString(CellNum[0] + 1), System.Convert.ToString(CellNum[1] + 1) }),
            };
            // Поиск Бена Нага
            mis = GameObject.Find("Missionary").transform;
        }
    }
        

	void Update () {
		
	}

	void OnCollisionEnter () {
        

    }

    void OnCollisionExit() {

    }


    void OnMouseDown() {
        // Проверка на возможность пойти на эту клетку
        if ((closed && Map.child.childCount > 0) == false && (Map.plane || Map.PirateCanGo[Map.child.name].IndexOf(name) != -1) && Map.select == 1) {
            // Действия если клетка была закрыта
            if (closed) {
                closed = false;
                GetComponent<SpriteRenderer>().sprite = Map.sprites[name];
                mis.SetParent(transform);
                // Бен переностится на эту клетку
                mis.localPosition = StayPoints[Map.PirateOnCells[mis.name][1]];
                // Записывается где теперь Бен и куда он может пойти
                Map.PirateOnCells[mis.name][0] = name;
                Map.PirateCanGo[mis.name] = string.Join("+", CanGo);
                // Настройка Бена
                mis.tag = Map.child.tag;

            }

            
            foreach (string i in Map.PirateOnCells.Keys) {
                if (i.IndexOf(Map.child.tag) != -1) { continue; }
                if (Map.PirateOnCells[i][0] == name) {
                    // Действия взависимости от пирата на клетке
                    pirate = GameObject.Find(i).transform;
                    switch (i)  {
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
                            if (Map.child.name == "Friday" && pirate.name == "Missionary") {
                                GameObject.Find("Friday").SetActive(false);
                                GameObject.Find("Missionary").SetActive(false);
                                break;
                            }
                            if (Missionary_pirate.wild) { goto default; }
                            else { break; }
                        default:
                            Map.child.DetachChildren();
                            if (Map.child.name == "Friday") { Friday.fri.tag = pirate.tag; break; }
                            if (Map.child.name == "Missionary" && Missionary_pirate.wild == false) { break; }
                            pirate.SetParent(GameObject.FindGameObjectWithTag(pirate.tag + "_ship").transform);
                            pirate.localPosition = Map.ShipStayPoints[Map.PirateOnCells[pirate.name][1]];
                            break;
                    }
                }
            }


            Map.child.SetParent(transform);
            Map.child.localPosition = StayPoints[Map.PirateOnCells[Map.child.name][1]];
            Map.PirateOnCells[Map.child.name][0] = name;
            Map.PirateCanGo[Map.child.name] = string.Join("+", CanGo);
            Map.steps++;
            Map.select = 0;
        }

    }

}

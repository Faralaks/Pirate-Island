using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_ship : MonoBehaviour {

    string[] CanGo = { "7_2" };
	Transform pirate1;
	Transform pirate2;
	Transform pirate3;
    GameObject ColObject;


	void Start () {
        if (name.IndexOf("_") != -1) {
            tag = "Red_ship";

            pirate1 = GameObject.Find("Red_pirate1").transform;
            pirate2 = GameObject.Find("Red_pirate2").transform;
            pirate3 = GameObject.Find("Red_pirate3").transform;

            pirate1.SetParent(transform);
            pirate2.SetParent(transform);
            pirate3.SetParent(transform);

            pirate1.localPosition = Map.ShipStayPoints["1"];
            pirate2.localPosition = Map.ShipStayPoints["2"];
            pirate3.localPosition = Map.ShipStayPoints["3"];
        }
	}
	
	void Update () {
        

    }

    void OnCollisionEnter(Collision col) {
        ColObject = GameObject.Find(col.contacts[0].otherCollider.name);
        Map.PirateOnCells[ColObject.name][0] = name;
        Map.PirateCanGo[ColObject.name] = string.Join("+", CanGo);

    }

    // Срабатывает при нажатие ЛКМ на эту клетку
    void OnMouseDown() {
        // Проверка на возможность пойти на эту клетку
        if (Map.plane || (Map.select == 1 && Map.PirateCanGo[Map.child.name].IndexOf(name) != -1)) {
            //  Объект пирата становится ребенком этой клетки
            Map.child.SetParent(transform);
            // Пират ставится на клетку согласно значению в словаре с координатами
            Map.child.localPosition = Map.ShipStayPoints[Map.PirateOnCells[Map.child.name][1]];
            // Записывается где теперь пират и куда он может пойти
            //Map.PirateOnCells[Map.child.name][0] = name;
            //Map.PirateCanGo[Map.child.name] = string.Join("+", CanGo);
            // Следующий ход
            Map.steps++;
            // Сброс выделения пирата
            Map.select = 0;
        }

    }


}

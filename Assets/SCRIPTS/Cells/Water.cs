using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    // Масив названий клеток куда может пойти игрок
    string[] CanGo;
    // Словарь с координатами куда ставить пирата
    Dictionary<string, Vector3> StayPoints = new Dictionary<string, Vector3>(9);


    void Start() {
        // Добавление координат в которые должен ставится пират
        StayPoints.Add("1", new Vector3(0.5f, 0.5f, -0.6f));
        StayPoints.Add("2", new Vector3(-0.5f, 0.5f, -0.6f));
        StayPoints.Add("3", new Vector3(-0.5f, -0.5f, -0.6f));
        StayPoints.Add("4", new Vector3(0.5f, -0.5f, -0.6f));

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

    }
        
	

	void Update () {
		
	}

	void OnCollisionEnter () {
        

    }

    void OnCollisionExit() {

    }

    void OnMouseDown() {
        // Срабатывает при нажатие ЛКМ на эту клетку

        // Проверка на возможность пойти на эту клетку
        if ((Map.select == 1 && Map.PirateCanGo[Map.child.name].IndexOf(name) != -1)) {

            
            //  объект пирата становится ребенком этой клетки
            Map.child.SetParent(transform);
            // Пират ставится на клетку согласно значению в словаре с координатами
            Map.child.localPosition = StayPoints[Map.PirateOnCells[Map.child.name][1]];
            // Записывается где теперь пират и куда он может пойти
            Map.PirateOnCells[Map.child.name][0] = name;
            Map.PirateCanGo[Map.child.name] = string.Join("+", CanGo);
            // Следующий ход
            Map.steps++;
            // Сброс выделения пирата
            Map.select = 0;
        }

    }

}

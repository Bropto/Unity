using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItemDrop : MonoBehaviour
{
    public int minSpawn = 0;
    public int maxSpawn = 2;
    public GameObject[] MonsterItem;
    Transform monTr;
    void Awake()
    {
        monTr = GetComponent<Transform>();
    }

    public void MakeItem()
    {
        for(int a = 0; a < Random.Range(minSpawn, maxSpawn); a++)
        {
            int item = Random.Range(0, 6);
            GameObject itemBox = Instantiate(MonsterItem[item]);
            itemBox.transform.position = monTr.transform.position + new Vector3(0, 0.5f, 0);
           
        }
    }
}

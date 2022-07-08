using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public GameObject shieldPrefab;
    public GameObject player;
    public Transform shieldPos;
    GameObject shield;
    public bool shieldbool;

    public bool shieldActive = false;

    void Start()
    {
        shieldPos = GetComponent<Transform>();
    }

    public void useShield()
    {
        for (int i = 0; i < DataController.Instance.sheild_Proj; i++)
        {
            shield = (GameObject)Instantiate(shieldPrefab, shieldPos.position + new Vector3(0, 1, 0), Quaternion.identity);
            DataController.Instance.sheildHp = 1000 * (i+1);
            shield.transform.parent = player.transform;
        }
        shieldbool = true;
    }

}

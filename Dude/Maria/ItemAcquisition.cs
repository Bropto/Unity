using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAcquisition : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");
    }

    private void OnTriggerStay(Collider other) //콜라이더 안에 있으면 계속 실행
    {
        if (other.gameObject.CompareTag("ITEM_SKILL1") || other.gameObject.CompareTag("ITEM_SKILL2")
            || other.gameObject.CompareTag("ITEM_SKILL3") || other.gameObject.CompareTag("ITEM_SKILL4")
            || other.gameObject.CompareTag("ITEM_HP") || other.gameObject.CompareTag("ITEM_MP")
            || other.gameObject.CompareTag("ITEM_KEY"))
        {
            Vector3 pos = other.transform.position;
            other.transform.position = Vector3.Slerp(pos, player.transform.position + Vector3.up, Time.deltaTime * 3);
        }

    }
}

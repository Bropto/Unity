using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeVortexProjectile : MonoBehaviour
//{
//설치기처럼 가만히 있기
//    GameObject player; 
//    Vector3 center;
//    public void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("PLAYER");
//        center = player.transform.position + new Vector3(0, 1, 0);
//    }
//    public void Update()
//    {
//        transform.RotateAround(center, Vector3.up, player.GetComponent<MariaSkill>().BVSpeed * Time.deltaTime); //중심점, 회전축, 속도  
//    }
//}
{
    GameObject player;
    Vector3 offset;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");
        offset = transform.position - player.transform.position;
    }

    public void Update()
    {
        //offset, RotateAround을 계속 계산하며 Object 위치 계속 변경
        transform.position = player.transform.position + offset;
        transform.RotateAround(player.transform.position, Vector3.up, DataController.Instance.BvSpeed * Time.deltaTime); //중심점, 회전축, 속도
        offset = transform.position - player.transform.position;
    }

}

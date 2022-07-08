using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeVortexProjectile : MonoBehaviour
//{
//��ġ��ó�� ������ �ֱ�
//    GameObject player; 
//    Vector3 center;
//    public void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("PLAYER");
//        center = player.transform.position + new Vector3(0, 1, 0);
//    }
//    public void Update()
//    {
//        transform.RotateAround(center, Vector3.up, player.GetComponent<MariaSkill>().BVSpeed * Time.deltaTime); //�߽���, ȸ����, �ӵ�  
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
        //offset, RotateAround�� ��� ����ϸ� Object ��ġ ��� ����
        transform.position = player.transform.position + offset;
        transform.RotateAround(player.transform.position, Vector3.up, DataController.Instance.BvSpeed * Time.deltaTime); //�߽���, ȸ����, �ӵ�
        offset = transform.position - player.transform.position;
    }

}

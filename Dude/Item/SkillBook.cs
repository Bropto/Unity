using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        //AddExplosionForce( Ⱦ����(����) ,���߽�����ġ, ���߹ݰ�, ������(����))
        rb.AddExplosionForce(1000, transform.position, 100f, 500);

    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErikaArrowCtrl : MonoBehaviour
{

    public float damage; //데미지는 입력해야함
    public float force = 600.0f;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);

    }

}

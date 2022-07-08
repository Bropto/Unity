using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCtrl : MonoBehaviour
{

    public float damage = 20.0f;
    public float force = 30f;
    private Rigidbody rb;
    Transform tr;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }
    private void OnEnable()
    {

        rb.velocity = tr.forward * force;
        //rb.AddForce(transform.forward * force);
        //rb.velocity = transform.forward * force;

    }





}

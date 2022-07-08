using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFireBall : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rb;
    public GameObject expEffect;
    public float radius = 10.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("ENEMY"))
        {
            ExpFireBall(coll.gameObject);
        }
    }

    void ExpFireBall(GameObject monster)
    {
        GameObject exp = Instantiate(expEffect, monster.transform.position, Quaternion.identity);
        Destroy(exp, 4.0f);
    }

}

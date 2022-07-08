using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCtrl : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rb;
    public GameObject expEffect;
    public GameObject Fireball;
    public float radius = 10.0f;
    MagicChaCtrl magicCha;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        magicCha = GetComponent<MagicChaCtrl>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.CompareTag("ENEMY"))
        {
            ExpFireBall();
            if (DataController.Instance.FireBall_Proj >= 2)
            {
                for (int i = 0; i < DataController.Instance.FireBall_Proj; i++)
                {
                    Quaternion rot = Quaternion.Euler(new Vector3(0, 90 * i, 0));
                    Vector3 forwardPos = Vector3.Lerp(transform.forward, transform.right * Mathf.Pow(-1, 1),2 ).normalized;
                    GameObject _InstantFire = Instantiate(Fireball, coll.transform.position + new Vector3(), coll.transform.rotation * rot);
                }
            }
        }
    }

    void ExpFireBall()
    {
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        Destroy(exp, 4.0f);
        IndirectDamage(tr.position);
    }

    void IndirectDamage(Vector3 pos)
    {
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 8);

        foreach(Collider coll in colls)
        {
            rb = coll.GetComponent<Rigidbody>();
            rb.AddExplosionForce(50.0f, pos, radius);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArrowCtrl : MonoBehaviour
{
    

    private Rigidbody rb;

    public GameObject bomb;
    float radius = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    
   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("GROUND") || collision.collider.CompareTag("ENEMY"))
        {
            Bomb_Effect();
            Destroy(this.gameObject);

        }
    }

    void Bomb_Effect()
    {
        GameObject bomb_Effect =  Instantiate(bomb, this.transform.position, Quaternion.identity);
        Destroy(bomb_Effect, 1f);
       
    }

    void IndirectDamage(Vector3 pos)
    {
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 8);

        foreach (Collider coll in colls)
        {
            rb = coll.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.None;

            rb.AddExplosionForce(1500.0f, pos, radius);

        }
    }
}

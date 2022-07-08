using UnityEngine;

public class MeteoCtrl : MonoBehaviour
{

    public GameObject Meteo;
    public GameObject expEffect;
    public Transform firePos;

    public float radius = 10.0f;

    private Transform tr;
    private Rigidbody rb;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("GROUND") || coll.collider.CompareTag("ENEMY"))
        {
            ExpMeteo();   
        }
    }

    void ExpMeteo()
    {
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        Destroy(exp, 4.0f);
        IndirectDamage(tr.position);
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
    void AddMeteo()
    {
        GameObject A_Meteo = Instantiate(Meteo, firePos.position, firePos.rotation);
    }

}

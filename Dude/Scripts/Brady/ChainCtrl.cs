using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChainCtrl : MonoBehaviour
{
    public enum State
    {
        IDLE,
        ATTACK
    }
    public State state = State.IDLE;


    Transform tr;
    Rigidbody rb;
    Transform E_target;
    int mon_num = 0;

    Vector3 center = new Vector3(0, 0.7f, 0);
    public float damage = 20f;
    public float force = 30f;
    float traceDist = 10f;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        
        StartCoroutine(CheckState());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ENEMY"))
        {
            state = State.ATTACK;
        }
    }

    IEnumerator CheckState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            if (E_target != null)
            {
                float distance = Vector3.Distance(tr.position, E_target.position);

                if (distance <= traceDist)
                {
                    state = State.ATTACK;
                }
                else
                {
                    state = State.IDLE;
                }
            }
            else
            {
                state = State.IDLE;
            }
        }
    }

    public void Update()
    {
        if (state == State.ATTACK)
        {
            E_target = GameObject.FindWithTag("ENEMY").GetComponent<Transform>();
            if (E_target != null)
            {
                Collider[] colls = Physics.OverlapSphere(tr.position, 4, LayerMask.GetMask("ENEMY"));

                if (colls.Length > 0)
                {
                    for (int i = 0; i < colls.Length; i++)
                    {
                        mon_num = Random.Range(0, colls.Length);
                        Vector3 targetPos = colls[mon_num].transform.position - tr.transform.position;
                        Quaternion rot = Quaternion.LookRotation(targetPos.normalized);
                        tr.rotation = rot;
                        rb.velocity = tr.forward * force;
                    }
                }
                else
                {
                    state = State.IDLE;
                }
            }
            else
            {
                state = State.IDLE;
                rb.velocity = tr.forward * force;
            }
        }
        else 
        {
            state = State.IDLE;
            rb.velocity = tr.forward * force;
        }
    }
}

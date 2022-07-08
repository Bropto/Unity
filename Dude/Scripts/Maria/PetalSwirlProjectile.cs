using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalSwirlProjectile : MonoBehaviour
{
    enum State
    {
        IDLE, ATTACK, RETURN
    }

    State state = State.IDLE;
    GameObject player;
    int layerEnemy;
    Rigidbody rb;

    float radius = 1;
    float runningTime = 0;
    Vector3 newPos;

    int i;
    float j;

    bool isReturn = false;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");
        rb = GetComponent<Rigidbody>();
        layerEnemy = 1 << LayerMask.NameToLayer("ENEMY");
        i = Random.Range(0, 2);
        j = Random.Range(0f, 180f);


        StartCoroutine(CheckState());
    }

    public void Update()
    {   
        if (state == State.IDLE)
        {
            //�÷��̾� �߽� ������1 ���˵� ���� ����
            runningTime += Time.deltaTime * DataController.Instance.PsRotateSpeed;
            float x = radius * Mathf.Cos(runningTime * Mathf.Deg2Rad);
            float z = radius * Mathf.Sin(runningTime * Mathf.Deg2Rad);

            float a = player.transform.position.x;
            float b = player.transform.position.z;

            newPos = new Vector3(x, 0, z); //���˵� ����
            if (i == 0)
            {   //���˵��� x�� ���� ȸ����ȯ, �÷��̾� rotation.y ����ŭ y�� ���� ȸ����ȯ, �÷��̾ �߽��� �ǵ��� �����̵�
                this.transform.position = RyQ(RxQ(newPos, j), player.transform.rotation.eulerAngles.y) + new Vector3(a, 1, b);
            }

            if (i == 1)
            {   //���˵��� z�� ���� ȸ����ȯ, �÷��̾� rotation.y ����ŭ y�� ���� ȸ����ȯ, �÷��̾ �߽��� �ǵ��� �����̵�
                this.transform.position = RyQ(RzQ(newPos, j), player.transform.rotation.eulerAngles.y) + new Vector3(a, 1, b);
            }
        }


        
        if (state == State.ATTACK)
        {
            //��ǥ OverlapSphere, Layer Ȱ���ؼ� List�� �ޱ�
            Collider[] colls = Physics.OverlapSphere(player.transform.position + Vector3.up,
                DataController.Instance.PsDistance,
                layerEnemy);

            //Rigidbody�� velocity�� �������� �ְ� ������ LookRotation���� �ǽð� �����ؼ� ������ ���ư��� ����ź ����
            if (colls.Length > 0)
            {
                
                rb.velocity = transform.forward * DataController.Instance.PsAttackSpeed * Time.deltaTime;
                Vector3 targetPos = (colls[0].transform.position + Vector3.up) - transform.position;
                transform.rotation = Quaternion.LookRotation(targetPos);

            }
            else
            {
                state = State.RETURN;

            }


        }

        if (state == State.RETURN)
        {
            //Rigidbody�� velocity�� �������� �ְ� ������ LookRotation���� �ǽð� �����ؼ� �÷��̾�� ���ƿ��� ����ź ����

            rb.velocity = transform.forward * DataController.Instance.PsAttackSpeed * Time.deltaTime;
            Vector3 targetPos1 = (player.transform.position + Vector3.up) - transform.position;
            transform.rotation = Quaternion.LookRotation(targetPos1);
            
            if (Vector3.Distance(transform.position, player.transform.position) < 2)
            {
                isReturn = false;
                state = State.IDLE;
            }
        }


    }

    IEnumerator CheckState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            //�÷��̾� �߽�, PsDistance ���������� ���̾� ENEMY Ž�� �ȵǸ�
            if (!Physics.CheckSphere(player.transform.position + Vector3.up,
                DataController.Instance.PsDistance,
                layerEnemy) && !isReturn)
            {
                state = State.IDLE;
            }
                

            //�÷��̾� �߽�, PsDistance ���������� ���̾� ENEMY Ž���Ǹ�
            else if (Physics.CheckSphere(player.transform.position + Vector3.up,
                DataController.Instance.PsDistance,
                layerEnemy) && !isReturn)
            {
                state = State.ATTACK;
            }
                
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ENEMY"))
        {
            isReturn = true;
            state = State.RETURN;
        }
    }


    Vector3 RxQ(Vector3 pos, float degree) //���� X�� ���� ȸ����ȯ
    {
        float x = (pos.x);
        float y = Mathf.Cos(degree * Mathf.Deg2Rad) * pos.y - Mathf.Sin(degree * Mathf.Deg2Rad) * pos.z;
        float z = Mathf.Sin(degree * Mathf.Deg2Rad) * pos.y + Mathf.Cos(degree * Mathf.Deg2Rad) * pos.z;

        return new Vector3(x, y, z);
    }

    Vector3 RyQ(Vector3 pos, float degree) //���� Y�� ���� ȸ����ȯ
    {
        float x = Mathf.Cos(degree * Mathf.Deg2Rad) * pos.x + Mathf.Sin(degree * Mathf.Deg2Rad) * pos.z;
        float y = pos.y;
        float z = -Mathf.Sin(degree * Mathf.Deg2Rad) * pos.x + Mathf.Cos(degree * Mathf.Deg2Rad) * pos.z;

        return new Vector3(x, y, z);
    }

    Vector3 RzQ(Vector3 pos, float degree) //���� Z�� ���� ȸ����ȯ
    {
        float x = Mathf.Cos(degree * Mathf.Deg2Rad) * pos.x - Mathf.Sin(degree * Mathf.Deg2Rad) * pos.y;
        float y = Mathf.Sin(degree * Mathf.Deg2Rad) * pos.x + Mathf.Cos(degree * Mathf.Deg2Rad) * pos.y;
        float z = pos.z;

        return new Vector3(x, y, z);
    }



}

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
            //플레이어 중심 반지름1 원궤도 돌기 세팅
            runningTime += Time.deltaTime * DataController.Instance.PsRotateSpeed;
            float x = radius * Mathf.Cos(runningTime * Mathf.Deg2Rad);
            float z = radius * Mathf.Sin(runningTime * Mathf.Deg2Rad);

            float a = player.transform.position.x;
            float b = player.transform.position.z;

            newPos = new Vector3(x, 0, z); //원궤도 구현
            if (i == 0)
            {   //원궤도를 x축 기준 회전변환, 플레이어 rotation.y 값만큼 y축 기준 회전변환, 플레이어가 중심이 되도록 평행이동
                this.transform.position = RyQ(RxQ(newPos, j), player.transform.rotation.eulerAngles.y) + new Vector3(a, 1, b);
            }

            if (i == 1)
            {   //원궤도를 z축 기준 회전변환, 플레이어 rotation.y 값만큼 y축 기준 회전변환, 플레이어가 중심이 되도록 평행이동
                this.transform.position = RyQ(RzQ(newPos, j), player.transform.rotation.eulerAngles.y) + new Vector3(a, 1, b);
            }
        }


        
        if (state == State.ATTACK)
        {
            //목표 OverlapSphere, Layer 활용해서 List로 받기
            Collider[] colls = Physics.OverlapSphere(player.transform.position + Vector3.up,
                DataController.Instance.PsDistance,
                layerEnemy);

            //Rigidbody의 velocity를 전방으로 주고 방향을 LookRotation으로 실시간 수정해서 적에게 날아가는 유도탄 구현
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
            //Rigidbody의 velocity를 전방으로 주고 방향을 LookRotation으로 실시간 수정해서 플레이어에게 돌아오는 유도탄 구현

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

            //플레이어 중심, PsDistance 반지름으로 레이어 ENEMY 탐지 안되면
            if (!Physics.CheckSphere(player.transform.position + Vector3.up,
                DataController.Instance.PsDistance,
                layerEnemy) && !isReturn)
            {
                state = State.IDLE;
            }
                

            //플레이어 중심, PsDistance 반지름으로 레이어 ENEMY 탐지되면
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


    Vector3 RxQ(Vector3 pos, float degree) //점의 X축 기준 회전변환
    {
        float x = (pos.x);
        float y = Mathf.Cos(degree * Mathf.Deg2Rad) * pos.y - Mathf.Sin(degree * Mathf.Deg2Rad) * pos.z;
        float z = Mathf.Sin(degree * Mathf.Deg2Rad) * pos.y + Mathf.Cos(degree * Mathf.Deg2Rad) * pos.z;

        return new Vector3(x, y, z);
    }

    Vector3 RyQ(Vector3 pos, float degree) //점의 Y축 기준 회전변환
    {
        float x = Mathf.Cos(degree * Mathf.Deg2Rad) * pos.x + Mathf.Sin(degree * Mathf.Deg2Rad) * pos.z;
        float y = pos.y;
        float z = -Mathf.Sin(degree * Mathf.Deg2Rad) * pos.x + Mathf.Cos(degree * Mathf.Deg2Rad) * pos.z;

        return new Vector3(x, y, z);
    }

    Vector3 RzQ(Vector3 pos, float degree) //점의 Z축 기준 회전변환
    {
        float x = Mathf.Cos(degree * Mathf.Deg2Rad) * pos.x - Mathf.Sin(degree * Mathf.Deg2Rad) * pos.y;
        float y = Mathf.Sin(degree * Mathf.Deg2Rad) * pos.x + Mathf.Cos(degree * Mathf.Deg2Rad) * pos.y;
        float z = pos.z;

        return new Vector3(x, y, z);
    }



}

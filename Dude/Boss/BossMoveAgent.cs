using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform playerTr;  //��ǥ ������ = �÷��̾�
    Transform bossTr;

   public float traceSpeed;      //�������� ������ �ӵ� ���� �ؾ��� 
                                        //1�� 10 2�� 12 3�� 14 0427
   // float damping = -1f;
    Vector3 _traceTarget;
    public Vector3 tranceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            agent.acceleration = 16;
            TranceTarget(_traceTarget);
        }
    }


    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null) //�÷��̾� �±װ� ������
        {
            playerTr = player.GetComponent<Transform>();
        }
        agent = GetComponent<NavMeshAgent>();
        bossTr = GetComponent<Transform>();
        //�������� �ٰ������� �����ϴ� �ɼ� ��Ȱ��ȭ
        //agent.autoBraking = false;  //�ڵ��극��ũ ��Ȱ��ȭ
        //agent.updateRotation = false; //�ڵ�ȸ�� ��� ��Ȱ��ȭ    



    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;  //��ø���
    }

    void TranceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
        { return; }

        agent.destination = pos; //�����Ҵ��
        agent.isStopped = false;
    }

    void Update()
    {
        ////�������� ��ǥ�� playertr
        //agent.destination = playerTr.transform.position;


    }
}

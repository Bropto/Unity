using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform playerTr;  //목표 움직임 = 플레이어
    Transform bossTr;

   public float traceSpeed;      //보스마다 움직임 속도 제어 해야함 
                                        //1단 10 2단 12 3단 14 0427
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
        if (player != null) //플레이어 태그가 맞을때
        {
            playerTr = player.GetComponent<Transform>();
        }
        agent = GetComponent<NavMeshAgent>();
        bossTr = GetComponent<Transform>();
        //목적지에 다가갈수록 감속하는 옵션 비활성화
        //agent.autoBraking = false;  //자동브레이크 비활성화
        //agent.updateRotation = false; //자동회전 기능 비활성화    



    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;  //즉시멈춰
    }

    void TranceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
        { return; }

        agent.destination = pos; //추적할대상
        agent.isStopped = false;
    }

    void Update()
    {
        ////움직임의 목표는 playertr
        //agent.destination = playerTr.transform.position;


    }
}

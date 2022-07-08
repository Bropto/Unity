using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAI : MonoBehaviour
{
    public enum Type            //보스의 타입(스테이지 보스인지,앤딩보스인지)
    {
        STAGEBOSS1,
        STAGEBOSS2,
        ENDBOSS
    }
    public Type bossType;

    public enum State
    {
        IDLE,
        TRACE,
        FLEE,
        ATTACK,
        DIE
    }
    //상태를 명시
    
    public State state = State.IDLE; //기본 상태


     Transform playerTr;
     Transform bossTr;

   
    bool isDie = false;
    bool isAttack = false;
    bool isMove = true;

     WaitForSeconds ws;
     BossMoveAgent bsMoveAgent;
     BossItemDrop bsItemDrop;
     Animator anim;




    [Header("보스a=2 b=4 c=5")]
    public int randomAttack;        //공격 패턴 입력 (a=2 b =4 c=5)
    
    readonly int hashMove = Animator.StringToHash("IsMove");

    readonly int hashAttackA = Animator.StringToHash("IsAttackA");
    readonly int hashAttackB = Animator.StringToHash("IsAttackB");
    readonly int hashAttackC = Animator.StringToHash("IsAttackC");

    public readonly int hashDie = Animator.StringToHash("Die");
    public readonly int hashDieIdx = Animator.StringToHash("DieIdx");

  
    public  float attackDistA = 4f; //공격사거리
    public  float traceDistA = 30f; //추적거리 
    [Header("스킬 딜레이")]
    float nextAttack = 1;          // 다음공격
    readonly float attackRate = 3.5f; // 공격 딜레이
    readonly float damping = 10f;   //플레이어 쳐다볼 회전계수



    [Header("스킬A 관련")]
    public float attackDamageA;        //기본공격공격 데미지   보스마다 데미지 바꿔야함! 
                                            //1단 10 2단 20 3단 30으로 임시 0427
    
    public BoxCollider Abs_AAttack_CA;      //보스A공격 콜라이더
    public BoxCollider Bbs_AAttack_CA;      //보스B공격 콜라이더    
    public BoxCollider Cbs_AAttack_CA;      //보스C공격 콜라이더


    [Header("스킬B 관련")]
    public float attackDamageB;        //기본공격공격 데미지   보스마다 데미지 바꿔야함! 
                                       // 2단 20 3단 30으로 임시 0427
    public Transform attackPosB;        //공격 지점
    public GameObject bsBBomb;         //보스 공격용 오브젝트
    ShakeCam shakecam;

    [Header("스킬C 관련")]

    public float attackDamageC = 20f;         //c데미지
                                              //3단 20 임시 0428   
    public BoxCollider Abs_CAttack_CA;      //보스A공격 콜라이더
    public BoxCollider Bbs_CAttack_CA;      //보스B공격 콜라이더
    public BoxCollider Cbs_CAttack_CA;      //보스C공격 콜라이더


    GameObject MobMinimapPoint; //미니맵 아이콘

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null) //플레이어 태그가 맞을때
        {
            playerTr=player.GetComponent<Transform>();
        }

        bossTr = GetComponent<Transform>();
        bsMoveAgent= GetComponent<BossMoveAgent>();
        bsItemDrop= GetComponent<BossItemDrop>();
        shakecam = GameObject.Find("CameraRig").GetComponent<ShakeCam>();

        anim = GetComponent<Animator>();
        MobMinimapPoint = transform.Find("Mini_Icon").gameObject;


        ws = new WaitForSeconds(0.5f);

        //// 보스 공격용 콜라이더를 찾기
        //if(bossType == Type.STAGEBOSS1)
        //{
        //    Abs_AAttack_CA = GameObject.Find("AttackAA").GetComponentInChildren<BoxCollider>();
        //    Abs_CAttack_CA = GameObject.Find("AttackAC").GetComponentInChildren<BoxCollider>();
        //}
        //else if(bossType == Type.STAGEBOSS2)
        //{
        //    Bbs_AAttack_CA = GameObject.Find("AttackBA").GetComponentInChildren<BoxCollider>();
        //    Bbs_CAttack_CA = GameObject.Find("AttackBC").GetComponentInChildren<BoxCollider>();
        //}
        //else if(bossType == Type.ENDBOSS)
        //{
        //    Cbs_AAttack_CA = GameObject.Find("AttackCA").GetComponentInChildren<BoxCollider>();
        //    Cbs_CAttack_CA = GameObject.Find("AttackCC").GetComponentInChildren<BoxCollider>();
        //}
  
        StartCoroutine(CheckState());
        StartCoroutine(BossAction());
    }


    private void Update()   //자동 공격이기때문에 업데이트로 호출함
    {
        if (isAttack)
        {
            if (Time.time >= nextAttack)
            {
                Attack();
                nextAttack = Time.time + attackRate+ Random.Range(0, 0.5f);
                // 다음공격은 = 지연시간 +공격딜레이
            }
            LookPlayer();
           
        }
    }


    //상태체크 코루틴
    IEnumerator CheckState()
    {
        while(!isDie)
        {
            //죽은상태라면 
            if (state == State.DIE)
            {
                yield break; 
            }

            float dist = Vector3.Distance(playerTr.position, bossTr.position);//플레이어랑 보스거리
            if (dist <= attackDistA)
            { 
                state = State.ATTACK;
            }

            else if (dist <= traceDistA)
            {
                int Moving = Random.Range(0, 3);
                if (Moving == 2)
                {
                    state = State.FLEE;
                }
                else if (Moving < 2)
                {
                    state = State.TRACE;
                }
            }

            else if (dist >= traceDistA || dist >= attackDistA) //0422 보스의 아이들 상태로 바꾸는 상홍
            { 
                state = State.IDLE; 
            }
            if (state == State.ATTACK)
            {
                yield return ws;
            }

            yield return new WaitForSeconds(0.2f);
        }

    }

    IEnumerator BossAction()
    {
       
        while (!isDie)
        {
            yield return ws;
          
            switch(state)
            {
                case State.IDLE:
                    bsMoveAgent.Stop();
                    isAttack = false;
                    aAttackEnd();           //공격콜라이더 off
                    cAttackEnd();
                    anim.SetBool(hashMove, false);
                    break;

                case State.FLEE:
                    bsMoveAgent.agent.SetDestination(playerTr.position + new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30)));
                    bsMoveAgent.agent.isStopped = false;
                    anim.SetBool(hashMove, true);
       
                    break;

                case State.TRACE:
                    bsMoveAgent.Stop();
                    isAttack = false;
                    aAttackEnd();           //공격콜라이더 off
                    cAttackEnd();
                    if (isMove == true)
                    {
                        bsMoveAgent.tranceTarget = playerTr.position;

                        anim.SetBool(hashMove, true);
                    }
                    else if (isMove == false)
                    {
                        state = State.IDLE;
                    }
                    break;
                case State.ATTACK:
                    bsMoveAgent.Stop();
                    anim.SetBool(hashMove, false);
                   
                   if (isAttack == false)
                    {
                      isAttack = true; 
                    }
                    


                    break;
                case State.DIE:
                    isDie = true;
                    isAttack = false;  //죽으면 공격중지
                    aAttackEnd();           //공격콜라이더 off
                    cAttackEnd();
                    bsMoveAgent.Stop();
                    anim.SetTrigger(hashDie);
                    anim.SetInteger(hashDieIdx, Random.Range(0, 3));
                    MobMinimapPoint.SetActive(false);      //미니맵 점 없애기코드

                    GetComponent<CapsuleCollider>().enabled = false;
                    if (bossType != Type.ENDBOSS)   //앤딩보스가 아닐때
                    {
                        DropBox();
                    }
                    else if (bossType == Type.ENDBOSS)  //앤딩보스일때
                    {
                        Invoke("DropKey", 1f);
                    }
                    GetComponent<BossDamage>().MobHpBar.SetActive(false);
                    Destroy(gameObject, 4f);

                    break;

            }

        }
    }

  

    public void Attack()
    {
      
        int randomAT = Random.Range(0, randomAttack); //랜덤 공격패턴구현

        switch (randomAT)
        {
            case 0:
            case 1:
            
                //스메쉬 패턴
                StartCoroutine(A_Attack());
                break;
            case 2:
            case 3:
                //땅찍고 스플래쉬
                StartCoroutine(B_Attack());
                break;
            case 4:
                StartCoroutine(C_Attack());
              
                //돌진 패턴
                break;
        }
    }


    IEnumerator A_Attack()
    {
        anim.SetTrigger(hashAttackA);

        yield return null; 
    }

    IEnumerator B_Attack()
    {
        bsMoveAgent.Stop();
        anim.SetTrigger(hashAttackB);
        yield return null;
    }

       
    IEnumerator C_Attack()
    {
        anim.SetTrigger(hashAttackC);

        yield return null;
    }




    // 애니메이션용 함수들
    void aAttackStart()
    {
        if (bossType == Type.STAGEBOSS1)
        {
            Abs_AAttack_CA.enabled = true;
           
        }
        else if (bossType == Type.STAGEBOSS2)
        {
            Bbs_AAttack_CA.enabled = true;
        
        }
        else if (bossType == Type.ENDBOSS)
        {
            Cbs_AAttack_CA.enabled = true;
     
        }

    }
    void aAttackEnd()
    {
        if (bossType == Type.STAGEBOSS1)
        {
            Abs_AAttack_CA.enabled = false;
        }
        else if (bossType == Type.STAGEBOSS2)
        {
            Bbs_AAttack_CA.enabled = false;
        }
        else if (bossType == Type.ENDBOSS)
        {
            Cbs_AAttack_CA.enabled = false;
        }
    }
    void bAttackStart()  //b애니메이션 이벤트용 함수
    {
        GameObject bsboom = Instantiate(bsBBomb);
        bsboom.transform.position = bossTr.transform.position;
        StartCoroutine(shakecam.ShakeCamera(0.1f, 0.2f, 0.5f));
        Destroy(bsboom, 0.5f);
    }

    void cAttackStart()
    {
        if (bossType == Type.STAGEBOSS1)
        {
            Abs_CAttack_CA.enabled = true;
           
        }
        else if (bossType == Type.STAGEBOSS2)
        {
            Bbs_CAttack_CA.enabled = true;
            
        }

        else if (bossType == Type.ENDBOSS)
        {
            Cbs_CAttack_CA.enabled = true;
          
        }

    }
    void cAttackEnd()
    {
        if (bossType == Type.STAGEBOSS1)
        {
            Abs_CAttack_CA.enabled = false;
        }
        else if (bossType == Type.STAGEBOSS2)
        {
            Bbs_CAttack_CA.enabled = false;
        }
        else if (bossType == Type.ENDBOSS)
        {
            Cbs_CAttack_CA.enabled = false;
        }
    }



    //아이템생성및 공격시 캐릭터 봄
    public void DropBox()
    {

        bsItemDrop.MakeItemBox();
    }
    public void DropKey()
    {
        bsItemDrop.MakeKey();
    }

    void LookPlayer()   //공격시 쳐다보게 함z
    {
        Quaternion rot = Quaternion.LookRotation(playerTr.position - bossTr.position);
        bossTr.rotation = Quaternion.Slerp(bossTr.rotation, rot,
                                            Time.deltaTime * damping);
    }


    //public void OnDrawGizmos()// 트레이스사거리 공격사거리 나타냄 
    //{
    //    if (state == State.TRACE)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawWireSphere(transform.position, traceDistA);
    //    }
    //    else if (state == State.ATTACK)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(transform.position, attackDistA);
    //    }
    //}

}
    
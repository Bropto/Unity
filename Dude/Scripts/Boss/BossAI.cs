using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAI : MonoBehaviour
{
    public enum Type            //������ Ÿ��(�������� ��������,�ص���������)
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
    //���¸� ���
    
    public State state = State.IDLE; //�⺻ ����


     Transform playerTr;
     Transform bossTr;

   
    bool isDie = false;
    bool isAttack = false;
    bool isMove = true;

     WaitForSeconds ws;
     BossMoveAgent bsMoveAgent;
     BossItemDrop bsItemDrop;
     Animator anim;




    [Header("����a=2 b=4 c=5")]
    public int randomAttack;        //���� ���� �Է� (a=2 b =4 c=5)
    
    readonly int hashMove = Animator.StringToHash("IsMove");

    readonly int hashAttackA = Animator.StringToHash("IsAttackA");
    readonly int hashAttackB = Animator.StringToHash("IsAttackB");
    readonly int hashAttackC = Animator.StringToHash("IsAttackC");

    public readonly int hashDie = Animator.StringToHash("Die");
    public readonly int hashDieIdx = Animator.StringToHash("DieIdx");

  
    public  float attackDistA = 4f; //���ݻ�Ÿ�
    public  float traceDistA = 30f; //�����Ÿ� 
    [Header("��ų ������")]
    float nextAttack = 1;          // ��������
    readonly float attackRate = 3.5f; // ���� ������
    readonly float damping = 10f;   //�÷��̾� �Ĵٺ� ȸ�����



    [Header("��ųA ����")]
    public float attackDamageA;        //�⺻���ݰ��� ������   �������� ������ �ٲ����! 
                                            //1�� 10 2�� 20 3�� 30���� �ӽ� 0427
    
    public BoxCollider Abs_AAttack_CA;      //����A���� �ݶ��̴�
    public BoxCollider Bbs_AAttack_CA;      //����B���� �ݶ��̴�    
    public BoxCollider Cbs_AAttack_CA;      //����C���� �ݶ��̴�


    [Header("��ųB ����")]
    public float attackDamageB;        //�⺻���ݰ��� ������   �������� ������ �ٲ����! 
                                       // 2�� 20 3�� 30���� �ӽ� 0427
    public Transform attackPosB;        //���� ����
    public GameObject bsBBomb;         //���� ���ݿ� ������Ʈ
    ShakeCam shakecam;

    [Header("��ųC ����")]

    public float attackDamageC = 20f;         //c������
                                              //3�� 20 �ӽ� 0428   
    public BoxCollider Abs_CAttack_CA;      //����A���� �ݶ��̴�
    public BoxCollider Bbs_CAttack_CA;      //����B���� �ݶ��̴�
    public BoxCollider Cbs_CAttack_CA;      //����C���� �ݶ��̴�


    GameObject MobMinimapPoint; //�̴ϸ� ������

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null) //�÷��̾� �±װ� ������
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

        //// ���� ���ݿ� �ݶ��̴��� ã��
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


    private void Update()   //�ڵ� �����̱⶧���� ������Ʈ�� ȣ����
    {
        if (isAttack)
        {
            if (Time.time >= nextAttack)
            {
                Attack();
                nextAttack = Time.time + attackRate+ Random.Range(0, 0.5f);
                // ���������� = �����ð� +���ݵ�����
            }
            LookPlayer();
           
        }
    }


    //����üũ �ڷ�ƾ
    IEnumerator CheckState()
    {
        while(!isDie)
        {
            //�������¶�� 
            if (state == State.DIE)
            {
                yield break; 
            }

            float dist = Vector3.Distance(playerTr.position, bossTr.position);//�÷��̾�� �����Ÿ�
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

            else if (dist >= traceDistA || dist >= attackDistA) //0422 ������ ���̵� ���·� �ٲٴ� ��ȫ
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
                    aAttackEnd();           //�����ݶ��̴� off
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
                    aAttackEnd();           //�����ݶ��̴� off
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
                    isAttack = false;  //������ ��������
                    aAttackEnd();           //�����ݶ��̴� off
                    cAttackEnd();
                    bsMoveAgent.Stop();
                    anim.SetTrigger(hashDie);
                    anim.SetInteger(hashDieIdx, Random.Range(0, 3));
                    MobMinimapPoint.SetActive(false);      //�̴ϸ� �� ���ֱ��ڵ�

                    GetComponent<CapsuleCollider>().enabled = false;
                    if (bossType != Type.ENDBOSS)   //�ص������� �ƴҶ�
                    {
                        DropBox();
                    }
                    else if (bossType == Type.ENDBOSS)  //�ص������϶�
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
      
        int randomAT = Random.Range(0, randomAttack); //���� �������ϱ���

        switch (randomAT)
        {
            case 0:
            case 1:
            
                //���޽� ����
                StartCoroutine(A_Attack());
                break;
            case 2:
            case 3:
                //����� ���÷���
                StartCoroutine(B_Attack());
                break;
            case 4:
                StartCoroutine(C_Attack());
              
                //���� ����
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




    // �ִϸ��̼ǿ� �Լ���
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
    void bAttackStart()  //b�ִϸ��̼� �̺�Ʈ�� �Լ�
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



    //�����ۻ����� ���ݽ� ĳ���� ��
    public void DropBox()
    {

        bsItemDrop.MakeItemBox();
    }
    public void DropKey()
    {
        bsItemDrop.MakeKey();
    }

    void LookPlayer()   //���ݽ� �Ĵٺ��� ��z
    {
        Quaternion rot = Quaternion.LookRotation(playerTr.position - bossTr.position);
        bossTr.rotation = Quaternion.Slerp(bossTr.rotation, rot,
                                            Time.deltaTime * damping);
    }


    //public void OnDrawGizmos()// Ʈ���̽���Ÿ� ���ݻ�Ÿ� ��Ÿ�� 
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
    
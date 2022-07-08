using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MagicMonAI : MonoBehaviour
{
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        FLEE,
        DAMAGE,
        DIE
    }

    public State state = State.IDLE;
    float traceDist = 100;
    float attackDist = 15;
    int diceNum = 0;
    public bool isDie = false;
    public bool isAttack = false;

    public GameObject magicBall;
    public Transform firePos;
    public GameObject DebuffZone;
    readonly float damping = 5f;

    MonsterItemDrop MonItemDrop;

    public Image hpBar;
    public Transform canvas;


    private float hp = 35;
    float iniHp = 35;
    public float damage = 20;

    MariaMovement M_Move;

    Transform monsterTr;
    Transform playerTr;
    NavMeshAgent agent;
    Animator anim;

    GameObject canvasEffect;//���� ���ֱ� 0610
    GameObject bloodEffect; //�� ȿ��
    Material mat;           //�ǰ� ����ȯȿ��

    //������ �޴� UI
    Transform DamageTakenTextPos;
    GameObject DamageTextPref;
    GameObject MobHpBar;//���� �Ⱥ��̱�
    GameObject MobMinimapPoint; //�̴ϸʾ�����

    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDamage = Animator.StringToHash("Damage");
    private readonly int hashDamageIdx = Animator.StringToHash("DamageIdx");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");
    private readonly int hashAttackIdx = Animator.StringToHash("AttackIdx");

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        M_Move = GameObject.FindWithTag("PLAYER").GetComponent<MariaMovement>();

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        MonItemDrop = GetComponent<MonsterItemDrop>();
        //�ؿ�3�� �ǰ�ȿ����
        canvasEffect = transform.Find("Canvas").gameObject;
        bloodEffect = Resources.Load<GameObject>("Effect/Blood");
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        iniHp = hp;
        MobMinimapPoint = transform.Find("Mini_Icon").gameObject;


        MobHpBar = transform.Find("Canvas/HpBar").gameObject;
        DamageTakenTextPos = transform.Find("Canvas/DamageTakenTextPos").transform;
        DamageTextPref = Resources.Load<GameObject>("DamageTakenTMP");


        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }
    private void OnCollisionEnter(Collision collision)
    {

        //MariaDamage
        if (DataController.Instance.CharSelectIdx == 0)
        {

            if (collision.collider.CompareTag("WEAPON") && playerTr.GetComponent<MariaSkill>().isSlash)
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= playerTr.GetComponent<MariaSkill>().SlashDamage;
                TakeDamage(playerTr.GetComponent<MariaSkill>().SlashDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }

            if (collision.collider.CompareTag("WEAPON") && playerTr.GetComponent<MariaSkill>().isHSlash)
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= playerTr.GetComponent<MariaSkill>().HSlashDamage;
                TakeDamage(playerTr.GetComponent<MariaSkill>().HSlashDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }


            if (collision.collider.CompareTag("WEAPON") && playerTr.GetComponent<MariaSkill>().isDSlash)
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.DsDamage;
                TakeDamage(DataController.Instance.DsDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }

            if (collision.collider.CompareTag("MARIAPS"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.PsDamage;
                TakeDamage(DataController.Instance.PsDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
            if (collision.collider.CompareTag("MARIABV"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.BvDamage;
                TakeDamage(DataController.Instance.BvDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }

        //BradyDamage
        if (DataController.Instance.CharSelectIdx == 1)
        {
            if (collision.collider.CompareTag("M_WEAPON"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());

                hp -= collision.gameObject.GetComponent<MagicCtrl>().damage;
                TakeDamage(collision.gameObject.GetComponent<MagicCtrl>().damage);

                collision.gameObject.SetActive(false);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }

            }
            if (collision.collider.CompareTag("EXPLODE"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.FireBallSplashDamage;
                TakeDamage(DataController.Instance.FireBallSplashDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
            if (collision.collider.CompareTag("FIREBALL"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.FireBallDamage;
                TakeDamage(DataController.Instance.FireBallDamage);

                Destroy(collision.gameObject);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }

            }
            if (collision.collider.CompareTag("METEO"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());

                hp -= DataController.Instance.Meteo;
                TakeDamage(DataController.Instance.Meteo);

                Destroy(collision.gameObject);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }

            }
            if (collision.collider.CompareTag("METEO_EXPLODE"))
            {

                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.MeteoSplash;
                TakeDamage(DataController.Instance.MeteoSplash);



                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }

            }
        }

        //ErikaDamage
        if (DataController.Instance.CharSelectIdx == 2)
        {

            if (collision.collider.CompareTag("ER_LATTACK"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.skill1_Damage; //�⺻���� ������ �߰��ʿ�
                TakeDamage(DataController.Instance.skill1_Damage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }

            if (collision.collider.CompareTag("ER_BOMB"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.skill2_Damage;
                TakeDamage(DataController.Instance.skill2_Damage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }


        hpBar.fillAmount = hp / iniHp;

    }
    private void OnTriggerEnter(Collider other)
    {
        //MariaDamage
        if (DataController.Instance.CharSelectIdx == 0)
        {

            if (other.gameObject.CompareTag("MARIAWS"))
            {
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.WsDamage;
                TakeDamage(DataController.Instance.WsDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }

        //BradyDamage
        if (DataController.Instance.CharSelectIdx == 1)
        {
            if (other.gameObject.CompareTag("CHAIN"))
            {
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.ChainDamage;
                TakeDamage(DataController.Instance.ChainDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }

        //ErikaDamage
        if (DataController.Instance.CharSelectIdx == 2)
        {
            if (other.gameObject.CompareTag("ER_TORNADO"))
            {
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.skill4_Damage;
                TakeDamage(DataController.Instance.skill4_Damage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }

            if (other.gameObject.CompareTag("ER_SKILLARROW"))
            {
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.skill1_Damage;
                TakeDamage(DataController.Instance.skill1_Damage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }



        hpBar.fillAmount = hp / iniHp;
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);

            if (state == State.DIE)
                yield break;

            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (distance <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (distance <= traceDist)
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
            else if(distance > attackDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }



    IEnumerator MonsterAction()
    {
        while (!isDie)
        {

            switch (state)
            {
                case State.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashMove, false);
                    break;
                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool(hashMove, true);
                    anim.SetBool(hashAttack, false);
                    break;
                case State.ATTACK:
                    anim.SetBool(hashAttack, true);
                    agent.isStopped = true;
                    AttackAnim();
                    break;
                case State.FLEE:
                    agent.SetDestination(playerTr.position + new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30)));
                    agent.isStopped = false;
                    anim.SetBool(hashMove, true);
                    break;
                case State.DIE:
                    isDie = true;
                    agent.isStopped = true;
                    anim.SetTrigger(hashDie);
                    anim.SetInteger(hashDieIdx, Random.Range(0, 2));
                    GetComponent<CapsuleCollider>().enabled = false;
                    MobHpBar.SetActive(false);      //���� ���ֱ��ڵ� 0610;
                    gameObject.tag = "Untagged";
                    MobMinimapPoint.SetActive(false);      //�̴ϸ� �� ���ֱ��ڵ�
                    DropItem();
                    Destroy(gameObject, 3f);
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }


    void Update()
    {
        if (state == State.ATTACK)
        {
            Quaternion rot = Quaternion.LookRotation(playerTr.position - monsterTr.position);
            monsterTr.rotation = Quaternion.Slerp(monsterTr.rotation, rot, Time.deltaTime * damping);

            canvas.forward = Camera.main.transform.forward;
        }
    }
    void Fire()
    {
        GameObject _magicBall = Instantiate(magicBall, firePos.position, firePos.rotation);
        Destroy(_magicBall, 2f);
    }

    void AttackAnim()
    {
        int num = diceNum;
        anim.SetInteger(hashAttackIdx, num);
    }

    void AttackDice()
    {
        diceNum = Random.Range(0, 2);
    }

    void DebuffCasting()
    {
        GameObject _DebuffZone = Instantiate(DebuffZone, playerTr.position, Quaternion.identity);
        Destroy(_DebuffZone, 5f);
    }
    public void DropItem()
    {
        MonItemDrop.MakeItem();
    }

    //�ǰ�ȿ�� �Լ���
    void ShowBloodEffect(Collision coll)
    {

        ContactPoint cp = coll.GetContact(0);
        Quaternion rot = Quaternion.LookRotation(-cp.normal);
        GameObject blood = Instantiate(bloodEffect, cp.point, rot);
        Destroy(blood, 2f);
    }

    IEnumerator MeshEffect()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        if (hp > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.clear;
        }
    }


    public void TakeDamage(float damage1)
    {
        GameObject DamageTakenText = Instantiate(DamageTextPref); // ������ �ؽ�Ʈ ������Ʈ
        DamageTakenText.GetComponent<MonsterDamageTaken>().damage = damage1; // ������ ����
        DamageTakenText.transform.parent = canvasEffect.transform;
        DamageTakenText.transform.position = DamageTakenTextPos.position;
    }
}

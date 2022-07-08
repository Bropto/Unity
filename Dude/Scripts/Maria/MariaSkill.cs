using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MariaSkill : MonoBehaviour
{
    #region Variables
    //About Animation
    Animator animator;
    readonly int hashIsRoll = Animator.StringToHash("Roll");

    //About UI
    MariaChaUI mariaChaUI;
    CoolTimeManager coolTimeManager;



    //About Slash
    readonly int hashIsSlash = Animator.StringToHash("Slash");
    readonly int hashIsHSlash = Animator.StringToHash("HSlash");
    public bool isSlash = false;
    public bool isHSlash = false;

    public float SlashDamage { get; set; } = 10;
    public float HSlashDamage { get; set; } = 15;
    
    
    //About DoubleSlash
    readonly int hashIsDSlash = Animator.StringToHash("DoubleSlash");
    float dsNeedMp = 5;
    public bool isDSlash = false;
    
    //About WindScar
    readonly int hashIsWindScar = Animator.StringToHash("WindScar");
    public GameObject windScarProjectile;

    float wsNeedMp = 5; 

    //About PetalSwirl
    readonly int hashIsPetalSwirl = Animator.StringToHash("PetalSwirl");
    public GameObject petalSwirlProjectile;
    float psNeedMp = 5;

    //About BladeVortex
    readonly int hashIsBladeVortex = Animator.StringToHash("BladeVortex");
    public GameObject bladeVortexProjectile;
    float bvNeedMp = 20; 


    #endregion


    void Start()
    {
        animator = GetComponent<Animator>();

        //FindObjectOfType<Script>() 스크립트 가진 오브젝트 찾기, GetComponent<Script> 해당 오브젝트가 가지고 있는 스크립트 참조
        mariaChaUI = DataController.Instance.UIManager.GetComponent<MariaChaUI>();
        coolTimeManager = DataController.Instance.UIManager.GetComponent<CoolTimeManager>();
    }

    #region SkillTrigger
    void Update()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false) //UI 클릭 아닐 때 
            {
                animator.SetTrigger(hashIsSlash);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false) //UI 클릭 아닐 때 
            {
                animator.SetTrigger(hashIsHSlash);
            }

        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger(hashIsRoll);
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger(hashIsRoll);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= dsNeedMp
            && coolTimeManager.isCool1 == false)
        {
            animator.SetFloat("DSspeed", DataController.Instance.DsSpeed);
            SkillDoubleSlash();
            coolTimeManager.isCool1 = true;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= dsNeedMp
            && coolTimeManager.isCool2 == false)
        {
            animator.SetFloat("WSspeed", DataController.Instance.WsSpeed);
            animator.SetTrigger(hashIsWindScar);
            coolTimeManager.isCool2 = true;

        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= dsNeedMp
            && coolTimeManager.isCool3 == false)
        {
            animator.SetTrigger(hashIsPetalSwirl);
            coolTimeManager.isCool3 = true;

        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= bvNeedMp
            && coolTimeManager.isCool4 == false)
        {
            animator.SetTrigger(hashIsBladeVortex);
            coolTimeManager.isCool4 = true;

        }


        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (mariaChaUI.H_Potion > 0)
            {
                mariaChaUI.H_Potion -= 1;

                //최대체력 10% 회복
                DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp 
                    += 1 / 10f * DataController.Instance.UIManager.GetComponent<MariaChaUI>().IniHp;

                mariaChaUI.H_Text.text = mariaChaUI.H_Potion.ToString();

                DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth();
            }


        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (mariaChaUI.M_Potion > 0)
            {
                mariaChaUI.M_Potion -= 1;

                //최대마나 10% 회복
                DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp += 
                    1 / 10f * DataController.Instance.UIManager.GetComponent<MariaChaUI>().IniMp;

                mariaChaUI.M_Text.text = mariaChaUI.M_Potion.ToString();
                DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();

            }

        }



    }
    #endregion


    #region SkillMethod
    void SkillDoubleSlash()
    {
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp -= dsNeedMp; //마나 적용
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();

        animator.SetTrigger(hashIsDSlash); //애니메이션 재생

    }

    void SkillWindScar() //animation event 사용
    {
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp -= wsNeedMp;
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();
        
        InstWindScar(0, 0); //중앙 투사체
        
        for (int i = 0; i < DataController.Instance.WsProjectile; i++) //양 옆 투사체 2개씩 늘도록 세팅
        {
            if (DataController.Instance.WsProjectile == 2)
            {
                InstWindScar(30f / 90f, i);
            }

            if (DataController.Instance.WsProjectile == 4)
            {
                if (i < 2)
                {
                    InstWindScar(67.5f / 90f, i);
                }

                else if (i >= 2 && i < 4)
                {
                    InstWindScar(33.75f / 90f, i);
                }
            }

            if (DataController.Instance.WsProjectile == 6)
            {
                if (i < 2)
                {
                    InstWindScar(67.5f / 90f, i);

                }

                else if (i >= 2 && i < 4)
                {
                    InstWindScar(((67.5f)* 1 / 3f)/ 90f, i);
                }

                else if (i >= 4 && i < 6)
                {
                    InstWindScar(((67.5f) * 2 / 3f) / 90f, i);
                }
            }

            if (DataController.Instance.WsProjectile == 8)
            {
                if (i < 2)
                {
                    InstWindScar(67.5f / 90f, i);

                }

                else if (i >= 2 && i < 4)
                {
                    InstWindScar(((67.5f) * 1 / 4f) / 90f, i);
                }

                else if (i >= 4 && i < 6)
                {
                    InstWindScar(((67.5f) * 2 / 4f) / 90f, i);
                }

                else if (i >= 6 && i < 8)
                {
                    InstWindScar(((67.5f) * 3 / 4f) / 90f, i);
                }
            }

        }
    }
    void InstWindScar(float angle, int i)
    {   
        //동적생성 위치를 정하기 위해 Lerp를 이용해서 대각선 점 찍기
        Vector3 forwardPos = Vector3.Lerp(transform.forward, transform.right * Mathf.Pow(-1, i), angle).normalized;

        //forwardPos를 평행이동
        Vector3 pos = transform.position + Vector3.up + forwardPos * 3;
        
        //forrwardPos 대각선 점으로 위치시키고 그 방향으로 쳐다보게 세팅
        Quaternion right = Quaternion.Euler(new Vector3(0, Mathf.Pow(-1, i) * 90 * angle, 0));

        //모양 세팅
        //Quaternion last = Quaternion.Euler(new Vector3(90, 0, 0));

        //플레이어의 로테이션과 합산
        Quaternion rot = transform.rotation * right;// * last;
        
        //생성 & 전방발사
        GameObject windScarProjectileInst = Instantiate(windScarProjectile, pos, rot);
        Rigidbody rigidWindScar = windScarProjectileInst.GetComponent<Rigidbody>();
        rigidWindScar.velocity = forwardPos * DataController.Instance.WsPojSpeed * Time.deltaTime;
        
        //파괴
        Destroy(windScarProjectileInst, DataController.Instance.WsDistance);
    }

    void SkillPetalSwirl() //animation event 사용
    {
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp -= psNeedMp;
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();

        for (int i = 0; i < DataController.Instance.PsNumOfProj; i++)
            InstPetalSwirl();

    }

    void InstPetalSwirl() //자세한 움직임은 PetalSwirlProjectile 스크립트 참조
    {
        GameObject petalSwirlProjectileInst = Instantiate(petalSwirlProjectile);
        Destroy(petalSwirlProjectileInst, DataController.Instance.PsDuration);
    }


    void SkillBladeVortex() //animation event 사용
    {
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp -= bvNeedMp;
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();
        
        if (DataController.Instance.BvNomOfProj == 1)
            InstBladeVortex(1 / 2f, 0, 0);

        else if (DataController.Instance.BvNomOfProj == 2)
        {
            for (int i = 0; i < 2; i++)
                InstBladeVortex(1 / 2f, i, 0);
        }

        else if (DataController.Instance.BvNomOfProj == 3)
        {
                InstBladeVortex(1 / 2f, 0, 0);
            InstBladeVortex(1 / 2f, 1, 0);
            InstBladeVortex(1 / 2f, 0, 1);
        }

        else if (DataController.Instance.BvNomOfProj == 4)
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    InstBladeVortex(1 / 2f, i, j);

        }

    }

    void InstBladeVortex(float angle, int i, int j)
    {
        if (angle >= 0 && angle <= 1)
        {
            //InstWindScar와 유사한데 차이점은 캐릭터의 뒤쪽에도 생성 가능한 점
            Vector3 forwardPos = Vector3.Lerp(transform.forward * Mathf.Pow(-1, i), transform.right * Mathf.Pow(-1, j), angle).normalized;
            Vector3 pos = transform.position + Vector3.up + forwardPos * 1;

            //Quaternion last = Quaternion.Euler(new Vector3(0, 90, 90)); //Object 모양 정하기
            Quaternion right = new Quaternion();
            Quaternion rot = new Quaternion();

            if (i % 2 == 0)
            {
                right = Quaternion.Euler(new Vector3(0, Mathf.Pow(-1, j) * 90 * angle, 0)); //XZ평면 상에서의 회전
                rot = transform.rotation * right; //* last;
            }
            else
            {
                //위의 방법대로 해보려고 시도했으나 꼬여서 그냥 LookRotation 활용. 위에서도 LookRotation 쓰면 더 편함
                right = Quaternion.LookRotation(pos - (transform.position + Vector3.up) );
                rot = right; //* last;
            }

            GameObject bladeVortexProjectileInst = Instantiate(bladeVortexProjectile, pos, rot);

            Destroy(bladeVortexProjectileInst, DataController.Instance.BvDuration);
            
        }
        else
            print("angle 값을 0에서 1사이로 넣으십시오");
    }


    #endregion


    //Animation Event
    //Slash, HSlash, DSlash 데미지 구분을 위해 모션에다가 bool값 조정하는 Event 2개 넣어서 구분.
    //예를 들어 HSlash면 모션 시작할 때 isHSlash true, 모션 끝날 때 모두 false.
    void SettingBool(string s)
    {
        isSlash = false;
        isHSlash = false;
        isDSlash = false;  
        
        if(s == "slash")
            isSlash = true;
        else if (s == "hslash")
            isHSlash = true;
        else if (s == "dslash")
            isDSlash = true;
        else
        {
            isSlash = false;
            isHSlash = false;
            isDSlash = false;
        }

    }


}

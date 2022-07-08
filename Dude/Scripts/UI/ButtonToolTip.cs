using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject TooltipBackImage;
    GameObject TooltipText;

    GameObject imagePref;
    bool exiting = false;

    public int skillNum = 1;
    string skillName = "Double Slash";

    public int skillExplainNum = 1;
    string skillExplain = "공격력 증가";
    string skillNumericExplain = "현재 공격력";
    float skillFigure = 100;

    string nextSkillNumericExplain = "다음 레벨 공격력";
    float nextSkillFigure = 120;

    public int masterSkillNum = 5;


    float plusPosX = 180;

    void Start()
    {
        imagePref = Resources.Load<GameObject>("SkillTooltipImage");

        //버튼 ID에 따른

        //설명 세팅
        ButtonIdentityExplain();
        //수치 세팅
        ButtonSpecialized(skillNum, skillExplainNum);
    }

    void ButtonIdentityExplain()
    {

        //스킬 번호, 이름 세팅
        if (skillNum == 1)
        {
            skillName = "Double Slash";
        }
        else if (skillNum == 2)
        {
            skillName = "Wind Scar";
        }
        else if (skillNum == 3)
        {
            skillName = "Petal Swirl";
        }
        else if (skillNum == 4)
        {
            skillName = "Blade Vortex";
        }

        //스킬 기능 설명 세팅
        if (skillExplainNum == 1)
        {
            skillExplain = "공격력 증가";
            skillNumericExplain = "현재 공격력";
            nextSkillNumericExplain = "다음 공격력";
        }
        else if (skillExplainNum == 2)
        {
            skillExplain = "칼 휘두르는 속도 증가";
            skillNumericExplain = "현재 속도";
            nextSkillNumericExplain = "다음 속도";

        }
        else if (skillExplainNum == 3)
        {
            skillExplain = "투사체 지속시간 증가";
            skillNumericExplain = "현재 투사체 지속시간";
            nextSkillNumericExplain = "다음 투사체 지속시간";

        }
        else if (skillExplainNum == 4)
        {
            skillExplain = "투사체 개수 증가";
            skillNumericExplain = "현재 투사체 개수";
            nextSkillNumericExplain = "다음 투사체 개수";

        }
        else if (skillExplainNum == 5)
        {
            skillExplain = "투사체 진행 속도 증가";
            skillNumericExplain = "현재 투사체 진행 속도";
            nextSkillNumericExplain = "다음 투사체 진행 속도";

        }
        else if (skillExplainNum == 6)
        {
            skillExplain = "투사체 진행 거리 증가";
            skillNumericExplain = "현재 투사체 진행 거리";
            nextSkillNumericExplain = "다음 투사체 진행 거리";

        }
    }
    public void ButtonSpecialized(int skillNum, int skillExplainNum)
    {
        if(skillNum == 1 && skillExplainNum == 1)
        {
            skillFigure = DataController.Instance.DsDamage;
            nextSkillFigure = skillFigure + 20;
        }
        else if (skillNum == 1 && skillExplainNum == 2)
        {
            skillFigure = DataController.Instance.DsSpeed;
            nextSkillFigure = skillFigure + 1;
        }
        else if (skillNum == 2 && skillExplainNum == 1)
        {
            skillFigure = DataController.Instance.WsDamage;
            nextSkillFigure = skillFigure + 20;
        }
        else if (skillNum == 2 && skillExplainNum == 2)
        {
            skillFigure = DataController.Instance.WsSpeed;
            nextSkillFigure = skillFigure + 1;
        }
        else if (skillNum == 2 && skillExplainNum == 4)
        {
            skillFigure = DataController.Instance.WsProjectile + 1;
            nextSkillFigure = skillFigure + 2;
        }
        else if (skillNum == 2 && skillExplainNum == 5)
        {
            skillFigure = DataController.Instance.WsPojSpeed;
            nextSkillFigure = skillFigure + 500;
        }
        else if (skillNum == 2 && skillExplainNum == 6)
        {
            skillFigure = DataController.Instance.WsDistance;
            nextSkillFigure = skillFigure + 10;
        }
        else if (skillNum == 3 && skillExplainNum == 1)
        {
            skillFigure = DataController.Instance.PsDamage;
            nextSkillFigure = skillFigure + 20;
        }
        else if (skillNum == 3 && skillExplainNum == 3)
        {
            skillFigure = DataController.Instance.PsDuration;
            nextSkillFigure = skillFigure + 5;
        }
        else if (skillNum == 3 && skillExplainNum == 4)
        {
            skillFigure = DataController.Instance.PsNumOfProj;
            nextSkillFigure = skillFigure + 5;
        }
        else if (skillNum == 3 && skillExplainNum == 5)
        {
            skillFigure = DataController.Instance.PsAttackSpeed;
            nextSkillFigure = skillFigure + 100;
        }
        else if (skillNum == 3 && skillExplainNum == 6)
        {
            skillFigure = DataController.Instance.PsDistance;
            nextSkillFigure = skillFigure + 10;
        }
        else if (skillNum == 4 && skillExplainNum == 1)
        {
            skillFigure = DataController.Instance.BvDamage;
            nextSkillFigure = skillFigure + 20;
        }
        else if (skillNum == 4 && skillExplainNum == 3)
        {
            skillFigure = DataController.Instance.BvDuration;
            nextSkillFigure = skillFigure + 5;
        }
        else if (skillNum == 4 && skillExplainNum == 4)
        {
            skillFigure = DataController.Instance.BvNomOfProj;
            nextSkillFigure = skillFigure + 1;
        }

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (!exiting)
        {
            exiting = true; //동적생성 후 파괴 반복되면 GarbageCollector 호출되어 성능 저하를 발생시키므로 한번 동적생성하고 그걸 SetActive로 재활용 하기 위한 bool 변수
            TooltipBackImage = Instantiate(imagePref);
            TooltipBackImage.transform.parent = transform.parent.transform.parent.transform; //버튼의 부모 : 패널, 패널의 부모 : 캔버스/ 캔버스의 자식으로 세팅
            TooltipBackImage.transform.position = transform.position + new Vector3(plusPosX, 0, 0); //버튼의 오른쪽으로 위치시킴
            TooltipText = TooltipBackImage.transform.Find("Text").gameObject; //이미지의 자식 텍스트 연결

            TooltipText.GetComponent<Text>().text = //Start에서 세팅(Inspector에 세팅)한 텍스트 작성
                "스킬" + skillNum + " " + skillName + "\n" +
                skillExplain + "\n" +
                skillNumericExplain + " : " + skillFigure + "\n" +
                nextSkillNumericExplain + " : " + nextSkillFigure + "\n" +
                "마스터 레벨 : " + masterSkillNum;

            StartCoroutine(FadeInTooltip()); //서서히 알파값 올리는 코루틴 실행
        }

        else
        {
            TooltipText.GetComponent<Text>().text = //Start에서 세팅(Inspector에 세팅)한 텍스트 작성
    "스킬" + skillNum + " " + skillName + "\n" +
    skillExplain + "\n" +
    skillNumericExplain + " : " + skillFigure + "\n" +
    nextSkillNumericExplain + " : " + nextSkillFigure + "\n" +
    "마스터 레벨 : " + masterSkillNum;

            StartCoroutine(FadeInTooltip());

        }
    }

    public void ClickEvent()
    {
        ButtonSpecialized(skillNum, skillExplainNum);
        TooltipText.GetComponent<Text>().text = //Start에서 세팅(Inspector에 세팅)한 텍스트 작성
        "스킬" + skillNum + " " + skillName + "\n" +
        skillExplain + "\n" +
        skillNumericExplain + " : " + skillFigure + "\n" +
        nextSkillNumericExplain + " : " + nextSkillFigure + "\n" +
        "마스터 레벨 : " + masterSkillNum;

    }


void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TooltipBackImage.SetActive(false);

    }


    IEnumerator FadeInTooltip()
    {
        TooltipBackImage.SetActive(true);
        Color imageColor = TooltipBackImage.GetComponent<Image>().color;
        Color textColor = TooltipText.GetComponent<Text>().color;
        imageColor.a = 0;
        textColor.a = 0;
        TooltipBackImage.GetComponent<Image>().color = imageColor;
        TooltipText.GetComponent<Text>().color = textColor;

        float time = 0;
        float fadeinTime = 0.7f;
        while (time < fadeinTime)
        {
            time += Time.deltaTime;

            imageColor.a = time / fadeinTime;
            textColor.a = time / fadeinTime;

            TooltipBackImage.GetComponent<Image>().color = imageColor;
            TooltipText.GetComponent<Text>().color = textColor;

            yield return new WaitForFixedUpdate();
        }
        yield return null;

    }

    void SetActiceTooltip()
    {
        TooltipBackImage.SetActive(true);

    }
}

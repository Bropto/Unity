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
    string skillExplain = "���ݷ� ����";
    string skillNumericExplain = "���� ���ݷ�";
    float skillFigure = 100;

    string nextSkillNumericExplain = "���� ���� ���ݷ�";
    float nextSkillFigure = 120;

    public int masterSkillNum = 5;


    float plusPosX = 180;

    void Start()
    {
        imagePref = Resources.Load<GameObject>("SkillTooltipImage");

        //��ư ID�� ����

        //���� ����
        ButtonIdentityExplain();
        //��ġ ����
        ButtonSpecialized(skillNum, skillExplainNum);
    }

    void ButtonIdentityExplain()
    {

        //��ų ��ȣ, �̸� ����
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

        //��ų ��� ���� ����
        if (skillExplainNum == 1)
        {
            skillExplain = "���ݷ� ����";
            skillNumericExplain = "���� ���ݷ�";
            nextSkillNumericExplain = "���� ���ݷ�";
        }
        else if (skillExplainNum == 2)
        {
            skillExplain = "Į �ֵθ��� �ӵ� ����";
            skillNumericExplain = "���� �ӵ�";
            nextSkillNumericExplain = "���� �ӵ�";

        }
        else if (skillExplainNum == 3)
        {
            skillExplain = "����ü ���ӽð� ����";
            skillNumericExplain = "���� ����ü ���ӽð�";
            nextSkillNumericExplain = "���� ����ü ���ӽð�";

        }
        else if (skillExplainNum == 4)
        {
            skillExplain = "����ü ���� ����";
            skillNumericExplain = "���� ����ü ����";
            nextSkillNumericExplain = "���� ����ü ����";

        }
        else if (skillExplainNum == 5)
        {
            skillExplain = "����ü ���� �ӵ� ����";
            skillNumericExplain = "���� ����ü ���� �ӵ�";
            nextSkillNumericExplain = "���� ����ü ���� �ӵ�";

        }
        else if (skillExplainNum == 6)
        {
            skillExplain = "����ü ���� �Ÿ� ����";
            skillNumericExplain = "���� ����ü ���� �Ÿ�";
            nextSkillNumericExplain = "���� ����ü ���� �Ÿ�";

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
            exiting = true; //�������� �� �ı� �ݺ��Ǹ� GarbageCollector ȣ��Ǿ� ���� ���ϸ� �߻���Ű�Ƿ� �ѹ� ���������ϰ� �װ� SetActive�� ��Ȱ�� �ϱ� ���� bool ����
            TooltipBackImage = Instantiate(imagePref);
            TooltipBackImage.transform.parent = transform.parent.transform.parent.transform; //��ư�� �θ� : �г�, �г��� �θ� : ĵ����/ ĵ������ �ڽ����� ����
            TooltipBackImage.transform.position = transform.position + new Vector3(plusPosX, 0, 0); //��ư�� ���������� ��ġ��Ŵ
            TooltipText = TooltipBackImage.transform.Find("Text").gameObject; //�̹����� �ڽ� �ؽ�Ʈ ����

            TooltipText.GetComponent<Text>().text = //Start���� ����(Inspector�� ����)�� �ؽ�Ʈ �ۼ�
                "��ų" + skillNum + " " + skillName + "\n" +
                skillExplain + "\n" +
                skillNumericExplain + " : " + skillFigure + "\n" +
                nextSkillNumericExplain + " : " + nextSkillFigure + "\n" +
                "������ ���� : " + masterSkillNum;

            StartCoroutine(FadeInTooltip()); //������ ���İ� �ø��� �ڷ�ƾ ����
        }

        else
        {
            TooltipText.GetComponent<Text>().text = //Start���� ����(Inspector�� ����)�� �ؽ�Ʈ �ۼ�
    "��ų" + skillNum + " " + skillName + "\n" +
    skillExplain + "\n" +
    skillNumericExplain + " : " + skillFigure + "\n" +
    nextSkillNumericExplain + " : " + nextSkillFigure + "\n" +
    "������ ���� : " + masterSkillNum;

            StartCoroutine(FadeInTooltip());

        }
    }

    public void ClickEvent()
    {
        ButtonSpecialized(skillNum, skillExplainNum);
        TooltipText.GetComponent<Text>().text = //Start���� ����(Inspector�� ����)�� �ؽ�Ʈ �ۼ�
        "��ų" + skillNum + " " + skillName + "\n" +
        skillExplain + "\n" +
        skillNumericExplain + " : " + skillFigure + "\n" +
        nextSkillNumericExplain + " : " + nextSkillFigure + "\n" +
        "������ ���� : " + masterSkillNum;

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

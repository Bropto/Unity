using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonToolTipXmlVersion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject TooltipBackImage;
    GameObject TooltipText;
    GameObject imagePref;

    public int skillNum;
    public int skillExplainNum;

    public string skillName;
    public string skillExplain;
    public string skillNumericExplain;
    public string[] skillFigure;

    public string nextSkillNumericExplain;
    public string[] nextSkillFigure;

    public int masterSkillNum;


    float plusPosX = 166.8f;
    float plusPosY = 6.6f;

    int count = 0;


    void Start()
    {
        imagePref = Resources.Load<GameObject>("SkillTooltipImage");

        //��ư ID�� ���� ����
        Invoke("DataSetting", 0.5f);

        TooltipBackImage = Instantiate(imagePref);
        TooltipBackImage.name = gameObject.name + "Tooltip";

        TooltipBackImage.transform.parent = transform.parent.transform.parent.transform; //��ư�� �θ� : �г�, �г��� �θ� : ĵ����/ ĵ������ �ڽ����� ����
        TooltipBackImage.transform.position = transform.position + new Vector3(plusPosX, plusPosY, 0); //��ư�� ���������� ��ġ��Ŵ
        TooltipText = TooltipBackImage.transform.Find("Text").gameObject; //�̹����� �ڽ� �ؽ�Ʈ ����


        TooltipBackImage.SetActive(false);

    }

    private void OnDisable()
    {
        TooltipBackImage.SetActive(false);
    }

    void DataSetting()
    {
        DataController.Instance.UIManager.GetComponent<ButtonToolTipManager>().LoadXml(skillNum, skillExplainNum);

    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
         StartCoroutine(FadeInTooltip());
        TooltipTextSetting();

    }

    void TooltipTextSetting()
    {
        if(count < masterSkillNum -1)
        {
            TooltipText.GetComponent<Text>().text = "��ų" + skillNum + " " + skillName + "\n" + skillExplain + "\n" + skillNumericExplain + " : "
    + skillFigure[count] + "\n" + nextSkillNumericExplain + " : " +
    skillFigure[count + 1] + "\n" + "������ ���� : " + masterSkillNum;
        }
        else if(count == masterSkillNum - 1)
        {
            TooltipText.GetComponent<Text>().text = "��ų" + skillNum + " " + skillName + "\n" + skillExplain + "\n" + skillNumericExplain + " : "
+ skillFigure[count] + "\n" + "��ų ������\n" + "������ ���� : " + masterSkillNum;

        }


    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TooltipBackImage.SetActive(false);

    }

    public void ClickEvent()
    {
        count++;
        TooltipTextSetting();

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


}


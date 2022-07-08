using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MonsterDamageTaken : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    TextMeshProUGUI text;
    Color textColor;
    public float damage;

    float criticalPercentage = 0.5f;
    bool isCritical = false;


    void Start()
    {
        moveSpeed = 1;
        alphaSpeed = 2.0f;
        destroyTime = 0.8f;

        text = GetComponent<TextMeshProUGUI>();
        textColor = text.color;

        damage = Dice(damage);

        if (isCritical)
        {
            
            textColor = Color.yellow;
        }




        text.text = damage.ToString("N0");
        
        Invoke("DestroyObject", destroyTime);


    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치올리기
        transform.forward = Camera.main.transform.forward;
        textColor.a = Mathf.Lerp(textColor.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = textColor;

    }

    public float Dice(float damage)
    {
        float deviation = Random.Range(0.95f, 1.05f);
        damage *= deviation;

        float critical = Random.Range(0f, 1f);
        if (critical < criticalPercentage)
        {
            isCritical = true;
            damage *= 2;
        }

        return damage;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}

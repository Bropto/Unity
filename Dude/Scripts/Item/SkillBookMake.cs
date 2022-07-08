using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBookMake : MonoBehaviour
{
    [Header("��ų�� �������� ��ȯ")]
    public int minSpawn = 3;
    public int maxSpawn = 5;

    public GameObject[] skillbooks;
    public Transform ItemBoxTr;

    private void Awake()
    {
      
            ItemBoxTr = GetComponent<Transform>();
            Invoke("RandomSkillBookDrop", 3f);      //0427 5>>>3�ʵڷ� ���� 
   

    }
    public void RandomSkillBookDrop()
    {
        int i = Random.Range(minSpawn, maxSpawn);// 3~5���� ������ ��ȯ�Ҳ��ϱ�

        for (int a = 0; a < i; a++)    //a= å�� ���� 
        {
            int booksnum = Random.Range(0, 4);  //å�� ��ȣ ��ȯ
            switch (booksnum)
            {
               
                case 0:
                    Instantiate(skillbooks[booksnum],
                                ItemBoxTr.transform.position, 
                                Quaternion.identity);
                    break;
                case 1:
                    Instantiate(skillbooks[booksnum],
                                ItemBoxTr.transform.position,
                                Quaternion.identity); break;
                case 2:
                    Instantiate(skillbooks[booksnum],
                                ItemBoxTr.transform.position,
                                Quaternion.identity); break;
                case 3:
                    Instantiate(skillbooks[booksnum],
                                ItemBoxTr.transform.position,
                                Quaternion.identity); break;

            }

        }
    }
}

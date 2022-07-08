using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBookMake : MonoBehaviour
{
    [Header("스킬북 랜덤갯수 소환")]
    public int minSpawn = 3;
    public int maxSpawn = 5;

    public GameObject[] skillbooks;
    public Transform ItemBoxTr;

    private void Awake()
    {
      
            ItemBoxTr = GetComponent<Transform>();
            Invoke("RandomSkillBookDrop", 3f);      //0427 5>>>3초뒤로 생성 
   

    }
    public void RandomSkillBookDrop()
    {
        int i = Random.Range(minSpawn, maxSpawn);// 3~5개의 아이템 소환할꺼니까

        for (int a = 0; a < i; a++)    //a= 책의 갯수 
        {
            int booksnum = Random.Range(0, 4);  //책의 번호 소환
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

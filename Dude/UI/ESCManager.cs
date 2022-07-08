using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESCManager : MonoBehaviour
{
    public GameObject ESC_Window;
    public bool A_ESC_Window = false;

    public GameObject Option_Win;
    bool IsOption = false;

    public GameObject RankingBoard;

    public Text RankBoardText;
    public Text RankBoardText1;
    public Text RankBoardText2;
    public Text RankBoardText3;
    void Start()
    {
        ESC_Window.SetActive(false);
        A_ESC_Window = false;

        RankBoardText = ESC_Window.transform.Find("ESCPanel/ESC_Window/Rankingboard/RankBoardPanel/Image/RankBoardText").GetComponent<Text>();
        RankBoardText1 = ESC_Window.transform.Find("ESCPanel/ESC_Window/Rankingboard/RankBoardPanel/Image/RankBoardText_1").GetComponent<Text>();
        RankBoardText2 = ESC_Window.transform.Find("ESCPanel/ESC_Window/Rankingboard/RankBoardPanel/Image/RankBoardText_2").GetComponent<Text>();
        RankBoardText3 = ESC_Window.transform.Find("ESCPanel/ESC_Window/Rankingboard/RankBoardPanel/Image/RankBoardText_3").GetComponent<Text>();

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "StartScene" && SceneManager.GetActiveScene().name != "CharacterSelect")
        {
            if (Input.GetKeyDown(KeyCode.Escape) && IsOption == false)
            {
                if (A_ESC_Window == false)
                {
                    ESC_Window.SetActive(true);
                    A_ESC_Window = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (A_ESC_Window == true)
                {
                    ESC_Window.SetActive(false);
                    A_ESC_Window = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && IsOption == true)
            {
                Option_Win.SetActive(false);
                IsOption = false;
            }
        }
       
    }

    public void Resume()
    {
        ESC_Window.SetActive(false);
        A_ESC_Window = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void Option()
    {
        Option_Win.SetActive(true);
        IsOption = true;
    }

    public void Ranking()
    {
        RankingBoard.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        
        if (DataController.Instance.mainWindowManager.guestLogin == false)
        {
            DataController.Instance.UIManager.GetComponent<MainWindowManager>().LoadRecordESC();
        }

        else if (DataController.Instance.mainWindowManager.guestLogin == true)
        {
            RankBoardText.text = "";
            RankBoardText1.text = "";
            RankBoardText2.text = "";
            RankBoardText3.text = "";

            for (int i = 0; i < DataController.Instance.displayRankNo; i++)
            {
                //숫자 작은게 랭킹 순위권이라 초기값을 크게 적용
                if (PlayerPrefs.GetFloat(i + "BestScore") == 0)
                {
                    PlayerPrefs.SetFloat(i + "BestScore", DataController.Instance.iniScore);
                }

                //저장된 랭킹 정보 가져오기
                DataController.Instance.bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
                DataController.Instance.bestName[i] = PlayerPrefs.GetString(i + "BestName");
                DataController.Instance.bestChar[i] = PlayerPrefs.GetString(i + "BestChar");

                if (DataController.Instance.bestScore[i] != DataController.Instance.iniScore)
                {
                    RankBoardText.text += string.Format("{0:00}", i + 1) + "\n";
                    RankBoardText1.text += DataController.Instance.bestName[i] + "\n";
                    RankBoardText2.text += DataController.Instance.bestChar[i] + "\n";
                    RankBoardText3.text += string.Format("{0:N2}", DataController.Instance.bestScore[i]) + "\n";

                }
            }

        }
    }

    public void Ranking_C()
    {
        RankingBoard.SetActive(false);
    }

    public void CharSelectScene()
    {
        DataController.Instance.ToCharSelectScene();
        ESC_Window.SetActive(false);
        A_ESC_Window = false;
    }
    public void Quit()
    {
        Application.Quit();
    }



}

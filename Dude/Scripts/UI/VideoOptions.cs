using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOptions : MonoBehaviour
{
    FullScreenMode screenMode;

    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;

    void Start()
    {
        FullScreenBtn(fullscreenBtn);

        InitUI();
    }

    void InitUI()
    {

        for(int i = 0; i< Screen.resolutions.Length; i++)
        {
            if(Screen.resolutions[i].refreshRate ==60)
                resolutions.Add(Screen.resolutions[i]);
        }
        
        resolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach(Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + " x " + item.height;
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OnBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }
}

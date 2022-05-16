using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] windows;
    [SerializeField] private GameObject buttons;
    public GameObject PlayMenu;
    public SaveConfig SaveConfig;
    public Language Language;
    
    void Awake()
    {
        Application.targetFrameRate = 360;
        ToggleWindow("LoadingMenu");
    }
    public void CloseWindows()
    {
        foreach( var item in windows)
            item.SetActive(false);
    }

    public void ToggleWindow(string nameOfWindow)
    {
        CloseWindows();
        windows.Single((x)=> x.name == nameOfWindow).SetActive(true);
    }
    public void PlayButton()
    {
        PlayMenu.SetActive(true);
        buttons.SetActive(false);
    }
    public void FindRoom()
    {
        ToggleWindow("FindRoomMenu");
    }
    public void CreateRoom()
    {
        ToggleWindow("CreateRoomMenu");
    }
    public void ChangeNickname()
    {
        ToggleWindow("ChangeNicknameMenu");
    }
    public void Configurator()
    {
        ToggleWindow("ConfiguratorMenu");
        PlayMenu.SetActive(false);
        buttons.SetActive(false);        
    }
    public void CloseConfigurator()
    {
        CloseWindows();
        PlayMenu.SetActive(true);
        SaveConfig.Save();
    }
    public void Options()
    {
        ToggleWindow("OptionsMenu");
        buttons.SetActive(false);
    }
    public void CloseOptions()
    {
        Language.SaveLanguage();
        CloseWindows();
        buttons.SetActive(true);
    }
    public void CloseNickname()
    {
        CloseWindows();
        buttons.SetActive(true);
    }
    public void Back()
    {
        PlayMenu.SetActive(false);
        buttons.SetActive(true);
    }
    public void CloseButton()
    {
        CloseWindows();
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}

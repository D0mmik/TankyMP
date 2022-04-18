using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] windows;
    [SerializeField] private GameObject buttons;
    public GameObject playMenu;
    public SaveConfig saveConfig;
    public Language language;
    
    void Start()
    {
        ToggleWindow("LoadingMenu");
    }
    public void CloseWindows()
    {
        foreach( var item in windows)
        {
            item.SetActive(false);
        }
    }

    public void ToggleWindow(string nameOfWindow)
    {
        CloseWindows();
        windows.Single((x)=> x.name == nameOfWindow).SetActive(true);
    }
    public void PlayButton()
    {
        playMenu.SetActive(true);
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
        playMenu.SetActive(false);
        buttons.SetActive(false);        
    }
    public void CloseConfigurator()
    {
        CloseWindows();
        playMenu.SetActive(true);
        saveConfig.Save();
    }
    public void Options()
    {
        ToggleWindow("OptionsMenu");
        buttons.SetActive(false);
    }
    public void CloseOptions()
    {
        language.SaveLanguage();
        CloseWindows();
        buttons.SetActive(true);
    }
    public void Back()
    {
        playMenu.SetActive(false);
        buttons.SetActive(true);
    }
    public void CloseButton()
    {
        CloseWindows();
        buttons.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    
}

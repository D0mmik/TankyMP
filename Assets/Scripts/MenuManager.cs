using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] windows;
    [SerializeField] private GameObject buttons;
    
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
        buttons.SetActive(false);
        
    }
    public void CloseButton()
    {
        CloseWindows();
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    
}

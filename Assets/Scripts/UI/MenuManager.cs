using System.Linq;
using Configuration;
using UnityEngine;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] GameObject[] Windows;
        [SerializeField] GameObject Buttons;
        public GameObject PlayMenu;
        public SaveConfig SaveConfig;
        public Language.Language Language;
    
        void Awake()
        {
            Application.targetFrameRate = 360;
            ToggleWindow("LoadingMenu");
        }
        public void CloseWindows()
        {
            foreach( var item in Windows)
                item.SetActive(false);
        }

        public void ToggleWindow(string nameOfWindow)
        {
            CloseWindows();
            Windows.Single((x)=> x.name == nameOfWindow).SetActive(true);
        }
        public void PlayButton()
        {
            PlayMenu.SetActive(true);
            Buttons.SetActive(false);
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
            Buttons.SetActive(false);        
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
            Buttons.SetActive(false);
        }
        public void CloseOptions()
        {
            Language.SaveLanguage();
            CloseWindows();
            Buttons.SetActive(true);
        }
        public void CloseNickname()
        {
            CloseWindows();
            Buttons.SetActive(true);
        }
        public void Back()
        {
            PlayMenu.SetActive(false);
            Buttons.SetActive(true);
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
}

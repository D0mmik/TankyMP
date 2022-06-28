using System.Linq;
using UnityEngine;

namespace Configuration
{
    public class Configurator : MonoBehaviour
    {
        [SerializeField] GameObject[] Windows;

        public void CloseWindows()
        {
            foreach( var item in Windows)
                item.SetActive(false);
        }

        private void ToggleWindow(string nameOfWindow)
        {
            CloseWindows();
            Windows.SingleOrDefault((x)=> x.name == nameOfWindow)?.SetActive(true);
        }
        public void WeaponsButton()
        {
            ToggleWindow("Weapons");
        }
        public void PropulsionButton()
        {
            ToggleWindow("Propulsion");
        }
        public void ArmorButton()
        {
            ToggleWindow("Armor");
        }
        public void ColorButton()
        {
            ToggleWindow("Color");
        }
        public void BeltButton()
        {
            ToggleWindow("BeltUpgrades");
        }
        public void FlyButton()
        {
            ToggleWindow("FlyUpgrades");
        }
        public void HoverButton()
        {
            ToggleWindow("HoverUpgrades");
        }
    }
}

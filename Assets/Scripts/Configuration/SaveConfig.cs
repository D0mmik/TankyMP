using PlayerScripts;
using UnityEngine;

namespace Configuration
{
    public class SaveConfig : MonoBehaviour
    {
        public ArmorChanger ArmorChanger;
        public ColorChanger ColorChanger;
        public WeaponsChanger WeaponsChanger;
        private const string Key_Armor = "armor";
        private const string Key_Color = "color";
        private const string Key_Weapon = "weapon";
        void Start()
        {
            Load();
        }

        public void Save()
        {
            PlayerPrefs.SetInt(Key_Armor, ArmorChanger.CurrentArmor);
            PlayerPrefs.SetInt(Key_Color, ColorChanger.CurrentColor);
            PlayerPrefs.SetInt(Key_Weapon, WeaponsChanger.CurrentWeapon);

        }
        public void Load()
        {
            ArmorChanger.DoArmor(PlayerPrefs.GetInt(Key_Armor));
            ColorChanger.DoColor(PlayerPrefs.GetInt(Key_Color));
            WeaponsChanger.DoWeapon(PlayerPrefs.GetInt(Key_Weapon));

        }
    }
}

using Photon.Pun;
using TMPro;
using UnityEngine;

namespace PlayerScripts
{
    public class NickName : MonoBehaviour
    {
        [SerializeField] TMP_Text NameIP;
        [SerializeField] TMP_Text LobbyNick;
        [SerializeField] GameObject NickNameMenu;
        [SerializeField] GameObject Buttons;
        bool hasNick;
        string nickName;

        void Start()
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("nick");
            LobbyNick.text = PhotonNetwork.NickName;

            hasNick = !PlayerPrefs.HasKey("nick");
            NickNameMenu.SetActive(hasNick);
        }

        public void SetName()
        {
            nickName = NameIP.text;
            PlayerPrefs.SetString("nick", nickName);
            LobbyNick.text = PhotonNetwork.NickName = nickName;

            NickNameMenu.SetActive(false);
            Buttons.SetActive(true);
        }
    }
}

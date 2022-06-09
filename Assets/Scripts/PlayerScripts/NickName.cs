using Photon.Pun;
using TMPro;
using UnityEngine;

namespace PlayerScripts
{
    public class NickName : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameIP;
        [SerializeField] private TMP_Text lobbyNick;
        [SerializeField] private GameObject nickNameMenu;
        [SerializeField] private GameObject buttons;
        private bool _hasNick;
        private string _nickName;

        void Start()
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("nick");
            lobbyNick.text = PhotonNetwork.NickName;

            _hasNick = !PlayerPrefs.HasKey("nick");
            nickNameMenu.SetActive(_hasNick);
        }

        public void SetName()
        {
            _nickName = nameIP.text;
            PlayerPrefs.SetString("nick", _nickName);
            lobbyNick.text = PhotonNetwork.NickName = _nickName;

            nickNameMenu.SetActive(false);
            buttons.SetActive(true);
        }
    }
}

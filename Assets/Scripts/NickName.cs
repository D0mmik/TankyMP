using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NickName : MonoBehaviour
{
    [SerializeField] private TMP_Text nameIP;
    [SerializeField] private TMP_Text lobbyNick;
    [SerializeField] private GameObject nickNameMenu;
    [SerializeField] private GameObject buttons;
    private bool hasNick;
    private string nickName;

    void Start()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString("nick");
        lobbyNick.text = PhotonNetwork.NickName;

        hasNick = !PlayerPrefs.HasKey("nick");
        nickNameMenu.SetActive(hasNick);
    }

    public void SetName()
    {
        nickName = nameIP.text;
        PlayerPrefs.SetString("nick", nickName);
        lobbyNick.text = PhotonNetwork.NickName = nickName;

        nickNameMenu.SetActive(false);
        buttons.SetActive(true);
    }
}

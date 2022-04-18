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
    void Start()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString("nick");
        lobbyNick.text = PhotonNetwork.NickName;
        Debug.Log(PhotonNetwork.NickName);
        if(!PlayerPrefs.HasKey("nick"))
        {
            nickNameMenu.SetActive(true);
            Debug.Log("true");
        }
        else
        {
            nickNameMenu.SetActive(false);
            Debug.Log("false");
        }
    }

    public void SetName()
    {
       PlayerPrefs.SetString("nick", nameIP.text);
       PhotonNetwork.NickName = PlayerPrefs.GetString("nick");
       lobbyNick.text = PhotonNetwork.NickName;
       nickNameMenu.SetActive(false);
       buttons.SetActive(true);
    }
}

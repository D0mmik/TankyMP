using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NickName : MonoBehaviour
{
    [SerializeField] private TMP_Text nameIP;
    [SerializeField] private GameObject nickNameMenu;
    void Start()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString("nick", "");
        Debug.Log(PhotonNetwork.NickName);
        if(PlayerPrefs.GetString("nick","") == "")
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
       nickNameMenu.SetActive(false);
    }
}

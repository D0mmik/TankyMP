using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NickName : MonoBehaviour
{
    [SerializeField] private TMP_Text nameIP;

    public void SetName()
    {
       PhotonNetwork.NickName = nameIP.text;
       this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField] private TMP_Text nameText;
    void Start()
    {
        if(photonView.IsMine)
            return;
        nameText.text = photonView.Owner.NickName;   
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class RoomPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public RoomInfo Info;

    public void OnStart(RoomInfo Info)
    {
        this.Info = Info;
        text.text = this.Info.Name;
    }
    public void OnClick()
    {
        PhotonNetwork.JoinRoom(Info.Name);
    }

}

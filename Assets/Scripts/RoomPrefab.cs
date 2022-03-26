using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class RoomPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public RoomInfo info;

    public void OnStart(RoomInfo Info)
    {
        info = Info;
        text.text = info.Name;
    }
    public void OnClick()
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

}

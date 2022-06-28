using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace PhotonMP
{
    public class RoomPrefab : MonoBehaviour
    {
        [SerializeField] TMP_Text RoomName;
        RoomInfo info;

        public void OnStart(RoomInfo info)
        {
            this.info = info;
            RoomName.text = this.info.Name;
        }
        public void OnClick()
        {
            PhotonNetwork.JoinRoom(info.Name);
        }

    }
}

using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace PhotonMP
{
    public class RoomPrefab : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        private RoomInfo _info;

        public void OnStart(RoomInfo info)
        {
            this._info = info;
            text.text = this._info.Name;
        }
        public void OnClick()
        {
            PhotonNetwork.JoinRoom(_info.Name);
        }

    }
}

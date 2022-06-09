using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace PhotonMP
{
    public class PlayerListPrefab : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text pName;
        private Player _player;
    
        public void OnStart(Player player)
        {
            _player = player;
            pName.text = _player.NickName;
        }
        public override void OnPlayerLeftRoom(Player leftPlayer)
        {
            if(!Equals(_player, leftPlayer))
                return;
        
            Destroy(gameObject);
        }
        public override void OnLeftRoom()
        {
            Destroy(this.gameObject);
        } 
    }
}

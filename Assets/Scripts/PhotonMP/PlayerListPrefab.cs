using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace PhotonMP
{
    public class PlayerListPrefab : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text PName;
        private Player player;
    
        public void OnStart(Player player)
        {
            this.player = player;
            PName.text = this.player.NickName;
        }
        public override void OnPlayerLeftRoom(Player leftPlayer)
        {
            if(!Equals(player, leftPlayer))
                return;
        
            Destroy(gameObject);
        }
        public override void OnLeftRoom()
        {
            Destroy(this.gameObject);
        } 
    }
}

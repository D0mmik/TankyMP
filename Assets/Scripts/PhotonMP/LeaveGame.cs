using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace PhotonMP
{
    public class LeaveGame : MonoBehaviourPunCallbacks
    {
        public void Leave()
        {
            if(!photonView.IsMine)
                return;

            Destroy(RoomManager.SRoomManager.gameObject);
            PhotonNetwork.LeaveRoom();
        }
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if(!photonView.IsMine)
                return;     
            SceneManager.LoadScene("Menu");
        }
    }
}

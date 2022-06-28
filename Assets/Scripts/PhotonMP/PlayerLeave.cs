using Photon.Pun;
using ShootingScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhotonMP
{
    public class PlayerLeave : MonoBehaviourPunCallbacks
    {
        [SerializeField] GameObject LeaveButton;
        [SerializeField] GameObject Bar;
        public static bool Paused;
        void Start()
        {
            LeaveButton.SetActive(false);
            Paused = false;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && !Paused && !Scope.SScoped)
            {
                LeaveButton.SetActive(true);
                Paused = true;
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && Paused)
            {
                LeaveButton.SetActive(false);
                Paused = false;
            }
        }
        public void Leave()
        {
            Destroy(RoomManager.SRoomManager.gameObject);
            Destroy(Bar);
            PhotonNetwork.LeaveRoom();
        }
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene("Menu");  
        }
    }
}

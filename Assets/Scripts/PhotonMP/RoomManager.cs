using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhotonMP
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        public static RoomManager SRoomManager;
        void Awake()
        {
            if(SRoomManager)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            SRoomManager = this;
        }
        public override void OnEnable()
        {
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        public override void OnDisable()
        {
            base.OnDisable();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if(scene.buildIndex != 1)
                return;
        
            GameObject playerManager = PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
        }
    }
}

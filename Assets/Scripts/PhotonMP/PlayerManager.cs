using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace PhotonMP
{
    public class PlayerManager : MonoBehaviourPun
    {
        [SerializeField] private Transform[] spawnpoints;
        private int i;
        private GameObject _controller;
        void Start()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            if(!photonView.IsMine)
                return;
        
            i = Random.Range(1,spawnpoints.Count());
            _controller = PhotonNetwork.Instantiate("Player", spawnpoints[i].position, Quaternion.identity, 0, new object[]{photonView.ViewID});
    
        }
        public void Die()
        {
            if(!photonView.IsMine)
                return;
        
            PhotonNetwork.Destroy(_controller);
            CreatePlayer();
        }
    }
}

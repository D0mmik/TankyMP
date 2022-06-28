using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace PhotonMP
{
    public class PlayerManager : MonoBehaviourPun
    {
        [SerializeField] Transform[] Spawnpoints;
        GameObject controller;
        void Start()
        {
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            if(!photonView.IsMine)
                return;
        
            var i = Random.Range(1,Spawnpoints.Count());
            controller = PhotonNetwork.Instantiate("Player", Spawnpoints[i].position, Quaternion.identity, 0, new object[]{photonView.ViewID});
    
        }
        public void Die()
        {
            if(!photonView.IsMine)
                return;
        
            PhotonNetwork.Destroy(controller);
            CreatePlayer();
        }
    }
}

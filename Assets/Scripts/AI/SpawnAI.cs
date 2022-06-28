using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace AI
{
    public class SpawnAI : MonoBehaviourPun
    {
        [Header("AI")]
        public int AICount;
        public int AIActive;
        [SerializeField] Transform[] Spawnpoints;

        public void Spawn()
        {
            if(!PhotonNetwork.IsMasterClient)
                return;
            if(AIActive < AICount)
            {
                var i = Random.Range(1,Spawnpoints.Count());
                PhotonNetwork.Instantiate("PlayerAI",Spawnpoints[i].position, Quaternion.identity);
                AIActive++;
            }          
        }   
    }
}
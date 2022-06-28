using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using PlayerScripts;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AI : MonoBehaviourPun
    { 
        [Header("AI Targets")]
        [SerializeField] NavMeshAgent NavMeshAgent;
        List<GameObject> opponentTarget;
        GameObject randomTarget;
        GameObject aiManager;

        [Header("Shooting")] 
        [SerializeField] float Reload;
        [SerializeField] Transform ShootPoint;
        [SerializeField] float Range;
        [SerializeField] float Health;
        float timer;

        [Header("Tank Configuration")]
        [SerializeField] GameObject[] Armor;
        [SerializeField] Material[] Color; 
        [SerializeField] GameObject Tank;

        SpawnAI spawnAI;
        bool instagib;

        void Awake()
        {
            instagib = GameModes.SInstagib;
            aiManager = GameObject.Find("AIManager");
            spawnAI = aiManager.GetComponent<SpawnAI>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            if(instagib)
            {
                Health = 1;
            }
            var randomArmor = Random.Range(0,Armor.Length);
            var randomColor = Random.Range(0,Color.Length);
            foreach( var item in Armor)
                item.SetActive(false);
            
            Armor[randomArmor].SetActive(true);
        
            Tank.GetComponent<MeshRenderer>().material = Color[randomColor];    
        }
        void Update()
        {
            if (randomTarget == null)
            {
                opponentTarget = GameObject.FindGameObjectsWithTag("Player").Where(x => !x.Equals(this.gameObject)).ToList();
                var randomTargetNumber = Random.Range(0,opponentTarget.Count);
                randomTarget = opponentTarget[randomTargetNumber];
            }
            NavMeshAgent.destination = randomTarget.transform.position;
            
            timer += Time.deltaTime % 60;
            if(timer >= 1)
            {
                Reload++;
                timer = 0;
            }
            if(Reload == 5)
                ShootAI();
       
        }
        public void TakeDamage(float damage)
        {      
            if(Health >= 0)
                photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage); 
        }
        [PunRPC]
        void RPC_TakeDamage(float damage)
        {
            if(!photonView.IsMine)
                return;
        
            Health -= damage;
        
            if(Health <= 0)
                photonView.RPC("RPC_Destroy", RpcTarget.MasterClient);
        }
        
        [PunRPC]
        void RPC_Destroy()
        {
            PhotonNetwork.Destroy(this.gameObject);
            spawnAI.AIActive--;
            spawnAI.Spawn();
        }

        void ShootAI()
        {
            if(Physics.Raycast(ShootPoint.position, ShootPoint.forward, out RaycastHit hit, Range))
            {
                if(hit.transform == null)
                    return;
            
                hit.transform.GetComponent<AI>()?.TakeDamage(30f);
                hit.transform.GetComponent<Target>()?.TakeDamage(30f);
            }
            Reload = 0;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using System.Linq;

public class AI : MonoBehaviourPun
{
    private  NavMeshAgent navMeshAgent;
    public List<GameObject> OpponentTarget;
    private int randomTargetNumber;
    public GameObject RandomTarget;

    private float timer;
    public float reload;
    public bool CanShoot;

    public Transform ShootPoint;
    private RaycastHit hit;
    private float range = 1000f;

    public float Health = 100;

    public int RandomArmor;
    public int RandomColor;
    public GameObject[] Armor;
    public Material[] Color;
    public GameObject Tank;
    public GameObject AIManager;
    private SpawnAI spawnAI;
    private bool instagib;

    void Awake()
    {
        instagib = GameModes.S_Instagib;
        AIManager = GameObject.Find("AIManager");
        spawnAI = AIManager.GetComponent<SpawnAI>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(instagib)
        {
            Health = 1;
        }
        RandomArmor = Random.Range(0,Armor.Length);
        RandomColor = Random.Range(0,Color.Length);
        foreach( var item in Armor)
            item.SetActive(false);
            
        Armor[RandomArmor].SetActive(true);
        
        Tank.GetComponent<MeshRenderer>().material = Color[RandomColor];    
    }
    void Update()
    {
        if(RandomTarget == null)
        {
            OpponentTarget = GameObject.FindGameObjectsWithTag("Player").Where(x => !x.Equals(this.gameObject)).ToList();
            randomTargetNumber = Random.Range(0,OpponentTarget.Count);
            RandomTarget = OpponentTarget[randomTargetNumber];
        }     
        navMeshAgent.destination = RandomTarget.transform.position;
    

        timer += Time.deltaTime % 60;
        if(timer >= 1)
        {
            reload++;
            timer = 0;
        }
        if(reload == 5)
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

        if(Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, range))
        {
            if(hit.transform == null)
                return;
            
            hit.transform.GetComponent<AI>()?.TakeDamage(30f);
            hit.transform.GetComponent<Target>()?.TakeDamage(30f);
            
        }
        reload = 0;
    }
}


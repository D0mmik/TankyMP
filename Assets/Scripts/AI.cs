using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using System.Linq;

public class AI : MonoBehaviourPun
{
    private  NavMeshAgent navMeshAgent;
    public List<GameObject> opponentTarget;
    private int randomTargetNumber;
    public GameObject randomTarget;

    private float timer;
    public float reload;
    public bool canShoot = true;

    public Transform shootPoint;
    private RaycastHit hit;
    private float range = 1000f;
    private Target target;
    private GameObject impact;

    public float health = 100;

    public int randomArmor;
    public int randomColor;
    public GameObject[] armor;
    public Material[] color;
    public GameObject tank;
    public GameObject aiManager;
    SpawnAI spawnAI;

    void Awake()
    {
        aiManager = GameObject.Find("AIManager");
        spawnAI = aiManager.GetComponent<SpawnAI>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if((bool)PhotonNetwork.CurrentRoom.CustomProperties["Instagib"] == true)
        {
            health = 1;
        }
        randomArmor = Random.Range(0,armor.Length);
        randomColor = Random.Range(0,color.Length);
        foreach( var item in armor)
        {
            item.SetActive(false);
        }
        armor[randomArmor].SetActive(true);
        
        tank.GetComponent<MeshRenderer>().material = color[randomColor];
    }
    void Update()
    {
        if(randomTarget == null)
        {
            opponentTarget = GameObject.FindGameObjectsWithTag("Player").Where(x => !x.Equals(this.gameObject)).ToList();
            randomTargetNumber = Random.Range(0,opponentTarget.Count);
            randomTarget = opponentTarget[randomTargetNumber];
        }     
        navMeshAgent.destination = randomTarget.transform.position;

        timer += Time.deltaTime % 60;
        if(timer >= 1)
        {
            reload++;
            timer = 0;
        }
        if(reload == 5)
        {
            ShootAI();
        }
       
    }
    public void TakeDamage(float damage)
    {      
        if(health >= 0)
        {
            photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage); 
        }
    }
    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if(!photonView.IsMine)
        {
            return;
        }
        health -= damage;
        
        if(health <= 0)
        {
            photonView.RPC("RPC_Destroy", RpcTarget.MasterClient); 
        }
    }

    [PunRPC]
    void RPC_Destroy()
    {
        PhotonNetwork.Destroy(this.gameObject);
        spawnAI.aiActive--;
        spawnAI.Spawn();
    }

    void ShootAI()
    {

        if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range))
        {
            if(hit.transform != null)
            {
                hit.transform.GetComponent<AI>()?.TakeDamage(30f);
                hit.transform.GetComponent<Target>()?.TakeDamage(30f);
            }
            // Collider[] colliders = Physics.OverlapSphere(hit.point, 0.3f);
            // if(colliders.Length != 0)
            // {
            //     impact = PhotonNetwork.Instantiate("impactPrefab", hit.point,Quaternion.LookRotation(hit.normal));
            //     impact.transform.SetParent(colliders[0].transform);
            // } 
            // StartCoroutine(WaitForDestroy());
        }
        reload = 0;
    }
    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(5f);
        if(impact != null)
        {
            PhotonNetwork.Destroy(impact);
        }
    }
}


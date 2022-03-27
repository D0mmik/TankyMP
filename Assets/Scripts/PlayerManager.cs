using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class PlayerManager : MonoBehaviourPun
{
    [SerializeField] private Transform[] spawnpoints;
    private int i = 0;
    public GameObject teamSelect;
    private bool redT = false;
    private bool blueT = false; 

    public void Red()
    {
        redT = true;
        blueT = false;
        GameObject.FindGameObjectWithTag("TeamSelect").SetActive(false);
        CreatePlayer(redT);
        Debug.Log("ffwf");
        
    }
    public void Blue()
    {
        blueT = true;
        redT = false;
        GameObject.FindGameObjectWithTag("TeamSelect").SetActive(false);
        CreatePlayer(redT);
    }
    private void CreatePlayer(bool redTeam)
    {
        i = Random.Range(1,spawnpoints.Count() + 1);
        Debug.Log(spawnpoints.Count());
        Debug.Log("číslo je" + i);
        if(redTeam == true)
        {
            PhotonNetwork.Instantiate("PlayerRed", spawnpoints[i].position, Quaternion.identity);
        }
        else if(redTeam == false)
        {
            PhotonNetwork.Instantiate("PlayerBlue", spawnpoints[i].position, Quaternion.identity);
        }
        Debug.Log("Player created");
    }
}

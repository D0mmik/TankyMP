using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameModes : MonoBehaviourPun
{
    public static bool S_Instagib;
    public static bool S_Randomizer;

    private void Awake()
    {
        S_Instagib = (bool)PhotonNetwork.CurrentRoom.CustomProperties["Instagib"];
        S_Randomizer = (bool)PhotonNetwork.CurrentRoom.CustomProperties["Randomizer"];   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ProgressBar : MonoBehaviourPunCallbacks
{
    public Image bar;
    void Start()
    {
        bar.fillAmount = 0;
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if(!propertiesThatChanged.ContainsKey("Score"))
        {
            return;
        } 
        bar.fillAmount = (float) propertiesThatChanged["Score"] / 20;
    }

}

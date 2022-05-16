using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ProgressBar : MonoBehaviourPunCallbacks
{
    public Image Bar;
    private float value;
    void Start()
    {
        Bar.fillAmount = 0;
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (!propertiesThatChanged.TryGetValue(("Score"), out object data)) 
            return;
        
        value = (float)data;
        Bar.fillAmount = value / 20;
    }

}

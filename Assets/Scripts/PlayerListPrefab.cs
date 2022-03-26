using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListPrefab : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text pName;
    private Player player;
    
    public void OnStart(Player Player)
    {
        player = Player;
        pName.text = player.NickName;
    }
    public override void OnPlayerLeftRoom(Player leftPlayer)
    {
        if(player == leftPlayer)
        {
            Destroy(gameObject);
        }
    }
    public override void OnLeftRoom()
    {
        Destroy(this.gameObject);
    }
    
}

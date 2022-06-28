using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerNameTag : MonoBehaviourPun
    {
        [SerializeField] private TMP_Text NameText;
        void Start()
        {
            if(photonView.IsMine)
                return;
            NameText.text = photonView.Owner.NickName;   
        }    
    }
}

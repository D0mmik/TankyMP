using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerNameTag : MonoBehaviourPun
    {
        [SerializeField] private TMP_Text nameText;
        void Start()
        {
            if(photonView.IsMine)
                return;
            nameText.text = photonView.Owner.NickName;   
        }    
    }
}

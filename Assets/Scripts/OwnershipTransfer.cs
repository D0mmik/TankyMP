using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OwnershipTransfer : MonoBehaviourPun
{
    void OnMouseDown()
    {
        base.photonView.RequestOwnership();
    }
}

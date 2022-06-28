using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace CapturePoint
{
    public class ProgressBar : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Image Bar;
        void Start()
        {
            Bar.fillAmount = 0;
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if (!propertiesThatChanged.TryGetValue(("Score"), out var data)) 
                return;
        
            var value = (float)data;
            Bar.fillAmount = value / 20;
        }

    }
}

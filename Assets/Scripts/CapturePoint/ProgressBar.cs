using Photon.Pun;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace CapturePoint
{
    public class ProgressBar : MonoBehaviourPunCallbacks
    {
        public Image Bar;
        private float _value;
        void Start()
        {
            Bar.fillAmount = 0;
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            if (!propertiesThatChanged.TryGetValue(("Score"), out var data)) 
                return;
        
            _value = (float)data;
            Bar.fillAmount = _value / 20;
        }

    }
}

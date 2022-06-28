using Photon.Pun;

namespace PlayerScripts
{
    public class GameModes : MonoBehaviourPun
    {
        public static bool SInstagib;
        public static bool SRandomizer;

        private void Awake()
        {
            SInstagib = (bool)PhotonNetwork.CurrentRoom.CustomProperties["Instagib"];
            SRandomizer = (bool)PhotonNetwork.CurrentRoom.CustomProperties["Randomizer"];   
        }
    }
}

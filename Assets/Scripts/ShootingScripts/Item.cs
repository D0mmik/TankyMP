using UnityEngine;

namespace ShootingScripts
{
    public abstract class Item : MonoBehaviour
    {
        public GunInfo GunInfo;
        public GameObject ItemGameObject;

        public abstract void Use();
    }
}

using UnityEngine;

namespace ShootingScripts
{
    [CreateAssetMenu(menuName = "FPS/New Gun")]
    public class GunInfo : ScriptableObject
    {
        public string GunName;
        public float Damage;
        public float ReloadTime;
        public bool Projectile;
        public float Bullets;
    }
}

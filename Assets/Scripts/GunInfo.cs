using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/New Gun")]
public class GunInfo : ScriptableObject
{
    public string GunName;
    public float Damage;
    public float ReloadTime;
    public bool Projectile;
    public float Bullets;
}

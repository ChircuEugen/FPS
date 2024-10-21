using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Add Gun", menuName = "Guns")]
public class GunSO : ScriptableObject
{
    public int damage = 15;
    public float range = 20f;
    public int maxBullets = 30;
    public int totalAmmo = 150;
    public float fireInterval = 15f;
    public float reloadTime = 1.5f;

    public ParticleSystem hitParticle;
}

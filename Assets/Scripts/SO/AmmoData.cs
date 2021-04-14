using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New AmmoData", menuName = "Ammo Data", order = 60)]
public class AmmoData : ScriptableObject
{
    [SerializeField]
    private string gunName;
    
    [SerializeField]
    private float gunnerSpeed;

    [SerializeField]
    private float shotPower;

    [SerializeField]
    private float gunFireRate;

    [SerializeField]
    private float fireRange;
    public string GunName { get { return gunName; } }
    public float GunnerSpeed { get { return gunnerSpeed; } }
    public float ShotPower { get { return shotPower; } }
    public float GunFireRate { get { return gunFireRate; } }
    public float FireRange { get { return fireRange; } }

}

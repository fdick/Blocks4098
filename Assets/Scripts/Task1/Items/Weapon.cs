using UnityEngine;

public enum WeaponKind
{
    Range,
    Melle,
}


public enum MassWeapon
{
    FlyWeight,
    MiddleWeight,
    HеavyWeight,
}

public class Weapon : ItemBase
{
    [SerializeField] private WeaponKind _weaponType = global::WeaponKind.Range;
    [SerializeField] private MassWeapon _mass = MassWeapon.FlyWeight;
    [SerializeField] private int _damage;

    [field: Min(0)]
    [field: SerializeField]
    public float CdTime { get; private set; } = 0; //attack frequency


    public WeaponKind WeaponType
    {
        get => _weaponType;
    }

    public MassWeapon Mass
    {
        get => _mass;
    }

    public int Damage
    {
        get => _damage;
    }

    [field: SerializeField] public int HitDistance { get; protected set; }
}
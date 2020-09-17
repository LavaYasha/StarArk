using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "Ships/WarShip")]
public class Warship : ScriptableObject, IWarship
{
    [SerializeField] private int _standartDistance;
    [SerializeField] private float _hitPoint;
    [SerializeField] private float _shieldPoint;
    [SerializeField] private string _name;
    [SerializeField] private GameObject _ship;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _weaponType;
    [SerializeField] private string _shieldType;
    [SerializeField] private float _shootingRange;
    [SerializeField] private float _damage;
    public int StandartDistance => _standartDistance;

    public float HitPoint => _hitPoint;

    public float ShieldPoint => _shieldPoint;
    public string Name => _name;

    public GameObject Ship => _ship;

    public Sprite Icon => _icon;

    public string WeaponType => _weaponType;
    public string ShieldType => _shieldType;
    public float ShootingRange => _shootingRange;
    public float Damage => _damage;
}

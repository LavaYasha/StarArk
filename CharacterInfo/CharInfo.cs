using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInfo : MonoBehaviour
{
    [SerializeField] private string _shipName = "";
    [SerializeField] private float _hitPoints = 0;
    [SerializeField] private float _shieldPoints = 0;

    [SerializeField] private string _weaponType = "";
    [SerializeField] private string _shieldType = "";

    [SerializeField] private int _moveDistance = 0;

    [SerializeField] private float _shootingRange = 0;
    [SerializeField] private float _damage = 0;
    public string ShipName => _shipName;
    public float HitPoints => _hitPoints;
    public float ShieldPoints => _shieldPoints;
    public string WeaponType => _weaponType;
    public string ShieldType => _shieldType;
    public int MoveDistance => _moveDistance;
    public float ShootingRange => _shootingRange;
    public float Damage => _damage;
    public bool Choosen { get; set; }
    public bool IsDead { get; private set; }

    public void Init(IWarship ship)
    {
        IsDead = false;
        _moveDistance = ship.StandartDistance;

        _hitPoints = ship.HitPoint;
        _shieldPoints = ship.ShieldPoint;

        _weaponType = ship.WeaponType;
        _shieldType = ship.ShieldType;

        _shootingRange = ship.ShootingRange;
        _damage = ship.Damage;
    }
    public void SetName(string name)
    {
        _shipName = name;
    }
    public void TakeDamage(float damage, string weaponType)
    {
        if (weaponType == _shieldType || (weaponType == "Yellow") && (_shieldType == "Green") ||
            (weaponType == "Green") && (_shieldType == "Blue") || (weaponType == "Blue") && (_shieldType == "Yellow"))
        {
            float halfDamage = damage / 2;
            HPDamage(halfDamage);
            ShieldDamage(damage - halfDamage);
        }
        else
        {
            HPDamage(damage);
        }
    }
    private void HPDamage(float damage)
    {
        if (damage < _hitPoints)
        {
            _hitPoints -= damage;
        }
        else
        {
            _hitPoints = 0;
            Death();
        }
    }

    private void ShieldDamage(float damage)
    {
        _shieldPoints -= damage;
    }

    private void Death()
    {
        TurnManager.UnitDeath(gameObject);
        IsDead = true;
        Destroy(gameObject);
    }
}

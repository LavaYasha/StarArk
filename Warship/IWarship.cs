using UnityEngine;
public interface IWarship
{
    int StandartDistance { get; }
    float HitPoint { get; }
    float ShieldPoint { get; }
    string Name { get; }
    GameObject Ship { get; }
    Sprite Icon { get; }
    string WeaponType { get; } // не уверен в типе данных
    string ShieldType { get; }
    float ShootingRange { get; }
    float Damage { get; }
}
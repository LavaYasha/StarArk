using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalShooting : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera gamecamera;
    GameObject laser;
    bool fire = false;
    CharInfo _info;
    private void Start()
    {
        _info = GetComponent<CharInfo>();
        laser = Resources.Load("Prefabs/Laser") as GameObject;
    }
    public void Fire(GameObject target)
    {
        gamecamera = GameObject.FindGameObjectWithTag("GameCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        gamecamera.Follow = gameObject.transform;
        Debug.Log($"Fire name {gameObject.name}");
        CacluleLaserLine(gameObject.transform.TransformPoint(gameObject.transform.position), target.transform.TransformPoint(target.transform.position), target);
        target.GetComponent<CharInfo>().TakeDamage(_info.Damage, _info.WeaponType);
        //StartCoroutine(Delay());
    }

    public void EndFire()
    {
        DestroyAllLasers();
        TurnManager.EndTurn();
    }

    //Courutine never use, but maybe useful
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        EndFire();
    }
    private void CacluleLaserLine(Vector3 begin, Vector3 end, GameObject target)
    {
        LookAtShootingTarget(end);
        RaycastHit hit;
        Ray laserRay = new Ray(begin, (end - begin));
        bool hitTarget = Physics.Raycast(laserRay, out hit);

        if (hitTarget)
        {
            DrawLine(begin, end);
        }
    }
    private void LookAtShootingTarget(Vector3 targetPos)
    {
        gameObject.transform.forward = targetPos - gameObject.transform.position;
    }
    private void DrawLine(Vector3 begin, Vector3 end)
    {
        GameObject go = Instantiate(laser, Vector3.zero, Quaternion.identity ,gameObject.transform);
        LineRenderer line = go.GetComponent<LineRenderer>();

        line.SetPosition(0, begin);
        line.SetPosition(1, end);
    }

    private void DestroyAllLasers()
    {
        foreach(Transform item in transform)
        {
            Destroy(item.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class LineBehavior : MonoBehaviour
{
    public Vector3 firingPoint, deflectPoint, enemyBody, playerBody;
    GameObject player, playerWeapon;
    public float laserDuration;

    LineRenderer bulletLine;
    void Awake()
    {
        bulletLine = GetComponent<LineRenderer>();
        bulletLine.positionCount = 0;
        bulletLine.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = GameObject.FindGameObjectWithTag("DeflectPoint");
        firingPoint = transform.position;
        enemyBody = transform.parent.position;
        playerBody = player.transform.position;
        deflectPoint= playerWeapon.transform.position;
    }

    public void DeflectedLineDraw()
    {
        firingPoint = transform.position;
        enemyBody = transform.parent.position;
        deflectPoint = playerWeapon.transform.position;
        bulletLine.useWorldSpace = true;
        bulletLine.positionCount = 3;
        bulletLine.SetPosition(0, firingPoint);
        bulletLine.SetPosition(1, deflectPoint);
        bulletLine.SetPosition(2, enemyBody);
        StartCoroutine(ShootBullet());
    }
    public void UndeflectedLineDraw()
    {
        firingPoint = transform.position;
        playerBody = player.transform.position;
        bulletLine.useWorldSpace = true;
        bulletLine.positionCount = 2;
        bulletLine.SetPosition(0, firingPoint);
        bulletLine.SetPosition(1, playerBody);
        StartCoroutine(ShootBullet());
    }

    IEnumerator ShootBullet()
    {
        bulletLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        bulletLine.useWorldSpace = false;
        bulletLine.enabled = false;
        bulletLine.positionCount = 0;
        this.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bullet;

    [Header("Bullet force")]
    public float shootForce, upwardForce;

    [Header("Gun stats")]
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //additional bools
    bool shooting, readyToShoot, reloading;

    [Header("Camera Reference")]
    public Camera fpsCam;
    public Transform attackPoint;

    [Header("Visual Reference")]

    public GameObject muzzleFlash;
    //public TextMeshProUGUI ammunitionDisplay;
    //public TextMeshProUGUI WeaponName;
    public string Weapontype;

    public bool allowInvoke;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

    }
    private void Update()
    {
        //WeaponName.text = Weapontype;
        MyInput();

        //if (ammunitionDisplay != null)
            //ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);
    }
    private void MyInput()
    {


        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


   

        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }

    }
    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
     //   RaycastHit hit;

        Vector3 targetPoint = ray.GetPoint(75f); ;


        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, z);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        if (bulletsLeft == 0)
        { Reload(); }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
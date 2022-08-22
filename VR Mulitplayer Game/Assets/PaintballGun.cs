using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class PaintballGun : Weapon
{
    //[SerializeField] private Projectile bulletPrefab;
    [SerializeField] public string bulletPrefab;

    protected override void StartShooting(XRBaseInteractor interactor)
    {
        base.StartShooting(interactor);
        Shoot();
    }

    protected override void Shoot()
    {
        base.Shoot();
        GameObject projectileInstanceGameObject = PhotonNetwork.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        //Projectile projectileInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Projectile projectileInstance = projectileInstanceGameObject.GetComponent<Projectile>();
        projectileInstance.Init(this); //projectileInstanceGameObject
        projectileInstance.Launch();
    }

    protected override void StopShooting(XRBaseInteractor interactor)
    {
        base.StopShooting(interactor);
    }
}

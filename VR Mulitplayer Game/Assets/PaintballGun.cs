using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class PaintballGun : Weapon
{
    private PhotonView photonView;
    [SerializeField] private Projectile bulletPrefab;
    //[SerializeField] public string bulletPrefab;

    protected override void StartShooting(ActivateEventArgs arg0)
    {
        base.StartShooting(arg0);
        Shoot();
    }

    protected override void Shoot()
    {
        base.Shoot();
        //GameObject projectileInstanceGameObject = PhotonNetwork.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        photonView.RPC("shootBullet", RpcTarget.All, bulletSpawn.position, bulletSpawn.rotation);
    }

    [PunRPC]
    void shootBullet(Vector3 position, Quaternion rotation)
    {
        Projectile projectileInstance = Instantiate(bulletPrefab, position, rotation);
        projectileInstance.Init(this);
        projectileInstance.Launch();
    }

    protected override void StopShooting(DeactivateEventArgs arg0)
    {
        base.StopShooting(arg0);
    }
}

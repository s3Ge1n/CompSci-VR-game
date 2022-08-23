using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RaycastPaintballGun : Weapon
{
    [SerializeField] float fireRate;
    private Projectile projectile;

    private WaitForSeconds wait;

    protected override void Awake()
    {
        base.Awake();
        projectile = GetComponentInChildren<Projectile>();
    }

    private void Start()
    {
        wait = new WaitForSeconds(1 / fireRate);
        projectile.Init(this);
    }
    protected override void StartShooting(ActivateEventArgs arg0)
    {
        base.StartShooting(arg0);
        StartCoroutine(ShootingCO());
    }

    private IEnumerator ShootingCO()
    {
        while (true)
        {
            Shoot();
            yield return wait;

        }
    }

    protected override void Shoot()
    {
        base.Shoot();
        projectile.Launch();
    }

    protected override void StopShooting(DeactivateEventArgs arg0)
    {
        base.StopShooting(arg0);
        StopAllCoroutines();
    }
}

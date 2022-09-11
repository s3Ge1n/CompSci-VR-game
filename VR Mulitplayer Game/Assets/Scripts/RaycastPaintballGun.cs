using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class RaycastPaintballGun : Weapon
{
    [SerializeField] private AudioClip _gunShot;
    [SerializeField] float fireRate;
    private AudioSource _gunSource;
    private Projectile projectile;

    private WaitForSeconds wait;

    protected override void Awake()
    {
        base.Awake();
        projectile = GetComponentInChildren<Projectile>();
    }

    private void Start()
    {
        _gunSource = GetComponent<AudioSource>();
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
        _gunSource.PlayOneShot(_gunShot);
        projectile.Launch();
    }

    protected override void StopShooting(DeactivateEventArgs arg0)
    {
        base.StopShooting(arg0);
        StopAllCoroutines();
    }
}

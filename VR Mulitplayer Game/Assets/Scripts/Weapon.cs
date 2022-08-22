using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabNetworkInteractable))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected float shootingForce;
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] protected float recoilForce;
    [SerializeField] protected float damage;

    private Rigidbody rigidBody;
    private XRGrabNetworkInteractable interactableWeapon;

    protected virtual void Awake()
    {
        interactableWeapon = GetComponent<XRGrabNetworkInteractable>();
        rigidBody = GetComponent<Rigidbody>();
        SetupInteractableWeaponEvents();
    }

    private void SetupInteractableWeaponEvents()
    {
        //interactableWeapon.activated.AddListener(StartShooting);
        //interactableWeapon.deactivated.AddListener(StopShooting);
    
        interactableWeapon.onActivate.AddListener(StartShooting);
        interactableWeapon.onDeactivate.AddListener(StopShooting);
    }

    /*
    private void StopShooting(DeactivateEventArgs arg0)
    {
       throw new NotImplementedException();
    }

    private void StartShooting(ActivateEventArgs arg0)
    {
       throw new NotImplementedException();
    }
    */
    protected virtual void StartShooting(XRBaseInteractor interactor)
    {
       throw new NotImplementedException();
    }

    protected virtual void StopShooting(XRBaseInteractor interactor)
    {
       throw new NotImplementedException();
    }

    protected virtual void Shoot()
    {
        ApplyRecoil();

    }

    private void ApplyRecoil()
    {
        rigidBody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
    }

    public float GetShootingForce()
    {
        return shootingForce;
    }

    public float GetDamage()
    {
        return damage;
    }

}

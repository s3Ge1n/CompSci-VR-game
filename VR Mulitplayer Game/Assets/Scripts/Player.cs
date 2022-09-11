using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Unity.XR.CoreUtils;
using System;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviourPun
{
    public LayerMask groundLayer;
    private XROrigin origin;
    public XRNode inputSource;
    private CharacterController character;

    [SerializeField] private AudioClip _painGrunt;
    [SerializeField] private float speed = 1f;
    [SerializeField] float health;
    [SerializeField] Transform head;
    [SerializeField] float heightOffset = 0.2f;

    private AudioSource _painSource;
    private Vector2 inputAxis;
    private float fallingSpeed;
    private int gravity = -10;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.LogError(string.Format("Player health: {0}", health));
        if (health <= 0)
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel(0);
        }
        _painSource.PlayOneShot(_painGrunt);
    }

    public Vector3 GetHeadPosition()
    {
        return head.position;
    }
    private void Start()
    {
        _painSource = GetComponent<AudioSource>();
        character = GetComponent<CharacterController>();
        origin = GetComponent<XROrigin>();
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, origin.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * speed * Time.deltaTime);

        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void CapsuleFollowHeadset()
    {
        character.height = origin.CameraInOriginSpaceHeight + heightOffset;
        Vector3 capsuleCenter = transform.InverseTransformPoint(origin.CameraInOriginSpacePos);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    private void Update()
    {
        _painSource.PlayOneShot(_painGrunt);
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    bool CheckIfGrounded()
    {
        //indicates we are on the ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}

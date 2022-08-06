using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private float jumpForce = 500.0f;

    private XROrigin _xrOrigin;
    private CapsuleCollider _collider;
    private Rigidbody _body;

    private bool IsGrounded => Physics.Raycast(new Vector2(transform.position.x, transform.position.y + 2.0f), Vector3.down, 2.0f);
    // Start is called before the first frame update
    void Start()
    {
        _xrOrigin = GetComponent<XROrigin>();
        _collider = GetComponent<CapsuleCollider>();
        _body = GetComponent<Rigidbody>();
        jumpActionReference.action.performed += OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        var center = _xrOrigin.CameraInOriginSpacePos;
        _collider.height = Mathf.Clamp(_xrOrigin.CameraInOriginSpaceHeight, 1.0f, 3.0f);
        _collider.center = new Vector3(center.x, _collider.height / 2, center.z);
    }
    private void OnJump(InputAction.CallbackContext obj)
    {
        if (!IsGrounded) return;
        _body.AddForce(Vector3.up * jumpForce);
    }
}

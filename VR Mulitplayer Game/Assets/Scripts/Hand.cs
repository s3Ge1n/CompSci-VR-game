using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    //Animation
    public float animationSpeed;
    Animator animator;
    SkinnedMeshRenderer mesh;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";

    //physics movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    private Transform _followTarget;
    private Rigidbody _body;

    void Start()
    {
        //Animation
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        //Physics Move
        _followTarget = followObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;
        _body.maxAngularVelocity = 20f;

        //tp hands
        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();

        PhysicsMove();
    }
    private void PhysicsMove()
    {
        //position
        var positionWithOffset = _followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        //_body.velocity = (positionWithOffset - transform.position).normalized * followSpeed * distance * Time.deltaTime;
        _body.velocity = (positionWithOffset - transform.position).normalized * followSpeed * distance;


        //rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);

        if (Mathf.Abs(axis.magnitude) != Mathf.Infinity)
        {
            if (angle > 180.0f) { angle -= 360.0f; }
            //_body.angularVelocity = angle * axis * Mathf.Deg2Rad * rotateSpeed * Time.deltaTime;
            _body.angularVelocity = angle * axis * Mathf.Deg2Rad * rotateSpeed;
        }
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    void AnimateHand()
    {
        if (Math.Abs(gripCurrent - gripTarget) > 0)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if (!(Math.Abs(triggerCurrent - triggerTarget) > 0)) return;

        triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
        animator.SetFloat(animatorTriggerParam, triggerCurrent);  

    }

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = !c.enabled;
        }
    }
}

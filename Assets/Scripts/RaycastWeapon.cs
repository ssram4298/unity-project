using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.Linq;

public class RaycastWeaponController : XRGrabInteractable
{
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    public Transform rayCastOrigin;
    public float range = 100f;

    private Ray ray;
    private RaycastHit hitInfo;
    private bool isFiring = false;

    protected override void Awake()
    {
        base.Awake();
        selectEntered.AddListener(HandleSelectEntered);
        selectExited.AddListener(HandleSelectExited);
    }

    protected override void OnDestroy()
    {
        selectEntered.RemoveListener(HandleSelectEntered);
        selectExited.RemoveListener(HandleSelectExited);
        base.OnDestroy();
    }

    private void Update()
    {
        if (isSelected)
        {
            var inputDevices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);
            var rightHandDevice = inputDevices.FirstOrDefault();

            if (rightHandDevice.isValid)
            {
                if (rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
                {
                    StartFiring();
                }
                else
                {
                    StopFiring();
                }
            }
        }
    }

    public void StartFiring()
    {
        if (!isFiring)
        {
            isFiring = true;
            foreach (var particle in muzzleFlash)
            {
                particle.Emit(1);
            }
            PerformRaycast();
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    private void PerformRaycast()
    {
        ray.origin = rayCastOrigin.position;
        ray.direction = rayCastOrigin.forward;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo, range))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.blue, 5.0f);

            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
        }
    }

    private void HandleHit()
    {
        // Implement hit handling logic here
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        // Optionally, handle logic when the weapon is grabbed
    }

    private void HandleSelectExited(SelectExitEventArgs args)
    {
        StopFiring();  // Ensure firing stops when the gun is dropped
    }

    void OnDrawGizmos()
    {
        if (isFiring)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, hitInfo.point);
        }
    }
}

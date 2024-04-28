using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.Linq;

public class RaycastWeaponController : XRGrabInteractable
{
    [Header("Effects")]
    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public ParticleSystem enemyHitEffect;
    public ParticleSystem hologramHitEffect;
    public TrailRenderer tracerEffect;

    [Header("Raycast Settings")]
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
            tracer.transform.position = hitInfo.point;

            HandleHit();
        }
    }

    private void HandleHit()
    {
        // Implement hit handling logic here
        if (hitInfo.collider.CompareTag("Bot"))
        {
            // Instantiate special effects for hitting an enemy
            Instantiate(enemyHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            // Attempt to get the BotHealth component and call TakeDamage
            var enemyHealth = hitInfo.collider.GetComponent<EnemyBot>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(5);
            }
        }
        else if (hitInfo.collider.CompareTag("AdvancedBot"))
        {
            // Instantiate special effects for hitting an enemy
            Instantiate(enemyHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            // Attempt to get the BotHealth component and call TakeDamage
            var enemyHealth = hitInfo.collider.GetComponent<AdvancedEnemyBot>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(5);
            }
        }
        else if (hitInfo.collider.CompareTag("Boss"))
        {
            // Instantiate special effects for hitting an enemy
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            // Attempt to get the BotHealth component and call TakeDamage
            var enemyHealth = hitInfo.collider.GetComponent<BossController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(100);
            }
        }
        else if (hitInfo.collider.CompareTag("Enemy"))
        {
            // Instantiate special effects for hitting a hologram
            Instantiate(enemyHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            // Set the hologram object inactive
            var enemyHealth = hitInfo.collider.GetComponent<BotHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(5);
            }
        }
        else if (hitInfo.collider.CompareTag("Hologram"))
        {
            // Instantiate special effects for hitting a hologram
            hologramHitEffect.transform.position = hitInfo.point;
            hologramHitEffect.transform.forward = hitInfo.normal;
            hologramHitEffect.Emit(1);
            // Set the hologram object inactive
            var holoBot = hitInfo.collider.GetComponent<HoloBot>();
            if (holoBot != null)
            {
                holoBot.IncrementCounter();
            }
        }
        else
        {
            // Normal hit effect
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);
        }

        // Display tracer effect endpoint
        var tracerEnd = Instantiate(tracerEffect, hitInfo.point, Quaternion.identity);
        tracerEnd.AddPosition(hitInfo.point);
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        // Optionally, handle logic when the weapon is grabbed
    }

    private void HandleSelectExited(SelectExitEventArgs args)
    {
        StopFiring();  // Ensure firing stops when the gun is dropped
    }
}

using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.Linq;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;  // Drag your bullet prefab here in the Inspector
    public Transform bulletSpawn;    // Assign a child GameObject as the spawn point
    public GameObject crosshair;     // Assign the crosshair UI GameObject in the Inspector
    public float shootingForce = 1500f;
    public float bulletLifeTime = 3f;
    public float fireRate = 0.5f;    // Time between shots, in seconds
    private bool isGunHeld = false;
    private float nextTimeToFire = 0f; // When is the next time we're allowed to fire
    private XRGrabInteractable grabInteractable;
    public LayerMask aimLayerMask;   // Assign a layer mask to filter which objects should be aimed at

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HandleSelectEntered);
        grabInteractable.selectExited.AddListener(HandleSelectExited);
        if (crosshair != null)
            crosshair.SetActive(false);  // Ensure the crosshair is hidden by default
    }

    void Update()
    {
        if (isGunHeld && Time.time >= nextTimeToFire)
        {
            var inputDevices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);
            var rightHandDevice = inputDevices.FirstOrDefault();

            if (rightHandDevice.isValid)
            {
                if (rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
                {
                    nextTimeToFire = Time.time + fireRate; // Set the next time to fire
                    Shoot();
                }
            }
        }

        if (isGunHeld)
        {
            UpdateCrosshairPosition();
        }
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        isGunHeld = true;
    }

    private void HandleSelectExited(SelectExitEventArgs args)
    {
        isGunHeld = false;
        if (crosshair != null)
            crosshair.SetActive(false);  // Hide the crosshair when the gun is not held
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawn.forward * shootingForce);
        Destroy(bullet, bulletLifeTime); // Destroy bullet after a set time to clean up
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(HandleSelectEntered);
            grabInteractable.selectExited.RemoveListener(HandleSelectExited);
        }
    }

    private void UpdateCrosshairPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, 100f, aimLayerMask))
        {
            if (crosshair != null)
            {
                crosshair.SetActive(true);
                crosshair.transform.position = hit.point + hit.normal * 0.01f; // Position slightly off the surface to avoid z-fighting
                crosshair.transform.forward = -hit.normal; // Make the crosshair face toward the camera if it's a 3D object
            }
        }
        else
        {
            if (crosshair != null)
                crosshair.SetActive(false);
        }
    }
}

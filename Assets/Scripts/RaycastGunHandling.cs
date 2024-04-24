using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.Linq;

public class RaycastGunHandling: MonoBehaviour
{
    private bool isGunHeld = false;
    private XRGrabInteractable grabInteractable;

    //public RaycastWeapon weapon;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HandleSelectEntered);
        grabInteractable.selectExited.AddListener(HandleSelectExited);
    }

    void Update()
    {
        if (isGunHeld)
        {
            var inputDevices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);
            var rightHandDevice = inputDevices.FirstOrDefault();

            if (rightHandDevice.isValid)
            {
                if (rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
                {
                    //weapon.StartFiring();
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
    }

    void Shoot()
    {
    
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
    }
}

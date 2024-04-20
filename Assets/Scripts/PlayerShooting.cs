using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 200f;
    public float bulletMass = 2f;
    public float maxSpeed = 500f;
    private bool isGunHeld = false;

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HandleSelectEntered);
        grabInteractable.selectExited.AddListener(HandleSelectExited);
    }

    void Update()
    {
        if (isGunHeld && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = firePoint.forward * bulletSpeed;
        bulletRigidbody.useGravity = true;
        bulletRigidbody.mass = bulletMass;
        bulletRigidbody.drag = 0.5f;
        bulletRigidbody.angularDrag = 0.5f;

        bulletRigidbody.velocity = Vector3.ClampMagnitude(bulletRigidbody.velocity, maxSpeed);
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        isGunHeld = true;
    }

    private void HandleSelectExited(SelectExitEventArgs args)
    {
        isGunHeld = false;
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(HandleSelectEntered);
            grabInteractable.selectExited.RemoveListener(HandleSelectExited);
        }
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RaycastEnemyWeapon : XRGrabInteractable
{
    public ParticleSystem[] muzzleFlash;
    //public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    public Transform rayCastOrigin;
    public Transform playerTarget;  // Target the player's transform
    public float range = 100f;
    public float fireRate = 2.0f;  // Time between shots

    private Ray ray;
    private RaycastHit hitInfo;
    private bool isFiring = false;
    private float nextFireTime = 0f;

    private void Update()
    {
        // Automatically fire at random intervals
        if (Time.time > nextFireTime)
        {
            
            StartFiring();
            nextFireTime = Time.time + fireRate + Random.Range(0.0f, 2.0f);  // Randomize firing rate
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
        ray.direction = (playerTarget.position - ray.origin).normalized;  // Direct raycast towards the player

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo, range))
        {
            Debug.Log("Enemy Fired a Gun");
            Debug.DrawLine(ray.origin, hitInfo.point, Color.blue, 5.0f);

            //hitEffect.transform.position = hitInfo.point;
            //hitEffect.transform.forward = hitInfo.normal;
            //hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
        }
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

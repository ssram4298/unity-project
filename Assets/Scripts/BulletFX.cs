using UnityEngine;

public class BulletFX : MonoBehaviour
{
    public GameObject bloodSplatterPrefab; // Assign this in the inspector

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Make sure your enemy has an "Enemy" tag
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            
            GameObject splatter = Instantiate(bloodSplatterPrefab, pos, rot);
            splatter.transform.localScale *= 0.5f; // Scale down the splatter to 50% of its original size

            Destroy(gameObject); // Destroy the bullet on impact
        }
    }
}

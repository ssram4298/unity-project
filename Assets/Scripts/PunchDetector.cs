using UnityEngine;
using System.Collections;

public class PunchDetector : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    private Vector3 previousLeftPosition;
    private Vector3 previousRightPosition;
    private Vector3 currentLeftPosition;
    private Vector3 currentRightPosition;
    public float punchThreshold = 3.14f;

    void Update()
    {
        TrackHandMovement(leftHand, ref previousLeftPosition, ref currentLeftPosition);
        TrackHandMovement(rightHand, ref previousRightPosition, ref currentRightPosition);
    }

    private void TrackHandMovement(GameObject hand, ref Vector3 previousPosition, ref Vector3 currentPosition)
    {
        previousPosition = currentPosition;
        currentPosition = hand.transform.position;
        Vector3 velocity = (currentPosition - previousPosition) / Time.deltaTime;
        if (velocity.magnitude > punchThreshold)
        {
            Debug.Log(hand.name + " Punch Detected");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot"))
        {
            Debug.Log("%^(*^*^)*^%*^(&)$&%*^(&^%^^&)*^&^&)((^%^(*&Collision Detected");
            Vector3 velocity = (currentLeftPosition - previousLeftPosition) / Time.deltaTime;
            if (velocity.magnitude > punchThreshold || ((currentRightPosition - previousRightPosition) / Time.deltaTime).magnitude > punchThreshold)
            {
                var enemyBot = collision.gameObject.GetComponent<EnemyBot>();
                if (enemyBot != null)
                {
                    enemyBot.TakeDamage(100);
                }
            }
        }
    }
}


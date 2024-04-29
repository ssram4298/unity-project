using System.Collections;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    public float interactionDistance = 3.0f;
    public GameObject player;
    public NotificationController notificationController;
    public GameController gameController;
    public AudioSource audioSource;

    private Animator npcAnimator;
    private bool isPlayerInRange = false;
    private bool hasInteracted = false;  // Flag to ensure interaction happens only once

    void Start()
    {
        npcAnimator = GetComponent<Animator>();
        if (npcAnimator == null)
        {
            Debug.LogWarning("NPC Animator component not found!", this);
        }

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found!", this);
        }
    }

    void Update()
    {
        CheckPlayerDistance();
        if (isPlayerInRange)
        {
            RotateTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        Debug.Log("Rotating towards player!");
        Vector3 lookPosition = player.transform.position;
        lookPosition.y = transform.position.y; // This ensures the NPC only rotates around the y-axis

        transform.LookAt(lookPosition);
    }

    private void CheckPlayerDistance()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= interactionDistance)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                notificationController.ActivateNotificationArea("Press A to interact!");
            }

            if (Input.GetButtonDown("Fire1") && isPlayerInRange)
            {
                npcAnimator.SetTrigger("Interacted");
                audioSource.Play();  // Play audio after the animation has finished
                hasInteracted = true;  // Set flag to prevent further interactions
                StartCoroutine(PlayAudioAfterAnimation());
                notificationController.DeactivateNotificationArea();
            }
        }
        else if (isPlayerInRange)
        {
            isPlayerInRange = false;
            notificationController.DeactivateNotificationArea();
        }
    }

    private IEnumerator PlayAudioAfterAnimation()
    {
        // Wait for the current animation state to finish
        yield return new WaitForSeconds(npcAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Optionally wait for any transition to finish
        while (npcAnimator.IsInTransition(0))
        {
            yield return null;
        }

        // Wait for the audio to finish playing
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Call the completion function after the animation and audio have fully finished
        CompleteMissionFunction();
    }

    private void CompleteMissionFunction()
    {
        gameController.CompleteMission();
        gameController.Mission5();
    }
}

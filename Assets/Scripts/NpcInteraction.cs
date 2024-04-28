using System.Collections;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    public float interactionDistance = 3.0f;
    public GameObject player;
    public NotificationController notificationController;

    public GameController gameController;
    
    private Animator npcAnimator;
    private bool isPlayerInRange = false;

    void Start()
    {
        // Dynamically get the Animator component attached to the same GameObject as this script
        npcAnimator = GetComponent<Animator>();
        if (npcAnimator == null)
        {
            Debug.LogWarning("NPC Animator component not found!", this);
        }
    }

    void Update()
    {
        CheckPlayerDistance();
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
                Debug.Log("Player Interacted");
                npcAnimator.SetTrigger("Interacted");
                StartCoroutine(WaitForAnimation());
            }
        }
        else if (isPlayerInRange)
        {
            isPlayerInRange = false;
            notificationController.DeactivateNotificationArea();
        }
    }

    private IEnumerator WaitForAnimation()
    {
        // Wait for the current state to finish
        yield return new WaitForSeconds(npcAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Optionally wait for any transition to finish
        while (npcAnimator.IsInTransition(0))
        {
            yield return null;
        }

        // Call the completion function after the animation has fully finished
        CompleteMissionFunction();
    }

    private void CompleteMissionFunction()
    {
        Debug.Log("Mission completed!");
        // Implement mission completion logic here
        gameController.CompleteMission();
        gameController.Mission5();
    }
}

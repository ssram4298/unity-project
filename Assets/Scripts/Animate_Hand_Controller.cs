using UnityEngine;
using UnityEngine.InputSystem;

// Ensure this script is attached to a GameObject that has an Animator component
[RequireComponent(typeof(Animator))]
public class Animate_Hand_Controller : MonoBehaviour
{
    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;

    private Animator _handAnimator;
    private float _gripValue;
    private float _triggerValue;

    private void Start()
    {
        // Initialize the Animator component
        _handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateGrip();
        AnimateTrigger();
    }

    private void AnimateGrip()
    {
        // Read the current value of the grip input and update the Animator's Grip parameter
        _gripValue = gripInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Grip", _gripValue);
    }

    private void AnimateTrigger()
    {
        // Read the current value of the trigger input and update the Animator's Trigger parameter
        _triggerValue = triggerInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);
    }
}

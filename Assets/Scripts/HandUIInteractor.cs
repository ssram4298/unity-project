using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class HandUIInteractor : MonoBehaviour
{
    public XRController controller;
    public XRInteractorLineVisual lineVisual;

    private void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<XRController>();
        }

        if (lineVisual == null)
        {
            lineVisual = GetComponentInChildren<XRInteractorLineVisual>();
        }
    }

    private void Update()
    {
        controller.gameObject.SetActive(controller.enableInputActions);
        UpdateLineVisual();
    }

    private void UpdateLineVisual()
    {
        if (lineVisual != null)
        {
            lineVisual.enabled = controller.enableInputActions;
        }
    }
}

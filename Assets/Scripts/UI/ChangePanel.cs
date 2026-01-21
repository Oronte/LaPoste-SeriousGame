using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    [SerializeField] private GameObject panelToActivate;
    [SerializeField] private GameObject panelToDeactivate;

    public void ActivatePanel()
    {
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Panel to activate is not assigned.");
        }
    }

    public void DeactivatePanel()
    {
        if (panelToDeactivate != null)
        {
            panelToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Panel to deactivate is not assigned.");
        }
    }
}

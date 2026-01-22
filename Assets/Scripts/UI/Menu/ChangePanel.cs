using System.Collections.Generic;
using UnityEngine;


public class ChangePanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> panelToActivate;
    [SerializeField] private List<GameObject> panelToDeactivate;
    public void ActivatePanel()
    {
        foreach (GameObject _panel in panelToActivate)
        {
            if (_panel != null)
                _panel.SetActive(true);
            else
                Debug.LogWarning("Panel to activate is not assigned.");
        }
    }

    public void DeactivatePanel()
    {
        foreach (GameObject _panel in panelToDeactivate)
        {
            if (_panel != null)
                _panel.SetActive(false);
            else
                Debug.LogWarning("Panel to deactivate is not assigned.");
        }
    }
}

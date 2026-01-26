using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IHM : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] GameObject vacation = null;
    [SerializeField] Toggle piloteButton = null;
    [SerializeField] GameObject profilPanel = null;
    [SerializeField] GameObject validateClosePanel = null;
    [SerializeField] Button validateButton = null;
    [SerializeField] GameObject starPanel = null;
    [SerializeField] Toggle configButton = null;
    [SerializeField] List<Button> orgaButtons = new List<Button>();

    [Header("Config", order = 1)]
    [SerializeField] string startVacation = "Debuter Vacation";
    [SerializeField] string endVacation = "Terminer Vacation";
    [SerializeField] int numberOfTry = 3;

    bool isLogged = false;
    int nbrTry = 0;

    public void Login()
    {
        if (isLogged || !piloteButton) return;

        SetLoginState(true);
        SetProfilPanelVisibility(false);
        if (starPanel)
        {
            starPanel.SetActive(true);
            if (configButton) configButton.interactable = true;
        }

    }

    public void EndVacation()
    {
        if (!isLogged) return;

        SetLoginState(false);
    }

    void SetLoginState(bool _logged)
    {
        if (vacation)
        {
            TextMeshProUGUI _vacationText = vacation.GetComponentInChildren<TextMeshProUGUI>();
            _vacationText.text = _logged ? endVacation : startVacation;
            isLogged = _logged;
        }
    }

    public void SetProfilPanelVisibility(bool _value)
    {
        if (profilPanel) profilPanel.SetActive(_value);
        if (validateClosePanel) validateClosePanel.SetActive(_value);
    }

    public void CloseProfilPanel()
    {
        if (isLogged) return;
        SetProfilPanelVisibility(false);
        if (vacation)
        {
            Toggle _vacationButton = vacation.GetComponentInChildren<Toggle>();
            if (_vacationButton) _vacationButton.isOn = false;
        }
    }

    public void SetAllOrgaButtons(bool _value)
    {
        foreach (Button _orga in orgaButtons)
        {
            _orga.gameObject.SetActive(_value);
            if (_orga.TryGetComponentInChildren(out RandomWidgetMove _random))
            {
                //_random.ToggleRandomMove(_value);
            }
        }
    }

    public void RemoveTry()
    {
        nbrTry += 1;
        //if (nbrTry >= numberOfTry)  Todo Lose
    }
}

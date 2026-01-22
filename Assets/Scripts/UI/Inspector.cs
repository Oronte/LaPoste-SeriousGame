using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inspector : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] TextMeshProUGUI dialogText = null;

    [Header("Config", order = 1)]
    [SerializeField] string dialog = "Coucou";

    [Header("Events", order = 2)]
    [SerializeField] UnityEvent onYesClicked = null;
    [SerializeField] UnityEvent onNoClicked = null;


    void Start()
    {
        if (dialogText) dialogText.text = dialog;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnYesClicked()
    {
        onYesClicked?.Invoke();
    }

    public void OnNoClicked()
    {
        onNoClicked?.Invoke();
    }

    public void SetText(string _text)
    {
        dialog = _text;
        if (dialogText) dialogText.text = dialog;
    }
}

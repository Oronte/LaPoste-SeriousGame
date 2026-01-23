using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class QuizButton : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] GameObject button;
    [SerializeField] XRPokeFilter pokeFilter = null;

    [Header("Config", order = 1)]
    [SerializeField] QuizAnswer answer = null;
    public UnityEvent<QuizButton> OnButtonActivated;


    public void SetAnswer(QuizAnswer _answer)
    {
        answer = _answer;
    }

    public QuizAnswer GetAnswer() => answer;

    void Start()
    {
        SetButtonColor(Color.white);
        if(!pokeFilter)
            pokeFilter = GetComponent<XRPokeFilter>();
    }

    public void DisableButton(bool _shouldBlackButton = true)
    {
        if(_shouldBlackButton)
            SetButtonColor(Color.black);
        if (!pokeFilter)
            return;
        pokeFilter.enabled = false;
    }

    public void ResetButton()
    {
        SetButtonColor(Color.white);
        if (!pokeFilter)
            return;
        pokeFilter.enabled = true;
        answer = null;
    }

    public void OnButtonPressed()
    {
        OnButtonActivated.Invoke(this);
    }

    public void SetButtonColor(Color _color)
    {
        button.GetComponent<MeshRenderer>().material.color = _color;
    }

}

using UnityEngine;
using UnityEngine.Events;

public class QuizButton : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] GameObject button;

    [Header("Config", order = 1)]
    [SerializeField] Color color = Color.red;
    [SerializeField] string text = "A";
    [SerializeField] QuizAnswer answer = null;
    public UnityEvent<QuizButton> OnButtonActivated;


    public void SetAnswer(QuizAnswer _answer)
    {
        answer = _answer;
    }
    public QuizAnswer GetAnswer() => answer;

    void Start()
    {
        Setup(color, text);
        SetButtonColor(color);
    }

    public void Setup(Color _color, string _text)
    {
        //if (button) 
    }

    public void ResetState()
    {

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

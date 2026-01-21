using UnityEngine;

public class QuizButton : MonoBehaviour
{
    [Header("References", order = 0)]
    [SerializeField] GameObject button;

    [Header("Config", order = 1)]
    [SerializeField] Color color = Color.white;
    [SerializeField] string text = "A";

    void Start()
    {
        Setup(color, text); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Color _color, string _text)
    {
        //if (button) 
    }
}

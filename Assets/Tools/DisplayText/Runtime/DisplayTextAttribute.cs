using UnityEngine;

public class DisplayTextAttribute : PropertyAttribute
{
    public string Text {  get; private set; }

    public DisplayTextAttribute(string _text)
    {
        this.Text = _text;
    }
}

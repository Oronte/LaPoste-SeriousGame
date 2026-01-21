using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "Scriptable Objects/Dialogue Database")]
public class DialogueDatabase : ScriptableObject
{
    public List<DialogueEntry> entries = new List<DialogueEntry>();

    public DialogueEntry AddEntry()
    {
        int _newId = entries.Count > 0 ? entries[entries.Count - 1].ID + 1 : 0;

        DialogueEntry _entry = new DialogueEntry
        {
            ID = _newId,
            Text = "",
            Audio = null
        };

        entries.Add(_entry);
        return _entry;
    }

    public void RecalculateIDS()
    {
        int _count = entries.Count;
        for (int i = 0; i < _count; i++)
        {
            entries[i].ID = i;
        }
    }
}

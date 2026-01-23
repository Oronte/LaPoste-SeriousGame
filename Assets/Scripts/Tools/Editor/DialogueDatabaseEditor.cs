using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class DialogueDatabaseEditor : EditorWindow
{
    private DialogueDatabase database = null;
    private ReorderableList list = null;
    Vector2 scroll = Vector2.zero;

    [MenuItem("Tools/Dialogue Database Editor")]
    public static void Open()
    {
        GetWindow<DialogueDatabaseEditor>("Dialogue Editor");
    }

    private void OnEnable()
    {
        database.RecalculateIDS();
    }

    private void OnGUI()
    {
        database = (DialogueDatabase)EditorGUILayout.ObjectField("Dialogue Database", database, typeof(DialogueDatabase), false);

        if(!database)
        {
            EditorGUILayout.HelpBox("Sélectionnez un DialogueDatabase pour commencer.", MessageType.Info);
            return;
        }

        EditorGUILayout.Space(5f);

        scroll = EditorGUILayout.BeginScrollView(scroll);

        if (list == null)
        {
            SetupList();
        }

        list.DoLayoutList();

        EditorGUILayout.EndScrollView();
    }

    private void SetupList()
    {
        list = new ReorderableList(database.entries, typeof(DialogueEntry), true, true, true, true);

        list.drawHeaderCallback = (Rect _rect) =>
        {
            EditorGUI.LabelField(_rect, "Liste des dialogues");
        };

        list.elementHeightCallback = (int _index) =>
        {
            return EditorGUIUtility.singleLineHeight * 4 + 20;
        };

        list.drawElementCallback = (Rect _rect, int _index, bool _isActive, bool _isFocused) =>
        {
            DialogueEntry _entry = database.entries[_index];
            float _line = EditorGUIUtility.singleLineHeight;

            _rect.y += 2;

            EditorGUI.LabelField(new Rect(_rect.x, _rect.y, 50, _line), $"ID: {_entry.ID}");

            _entry.Text = EditorGUI.TextField(new Rect(_rect.x + 60, _rect.y, _rect.width - 60, _line), _entry.Text);

            _entry.Icon = (Sprite)EditorGUI.ObjectField(new Rect(_rect.x, _rect.y + _line + 4, _rect.width, _line), "Icon", _entry.Icon, typeof(Sprite), false);
            
            _entry.Audio = (AudioClip)EditorGUI.ObjectField(new Rect(_rect.x, _rect.y + (_line + 4) * 2, _rect.width, _line), "Audio", _entry.Audio, typeof(AudioClip), false);
        };

        list.onAddCallback = (ReorderableList _list) =>
        {
            Undo.RecordObject(database, "Add Dialogue Entry");
            database.AddEntry();
            database.RecalculateIDS();
        };


        list.onRemoveCallback = (ReorderableList _list) =>
        {
            Undo.RecordObject(database, "Remove Dialogue Entry");
            database.entries.RemoveAt(_list.index);
            database.RecalculateIDS();
        };

        list.onReorderCallback = (ReorderableList _list) =>
        {
            Undo.RecordObject(database, "Reorder Dialogue Entries");
            database.RecalculateIDS();
        };
    }
}

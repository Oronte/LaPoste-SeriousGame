using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class QuizDatabaseEditor : EditorWindow
{
    private QuizDatabase database = null;
    private ReorderableList list = null;
    Vector2 scroll = Vector2.zero;

    [MenuItem("Tools/Quiz Database Editor")]
    public static void Open()
    {
        GetWindow<QuizDatabaseEditor>("Quiz Editor");
    }

    private void OnGUI()
    {
        database = (QuizDatabase)EditorGUILayout.ObjectField("Quiz Database", database, typeof(QuizDatabase), false);

        if(!database)
        {
            EditorGUILayout.HelpBox("Sélectionnez un QuizDatabase pour commencer.", MessageType.Info);
            return;
        }

        scroll = EditorGUILayout.BeginScrollView(scroll);

        DrawGlobalColors();

        if(list == null)
        {
            SetupList();
        }

        list.DoLayoutList();

        EditorGUILayout.EndScrollView();
    }

    private void DrawGlobalColors()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Couleurs globales", EditorStyles.boldLabel);

        database.ColorA = EditorGUILayout.ColorField("Couleur A", database.ColorA);
        database.ColorB = EditorGUILayout.ColorField("Couleur B", database.ColorB);
        database.ColorC = EditorGUILayout.ColorField("Couleur C", database.ColorC);
        database.ColorD = EditorGUILayout.ColorField("Couleur D", database.ColorD);

        EditorGUILayout.Space(5f);
    }

    private void SetupList()
    {
        list = new ReorderableList(database.questions, typeof(QuizQuestion), true, true, true, true);

        list.drawHeaderCallback = _rect =>
        {
            EditorGUI.LabelField(_rect, "Questions du quiz");
        };

        list.elementHeightCallback = _index =>
        {
            return EditorGUIUtility.singleLineHeight * 8 + 20;
        };

        list.drawElementCallback = (_rect, _index, active, _focused) =>
        {
            QuizQuestion _question = database.questions[_index];
            float _line = EditorGUIUtility.singleLineHeight;
            _rect.y += 2;

            EditorGUI.LabelField(new Rect(_rect.x, _rect.y, 50, _line), $"ID: {_question.ID}");
            _question.QuestionText = EditorGUI.TextField(new Rect(_rect.x + 60, _rect.y, _rect.width - 60, _line), _question.QuestionText);

            _question.IsMultipleAnswers = EditorGUI.ToggleLeft(new Rect(_rect.x, _rect.y + _line + 4, _rect.width, _line), "Réponses multiples", _question.IsMultipleAnswers);

            string[] _labels = { "A", "B", "C", "D" };
            Color[] _colors = { database.ColorA, database.ColorB, database.ColorC, database.ColorD };

            for (int i = 0; i < 4; i++)
            {
                float _y = _rect.y + (i + 2) * (_line + 4);

                EditorGUI.LabelField(new Rect(_rect.x, _y, 20, _line), _labels[i]);
                _question.Answers[i].Text = EditorGUI.TextField(new Rect(_rect.x + 25, _y, _rect.width - 120, _line), _question.Answers[i].Text);

                GUI.color = _colors[i];
                bool _newValue = EditorGUI.Toggle(new Rect(_rect.x + _rect.width - 80, _y, 20, _line), _question.CorrectAnswers[i]);
                GUI.color = Color.white;

                if (_newValue != _question.CorrectAnswers[i])
                {
                    Undo.RecordObject(database, "Modify Correct Answers");

                    if (!_question.IsMultipleAnswers)
                    {
                        for (int j = 0; j < 4; j++)
                        { 
                            _question.CorrectAnswers[j] = false;
                        }

                        _question.CorrectAnswers[i] = _newValue;
                    }
                    else
                    {
                        _question.CorrectAnswers[i] = _newValue;
                    }

                    EditorUtility.SetDirty(database);
                }
            }
        };

        list.onAddCallback = _list =>
        {
            Undo.RecordObject(database, "Add Quiz Question");
            database.AddQuestion();
            database.RecalculateIDS();
        };

        list.onRemoveCallback = _list =>
        {
            Undo.RecordObject(database, "Remove Quiz Question");
            database.questions.RemoveAt(_list.index);
            database.RecalculateIDS();
        };

        list.onReorderCallback = _list =>
        {
            Undo.RecordObject(database, "Reorder Quiz Questions");
            database.RecalculateIDS();
        };
    }
}

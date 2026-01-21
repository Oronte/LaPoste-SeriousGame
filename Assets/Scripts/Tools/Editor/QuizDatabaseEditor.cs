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

        if(list == null)
        {
            SetupList();
        }

        list.DoLayoutList();

        EditorGUILayout.EndScrollView();
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
            QuizQuestion _question = database.questions[_index];
            float _line = EditorGUIUtility.singleLineHeight;

            return _line * 2 + (_question.answers.Count * (_line + 4)) + _line + 30;
        };

        list.drawElementCallback = (_rect, _index, active, _focused) =>
        {
            QuizQuestion _question = database.questions[_index];
            float _line = EditorGUIUtility.singleLineHeight;
            _rect.y += 2;

            EditorGUI.LabelField(new Rect(_rect.x, _rect.y, 50, _line), $"ID: {_question.ID}");
            _question.QuestionText = EditorGUI.TextField(new Rect(_rect.x + 60, _rect.y, _rect.width - 60, _line), _question.QuestionText);


            _question.IsMultipleAnswers = EditorGUI.ToggleLeft(new Rect(_rect.x, _rect.y + _line + 4, _rect.width, _line), "Réponses multiples", _question.IsMultipleAnswers);

            float _y = _rect.y + _line * 2 + 10;


            int _answerCount = _question.answers.Count;
            for (int i = 0; i < _answerCount; i++)
            {
                QuizAnswer _answer = _question.answers[i];

                EditorGUI.BeginChangeCheck();

                _answer.Text = EditorGUI.TextField(new Rect(_rect.x, _y, _rect.width - 150, _line), _answer.Text);

                _answer.AnswerColor = EditorGUI.ColorField(new Rect(_rect.x + _rect.width - 145, _y, 60, _line), _answer.AnswerColor);

                bool _newCorrect = EditorGUI.Toggle(new Rect(_rect.x + _rect.width - 80, _y, 20, _line), _answer.IsCorrect);

                if(EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(database, "Modify Answer");

                    if(_newCorrect && !_question.IsMultipleAnswers)
                    {
                        foreach(QuizAnswer _ans in _question.answers)
                        {
                            _ans.IsCorrect = false;
                        }
                    }

                    _answer.IsCorrect = _newCorrect;
                    EditorUtility.SetDirty(database);
                }

                if(GUI.Button(new Rect(_rect.x + _rect.width - 50, _y, 50, _line), "Suppr"))
                {
                    Undo.RecordObject(database, "Remove Answer");
                    _question.answers.RemoveAt(i);
                    EditorUtility.SetDirty(database);
                    return;
                }

                _y += _line + 4;
            }

            GUI.enabled = _answerCount < 4;

            if(GUI.Button(new Rect(_rect.x, _y, 150, _line), "Ajouter une réponse"))
            {
                Undo.RecordObject(database, "Add Answer");
                _question.answers.Add(new QuizAnswer());
                EditorUtility.SetDirty(database);
            }

            GUI.enabled = true;
        };

        list.onAddCallback = _list =>
        {
            Undo.RecordObject(database, "Add Quiz Question");
            database.AddQuestion();
            database.RecalculateIDS();
            EditorUtility.SetDirty(database);
        };

        list.onRemoveCallback = _list =>
        {
            Undo.RecordObject(database, "Remove Quiz Question");
            database.questions.RemoveAt(_list.index);
            database.RecalculateIDS();
            EditorUtility.SetDirty(database);
        };

        list.onReorderCallback = _list =>
        {
            Undo.RecordObject(database, "Reorder Quiz Questions");
            database.RecalculateIDS();
            EditorUtility.SetDirty(database);
        };
    }
}
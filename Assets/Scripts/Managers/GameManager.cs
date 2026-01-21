using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [VisibleAnywhereProperty, Tooltip("Score maximal de tous les mini jeux du module")] public float maxScore = 0.0f;

    //Collection de tous les minijeu
    [SerializeField, VisibleAnywhereProperty, Tooltip("Tous les mini jeux")] List<MiniGame> allMinigames = new List<MiniGame>();
    //Index du jeu actuel
    [SerializeField, VisibleAnywhereProperty, Tooltip("Index du jeu actuel")] int currentGame = 0;
    //Score final actuel
    [SerializeField, VisibleAnywhereProperty, Tooltip("Score final actuel")] float finalScore = 0;
    //Joueur du niveau
    [SerializeField, VisibleAnywhereProperty, Tooltip("Joueur du niveau")] Player currentPlayer = null;

    /* Accesseurs */
    //Savoir si le module est fini si l'index du currentGame > au nombre de mini jeu dans la liste
    public bool IsModuleFinished => currentGame >= allMinigames.Count;
    public float FinalScore => finalScore;
    public MiniGame CurrentMiniGame => IsModuleFinished ? null : allMinigames[currentGame];

    public Player CurrentPlayer => currentPlayer;

    void Start()
    {
        InitMiniGames();
        InitPlayer();
        StartCurrentMiniGame();
    }

    /// <summary>
    /// Récupere le joueur et set sa variable
    /// </summary>
    void InitPlayer()
    {
        currentPlayer = FindAnyObjectByType<Player>();
    }

    /// <summary>
    /// Récupere la liste de tous les minijeu
    /// </summary>
    void InitMiniGames()
    {
        allMinigames.Clear();
        currentGame = 0;
        allMinigames = FindObjectsByType<MiniGame>(FindObjectsSortMode.None).ToList();
        foreach (MiniGame _game in allMinigames)
        {
            maxScore += _game.MaxScore;
        }
    }
    /// <summary>
    /// Change l'index du MiniJeu actuel pour passer au suivant
    /// </summary>
    void IncrementIndex()
    {
        ++currentGame;
    }


    void Update()
    {
        if (IsModuleFinished) return;
        CheckAndUpdateCurrentMiniGameFinished();
    }

    /// <summary>
    /// Regarde si le mini jeu actuel est fini, si oui il stop le mini jeu actuel, ajoute le score et lance 
    /// </summary>
    void CheckAndUpdateCurrentMiniGameFinished()
    {
        MiniGame _currentMiniGame = CurrentMiniGame;
        if (!_currentMiniGame) return;
        if (!_currentMiniGame.IsRunning) StartCurrentMiniGame();
        if (_currentMiniGame.IsFinished)
        {
            _currentMiniGame.StopGame();
            finalScore += _currentMiniGame.Score;
            IncrementIndex();
        }
    }
    /// <summary>
    /// Lance le Mini Jeu Actuel
    /// </summary>
    void StartCurrentMiniGame()
    {
        MiniGame _currentMiniGame = CurrentMiniGame;
        if (!_currentMiniGame) return;
        _currentMiniGame.StartGame();
    }

    public override string ToString()
    {
        return "Gestionaire de Jeu";
    }
    /// <summary>
    /// Permet de récuperer un mini jeu selon son type
    /// </summary>
    /// <typeparam name="T">Type enfant de MiniJeu dont on veut récupérer</typeparam>
    /// <returns>Mini jeu rechercher, null si non trouvé</returns>
    public T GetMiniGame<T>() where T : MiniGame, new()
    {
        int _size = allMinigames.Count;
        T _miniGameToReturn = null;
        for (int i = 0; i < _size; i++)
        {
            MiniGame _current = allMinigames[i];
            if (!_current) continue;
            if(_current.GetType() == typeof(T))
            {
                _miniGameToReturn = _current as T;
                break;
            }
        }
        return _miniGameToReturn;
    }
}

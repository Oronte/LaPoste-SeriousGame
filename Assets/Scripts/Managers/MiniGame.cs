using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    //Position de depart du mini jeu
    [SerializeField, ToggleVisibleAnywhereProperty, Tooltip("Position de depart du mini jeu")] Transform spawnPosition = null;
    //Est ce que le mini jeu est en cours ?
    [SerializeField, VisibleAnywhereProperty, Tooltip("Est ce que le mini jeu est en cours ?")] bool isRunning = false;
    //Le nombre de point maxiumum que l'on peut obtenir dans le minijeu
    [SerializeField, ToggleVisibleAnywhereProperty, Tooltip("Le nombre de point maxiumum que l'on peut obtenir dans le minijeu")] float maxScore = 1.0f;
    //Le nombre de point actuel dans le minijeu
    [SerializeField, VisibleAnywhereProperty, Tooltip("Le nombre de point actuel dans le minijeu")] float score = 0.0f;
    public float Score
    { 
        get => score;
        set
        {
            score = Mathf.Clamp(value, 0.0f, maxScore);
        }
    }

    public bool IsRunning => isRunning;
    public float MaxScore => maxScore;
    public Vector3 PositionToSpawn => spawnPosition.position;
    public abstract bool IsFinished { get; }
    /// <summary>
    /// Fait apparaitre le joueur
    /// </summary>
    protected void SpawnPlayerAtPos()
    {
        if (!GameManager.Instance) return;
        Player _player = GameManager.Instance.CurrentPlayer;
        if (!_player | !spawnPosition) return;
        _player.transform.position = spawnPosition.position;
    }
    /// <summary>
    /// Fonction appeller lors du lancement du mini jeu
    /// </summary>
    public virtual void StartGame()
    {
        isRunning = true;
        SpawnPlayerAtPos();
    }
    /// <summary>
    /// Fonction appeller lors de l'arret du mini jeu
    /// </summary>
    public virtual void StopGame()
    {
        isRunning = false;
    }
}

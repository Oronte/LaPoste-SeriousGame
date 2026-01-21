using UnityEngine;

public class BatterieTimingGame : MiniGame
{
    [SerializeField, VisibleAnywhereProperty, Tooltip("\"True\" si le jeu est fini ")] 
    bool isFinished = false;

    public override bool IsFinished => isFinished;

    [SerializeField, Tooltip("Définis le temps de réaction décalé en milliseconde")]
    float reactionDelay = 300.0f;
    [SerializeField, Tooltip("Définis le temps en seconde d'un cycle (aller retours complets)")]
    float timePerCycle = 2.0f;
    [SerializeField, Tooltip("Définis le nombre de cycle maximum")]
    int maxCycle = 4;
    [SerializeField, VisibleAnywhereProperty, Tooltip("Est le nombre de cycle effectuer depuis le début")]
    int cycleCount = 0;


    //Component references
    [VisibleAnywhereProperty] TB_FeedbackComponent feedback;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        feedback = GetComponent<TB_FeedbackComponent>();
        if (!feedback) return;

        feedback.PlaySound("Button Pop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartGame()
    {
        base.StartGame();
        isFinished = false;
        cycleCount = 0;
        Score = 0.0f;
        Debug.Log("start game betterie timing game ");
    }
}

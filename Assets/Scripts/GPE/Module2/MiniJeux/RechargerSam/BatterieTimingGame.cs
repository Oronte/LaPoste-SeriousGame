using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BatterieTimingGame : MiniGame
{
    [SerializeField, VisibleAnywhereProperty, Tooltip("\"True\" si le jeu est fini ")] 
    bool isFinished = false;

    public override bool IsFinished => isFinished;

    //reaction parameters
    [SerializeField, Tooltip("Définis le temps de réaction décalé en milliseconde")]
    float reactionDelay = 300.0f;

    //speed parameters
    [SerializeField, Tooltip("\"True\" si une courbe est utilisé pour definir la vitesse du mouvement de maniere plus precise")]
    bool useCurveSpeed = false;
    [SerializeField, Tooltip("Définis la vitesse "),  HideCondition("useCurveSpeed", _invert: true), Range(1, 20)]
    float speed = 2.0f;
    [SerializeField, Tooltip("Courbe definissant la vitesse du mouvement de la barre"),HideCondition("useCurveSpeed")]
    AnimationCurve speedCurve = null;

    //cycle parameters
    [SerializeField, Tooltip("Définis le nombre de cycle maximum")]
    int maxCycle = 4;
    [SerializeField, VisibleAnywhereProperty, Tooltip("Est le nombre de cycle effectuer depuis le début")]
    int cycleCount = 0;
    [SerializeField, Tooltip("temps en ms après lequel la barre s'arrète une fois le dernier cycle terminé")]
    int delayAfterLastCycle = 0;


    //internal variables for movement
    bool canMove = false;
    Vector2 barPos = Vector2.zero;
    float minMaxHeight = 0.0f;
    [SerializeField, VisibleAnywhereProperty]
    bool invert = false;
    [SerializeField, VisibleAnywhereProperty]
    bool isUp = false;



    //Component references
    [SerializeField, VisibleAnywhereProperty]
    TB_FeedbackComponent feedback;
    [SerializeField, Tooltip("Reference vers le bouton d'arret de la barre")] 
    TB_StopButton stopButton;

    //UI References
    [SerializeField, Tooltip("Reference vers le boitier du minijeu")]
    Image container = null;
    [SerializeField, Tooltip("Reference vers la barre rouge du minijeu")]
    TB_RedBar redBar = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        feedback = GetComponent<TB_FeedbackComponent>();
        if (!feedback) return;

        feedback.PlaySound("Button Pop");

        Init();
    }

    void Update()
    {
        MoveRedBar();
    }

    void Init()
    {
        if (!container || !redBar) return;
        redBar.Init();
        if (!redBar.RedBarImage) return;

        // calculate the min and max height for the red bar movement
        minMaxHeight = (container.rectTransform.rect.height / 2);
        barPos = redBar.RedBarImage.rectTransform.rect.position;

        //initialize the stop button
        if (!stopButton) return;
        stopButton.Init(this);

        //reset variables
        isFinished = false;
        cycleCount = 0;
        Score = 0.0f;
        canMove = true;

    }

    public override void StartGame()
    {
        base.StartGame();
       
    }

    void OnBatteryFlat()
    {
        canMove = false;
        isFinished = true;
    }

    void MoveRedBar()
    {
        if (!canMove) return;
        //_midBarSize is the half size of the red bar
        float _midBarSize = redBar.RedBarImage.rectTransform.rect.height;
        //_pos is the current position of the red bar center
        float _pos = redBar.RedBarImage.rectTransform.localPosition.y - (invert ? _midBarSize : 0) ;

        //_min and _max are the limits of the movement
        float _min = barPos.y + minMaxHeight * -1.0f;
        float _max = barPos.y + minMaxHeight ;

        //_dist is the distance from the current position to the next limit
        float _dist = Mathf.Abs(_pos - (invert ? _max : _min));
        //_distMax is the distance between the two limits
        float _distMax = minMaxHeight * 2;
        float _speed = (useCurveSpeed ? speedCurve.Evaluate(_dist/ _distMax) : speed) * Time.deltaTime;

        //update the position of the red bar
        redBar.RedBarImage.rectTransform.position += new Vector3(0, invert ? -_speed : _speed, 0);
        Vector3 _containerPos = container.rectTransform.rect.position;

        
        CheckIsOut(_pos, _min, _max);
        CheckIsFinish();

    }

    void CheckIsOut(float _pos, float _min, float _max)
    {
        //if the red bar is out the bouds,
        //invert the movement direction and add one to the cycle count if it's at the max
        bool _outMax = _pos >= _max;
        bool _outMin = _pos <= _min;

        if (_outMax || _outMin)
        {

            invert = _outMax;
            if (!isUp)
            {
                cycleCount++;
                Debug.Log("Cycle Count: " + cycleCount);
            }

            isUp = invert;
        }
    }

    void CheckIsFinish()
    {
        //if the cycle count reach the max cycle, the game is finished after a delay
        if (cycleCount >= maxCycle)
        {
            Invoke(nameof(OnBatteryFlat), delayAfterLastCycle * 0.001f);
            Debug.Log("Game Finished" );
        }
    }

    public void StopRedBar(ActivateEventArgs _agrs)
    {
        canMove = false;
        CheckGoodZone();
    }

    void CheckGoodZone()
    {
        Debug.Log("Ca marche !!!");
    }
}

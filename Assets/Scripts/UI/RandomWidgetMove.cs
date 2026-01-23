using UnityEngine;
using UnityEngine.UIElements;

public class RandomWidgetMove : MonoBehaviour
{
    //Lancement du move aléatoire au lancement
    [field: SerializeField, ToggleVisibleAnywhereProperty, Tooltip("Lancement du move aléatoire au lancement")] 
    public bool LaunchOnStart { get; set; } = false;
    //Est actuellement en cours
    [field: SerializeField, VisibleAnywhereProperty, Tooltip("Est actuellement en cours")]
    public bool IsRunning { get; protected set; } = false;
    //RectTransform du widget a déplacer (Récupérer automatiquement au Lancement)
    [SerializeField, ToggleVisibleAnywhereProperty, Tooltip("RectTransform du widget a déplacer (Récupérer automatiquement au Lancement)")]
    RectTransform widgetTransform = null;
    //"Frequence entre les différent movement aléatoire
    [SerializeField, ToggleVisibleAnywhereProperty, Tooltip("Frequence entre les différent movement aléatoire")]
    float frequency = 0.1f;
    //Rayon maximum pour le move
    [SerializeField, ToggleVisibleAnywhereProperty, Tooltip("Rayon maximum pour le move")]
    float randomMoveRadius = 0.1f;
    //Choix du type de translation au movement
    [SerializeField, ToggleVisibleAnywhereProperty, Tooltip("Choix du type de translation au movement")]
    Space translateType = Space.World;
    //Distance max de déplacement
    [SerializeField, ToggleVisibleAnywhereProperty, Tooltip("Distance max de déplacement")]
    float distanceLimit = 100.0f;

    //Position de départ
    Vector3 startPos = Vector3.zero;
    void Start()
    {
        Init();
    }
    /// <summary>
    /// Lance le movement aléatoire continu du widget
    /// </summary>
    public void StartRandomMove()
    {
        if (IsRunning) return;
        IsRunning = true;
        InvokeRepeating(nameof(RandomMove), frequency, frequency);

    }
    /// <summary>
    /// Arrete le movement aléatoire continu du widget
    /// </summary>
    public void StopRandomMove()
    {
        if (!IsRunning) return;
        IsRunning = false;
        CancelInvoke(nameof(RandomMove));
    }
    /// <summary>
    /// Deplace aléatoirement le widget avec le random radius et le translateType
    /// </summary>
    public void RandomMove()
    {
        if(!widgetTransform)
        {
            StopRandomMove();
            return;
        }
        Vector3 _randomOffset = Vector3.zero;
        do
        {
            _randomOffset = Random.insideUnitCircle * randomMoveRadius;

        } while (Vector3.Distance(widgetTransform.position + _randomOffset, startPos) >= distanceLimit);
        widgetTransform.Translate(_randomOffset, translateType);
    }

    /// <summary>
    /// Initialise le widget transform et lance automatiquement le movement aléatoire si activé
    /// </summary>
    void Init()
    {
        widgetTransform = GetComponent<RectTransform>();
        if(!widgetTransform) widgetTransform = GetComponentInChildren<RectTransform>();
        if (LaunchOnStart) StartRandomMove();
        startPos = widgetTransform.position;
    }
}

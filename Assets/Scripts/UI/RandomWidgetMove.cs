#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteAlways]
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
    //Distance max de déplacement
    [SerializeField, ToggleVisibleAnywhereProperty, Tooltip("Distance max de déplacement")]
    float distanceLimit = 100.0f;
    //Utilisation des debugs
    [SerializeField, Tooltip("Utilisation des debugs")]
    bool useDebug = false;
    //Utilisation des debugs seulement si l'objet est sélectionner
    [SerializeField, HideCondition(nameof(useDebug)), Tooltip("Seulement si l'objet est sélectionner")]
    bool debugOnlyIfSelected = true;
    //Couleur du cercle du rayon
    [SerializeField, HideCondition(nameof(useDebug)), ToggleVisibleAnywhereProperty, Tooltip("Couleur du cercle du rayon")]
    Color radiusColor = Color.red;
    //Couleur du cercle de la positio
    [SerializeField, HideCondition(nameof(useDebug)), ToggleVisibleAnywhereProperty, Tooltip("Couleur du cercle de la position")]
    Color posColor = Color.blue;
    //Rayon du cercle de la position
    [SerializeField, HideCondition(nameof(useDebug)), ToggleVisibleAnywhereProperty, Tooltip("Rayon du cercle de la position")]
    float posRadius = 0.1f;

    //Position de départ
    [SerializeField, VisibleAnywhereProperty, Tooltip("Position de départ")] Vector3 startPos = Vector3.zero;

    private void Awake()
    {
        widgetTransform = GetComponent<RectTransform>();
        if (!widgetTransform) widgetTransform = GetComponentInChildren<RectTransform>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Application.isPlaying) return;
        startPos = widgetTransform ? widgetTransform.position : Vector3.zero;
#endif
    }

    void Start()
    {
        if (!Application.isPlaying) return;
        Init();
    }
    /// <summary>
    /// Lance ou Arrete le movement aléatoire continu du widget selon son etat actuel
    /// </summary>
    public void ToggleRandomMove()
    {
        if (!IsRunning) StartRandomMove();
        else StopRandomMove();

    }
    /// <summary>
    /// Lance ou Arrete le movement aléatoire continu du widget selon son le bool
    /// </summary>
    /// <param name="_status">status final rechercher</param>
    public void SetRandomMove(bool _status)
    {
        if (_status) StartRandomMove();
        else StopRandomMove();

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
        Vector2 _randomUnit = Random.insideUnitCircle * randomMoveRadius;
        Vector3 _randomOffset = widgetTransform.up * _randomUnit.y + _randomUnit.x * widgetTransform.right; 
        Vector3 _targetPos = widgetTransform.position + _randomOffset;
        if (Vector3.Distance(_targetPos, startPos) >= distanceLimit)
        {
            _targetPos = Vector3.MoveTowards(widgetTransform.position, startPos, Random.value * randomMoveRadius);
            return;
        }
        //_randomOffset = Vector3.ProjectOnPlane(_randomOffset, widgetTransform.right);
        //widgetTransform.Translate(_randomOffset);
        widgetTransform.position = _targetPos;
    }

    /// <summary>
    /// Initialise le widget transform et lance automatiquement le movement aléatoire si activé
    /// </summary>
    void Init()
    {
        if (LaunchOnStart) StartRandomMove();
        startPos = widgetTransform ? widgetTransform.position : Vector3.zero;
    }

    /// <summary>
    /// Fonction d'unity qui permet les debugs
    /// </summary>
    private void OnDrawGizmos()
    {
        if(!useDebug || debugOnlyIfSelected) return;
        DebugRadius();
    }
    /// <summary>
    /// Fonction d'unity qui permet les debugs quand selectionner
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if(!useDebug || !debugOnlyIfSelected) return;
        DebugRadius();

    }

    /// <summary>
    /// Fonction pour afficher mes debugs sphere de rayon et position
    /// </summary>
    void DebugRadius()
    {
        if (!widgetTransform) return;
#if UNITY_EDITOR
        Handles.color = radiusColor;
        Handles.DrawWireDisc(startPos, widgetTransform.forward, distanceLimit);
        Handles.color = posColor;
        Handles.DrawSolidDisc(widgetTransform.position, widgetTransform.forward, posRadius);
        Handles.color = Color.white;
#endif
    }
}

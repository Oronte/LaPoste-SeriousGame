using UnityEngine;
using UnityEngine.UI;


public class TB_FeedbackComponent : MonoBehaviour
{
    // AUDIO 
    [SerializeField] AudioClip barreBipSon;
    [SerializeField] AudioClip courtCircuitSon;
    [SerializeField] AudioClip erreurSon;
    [SerializeField, VisibleAnywhereProperty] AudioSource audioSource;

    // VISUAL
    [SerializeField, Tooltip("flash rouge apparaissant en cas d'echec au mini-jeu")] 
    Image redFlashImage;
    [SerializeField, Tooltip("Courbe definissant le changement de l'opacité du flash rouge")]
    AnimationCurve opacityCurve = null;
    [SerializeField, Tooltip("définis la durée du flash rouge")]
    float flashTime = 1.0f;
    float flashTimer = 0f;
    Color colorFlash = Color.red;
    bool showRedFlash = false;



    // AUDIO
    public AudioClip BarreBipSon => barreBipSon;
    public AudioClip CourtCircuitSon => courtCircuitSon;
    public AudioClip ErreurSon => erreurSon;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    void Init()
    {
        audioSource = GetComponent<AudioSource>();
        redFlashImage.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (showRedFlash)
            ShowRedFlash();
    }

    public void PlaySound(AudioClip _clipToPLay)
    {
        if (!audioSource || !_clipToPLay)
        {
            //Debug.Log("AudioSource or AudioClips not set !");
            return;
        }

        audioSource.clip = _clipToPLay;
        audioSource.Play();
    }

    public void SetActiveRedFlash(bool _active)
    {
        showRedFlash = _active;
    }

    public void SetActiveRedFlash()
    {
        showRedFlash = true;
        flashTimer = 0f;

        colorFlash = Color.red;
        colorFlash.a = 0f;

        redFlashImage.gameObject.SetActive(true);
    }

    void ShowRedFlash()
    {
        if (!redFlashImage)
        {
            //Debug.Log("ya pas de red flash image");
            return;
        }

        flashTimer += Time.deltaTime;

        float _t = Mathf.Clamp01(flashTimer / flashTime); // 0 -> 1
        float _alpha = opacityCurve.Evaluate(_t);

        //Debug.Log($"t={_t} alpha={_alpha}");

        _alpha /= 255.0f;
        colorFlash.a = _alpha;
        redFlashImage.color = colorFlash;

        if (flashTimer >= flashTime)
        {
            showRedFlash = false;
            redFlashImage.gameObject.SetActive(false);
            colorFlash.a = 0f;
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifferenceZone : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] bool founded = false;
    [SerializeField] Sprite foundImage = null;
    [SerializeField] Image imageComponent = null;
    [SerializeField] Color foundColor = Color.red;
    [SerializeField] UnityEvent<DifferenceZone> onDiffFounded;

    public bool Founded => founded;

    public UnityEvent<DifferenceZone> OnDiffFounded => onDiffFounded;

    public void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (founded) return;

        founded = true;
        imageComponent.sprite = foundImage;
        imageComponent.color = foundColor;
        onDiffFounded?.Invoke(this);
    }
}

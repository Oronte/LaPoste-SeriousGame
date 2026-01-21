using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TB_RedBar : MonoBehaviour
{

    [SerializeField] protected Image redBarImage;

    public Image RedBarImage => redBarImage;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (!redBarImage) return;

    }

    protected virtual void Init()
    {
        redBarImage = GetComponent<Image>();
        RedBarImage.enabled = true;
        
    }


}

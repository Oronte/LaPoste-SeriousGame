using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TB_RedBar : MonoBehaviour
{

    [SerializeField] protected Image redBarImage;

    public Image RedBarImage => redBarImage;

    void Start()
    {
    }

    void Update()
    {

    }

    public void Init()
    {
        redBarImage = GetComponent<Image>();
        RedBarImage.enabled = true;
        
    }


}

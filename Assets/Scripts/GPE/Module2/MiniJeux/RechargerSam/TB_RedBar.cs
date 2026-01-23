using UnityEngine;
using UnityEngine.UI;

public class TB_RedBar : MonoBehaviour
{

    [SerializeField] Image redBarImage;

    public Image RedBarImage => redBarImage;


    void Start()
    {
        //GameManager.Instance.GetMiniGame<BatterieTimingGame>().RedBar = this
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

using UnityEngine;

public class BlurCameraEffect : MonoBehaviour
{
    public Material effectMat;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(effectMat != null)
        {
            Graphics.Blit(source, destination, effectMat);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}

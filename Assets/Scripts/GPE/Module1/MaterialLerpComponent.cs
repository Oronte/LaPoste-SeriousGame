using System.Collections;
using UnityEngine;

public class MaterialLerpComponent : MonoBehaviour
{
    [SerializeField] Material targetMateral = null;
    [SerializeField] float duration = 2.0f;

    private void Start()
    {
        //MeshRenderer _renderer = GetComponent<MeshRenderer>();
        //StartCoroutine(SmoothTextureChange(_renderer, _renderer.material, targetMateral, duration));
        Invoke("SwapTexture", duration);
    }

    void SwapTexture()
    {
        MeshRenderer _renderer = GetComponent<MeshRenderer>();
        if (!_renderer) return;
        _renderer.material = targetMateral;
    }

    IEnumerator SmoothTextureChange(Renderer _renderer, Material _matA, Material _matB, float _duration)
    {
        float _time = 0f;
        Material _tempMat = new Material(_matA);

        while (_time < 1f)
        {
            _time += Time.deltaTime / _duration;
            _tempMat.Lerp(_matA, _matB, _time);
            _renderer.material = _tempMat;
            yield return null;
        }

        _renderer.material = _matB;
    }
}

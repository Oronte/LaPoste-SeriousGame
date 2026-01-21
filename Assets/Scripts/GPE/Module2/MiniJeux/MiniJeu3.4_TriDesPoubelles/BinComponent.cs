using UnityEngine;
using System.Collections.Generic;

public class BinComponent : MonoBehaviour
{
    List<GarbageComponent> garbages = new List<GarbageComponent>();
    [SerializeField] bool canRecycle = false; 

    void OnTriggerEnter(Collider _other)
    {
        GarbageComponent _garbage = _other.GetComponent<GarbageComponent>();
        if (!_garbage) return;

        garbages.Add(_garbage);
    }

    void OnTriggerExit(Collider _other)
    {
        GarbageComponent _garbage = _other.GetComponent<GarbageComponent>();
        if (!_garbage) return;

        garbages.Remove(_garbage);
    }

    private void Update()
    {
        Debug.Log(ComputePercentage());
    }

    float ComputePercentage()
    {
        int _garbageCount = garbages.Count;
        if (_garbageCount == 0) return 0f;

        int _correctGarbageCount = 0;
        foreach (GarbageComponent _garbage in garbages)
            if (_garbage.IsReciclable == canRecycle) ++_correctGarbageCount;
            
        return _correctGarbageCount / _garbageCount;
    }
}

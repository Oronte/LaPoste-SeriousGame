using UnityEngine;
using System.Collections.Generic;

public class BinComponent : MonoBehaviour
{
    BinMniGame miniGame = null;
    List<GarbageComponent> garbages = new List<GarbageComponent>();
    [SerializeField] bool canRecycle = false; 

    void Start()
    {
        if (!miniGame) return;
        miniGame.bins.Add(this);
    }

    void OnTriggerEnter(Collider _other)
    {
        GarbageComponent _garbage = _other.GetComponent<GarbageComponent>();
        if (!_garbage) return;

        garbages.Add(_garbage);
        Debug.Log(garbages.Count);
    }

    void OnTriggerExit(Collider _other)
    {
        GarbageComponent _garbage = _other.GetComponent<GarbageComponent>();
        if (!_garbage) return;

        garbages.Remove(_garbage);

        Debug.Log(garbages.Count);
    }

    public int GetCorrectCount()
    {
        int _result = 0;
        foreach (GarbageComponent _garbage in garbages)
            if (_garbage.IsReciclable == canRecycle) ++_result;

        return _result;
    }
}

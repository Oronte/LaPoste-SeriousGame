using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BinMniGame : MiniGame
{
    [SerializeField, VisibleAnywhereProperty] public List<BinComponent> bins = new List<BinComponent>();
    List<GarbageComponent> garbages = new List<GarbageComponent>();
    public override bool IsFinished => ComputePercentage() > 0.8f;

    void Start()
    {
        garbages = Resources.FindObjectsOfTypeAll<GarbageComponent>().ToList();
    }

    float ComputePercentage()
    {
        int _correctGarbageCount = 0;
        int _totalGarbageCount = bins.Count;

        foreach (BinComponent _bin in bins)
            _correctGarbageCount += _bin.GetCorrectCount();

        return _correctGarbageCount / _totalGarbageCount;
    }
}

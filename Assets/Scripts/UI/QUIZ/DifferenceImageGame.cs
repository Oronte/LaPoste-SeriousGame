using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifferenceImageGame : MonoBehaviour
{

    [SerializeField] List<DifferenceZone> listOfDifference;
    [SerializeField] UnityEvent onAllDifferenceFound;
    [SerializeField] UnityEvent onDifferenceFound;

    private void Awake()
    {
        listOfDifference = GetComponentsInChildren<DifferenceZone>().ToList();
        //Binding
        foreach (DifferenceZone _differenceZone in listOfDifference)
        {
            _differenceZone.OnDiffFounded.AddListener(OnDifferenceFound);
        }
    }

    public void OnDifferenceFound(DifferenceZone _diff)
    {
        if(!_diff || !listOfDifference.Contains(_diff)) return;
        if (CheckIfAllDiffFound(out int _nbFounded, out int _nbRemaining))
            onAllDifferenceFound?.Invoke();

        onDifferenceFound?.Invoke();
    }

    bool CheckIfAllDiffFound(out int _nbFounded, out int _nbRemaining)
    {
        bool _result = true;
        _nbFounded = 0;
        _nbRemaining = 0;

        foreach (DifferenceZone _diff in listOfDifference)
        {
            if (_diff == null)
                continue;

            if (!_diff.Founded)
            {
                _nbRemaining++;
                _result = false;
            }
            else
            {
                _nbFounded++;
            }
        }

        return _result;
    }
}

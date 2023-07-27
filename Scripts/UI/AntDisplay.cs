using TMPro;
using UnityEngine;

public class AntDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI antCounter;

    private int _currentAntAmount;
    private int _maxCapacity;

    private void Awake()
    {
        _currentAntAmount = 0;
    }

    /// Called from the Editor
    public void OnMaxCapacitySet(int maxCapacity)
    {
        _maxCapacity = maxCapacity;
        antCounter.text = $"Ants: {_currentAntAmount}/{_maxCapacity}";
    }

    /// Called from the Editor
    public void OnAntCollected(int amount)
    {
        _currentAntAmount += amount;
        antCounter.text = $"Ants: {_currentAntAmount}/{_maxCapacity}";
    }

    /// Called from the Editor
    public void OnAntReset()
    {
        _currentAntAmount = 0;
        antCounter.text = $"Ants: {_currentAntAmount}";
    }
}

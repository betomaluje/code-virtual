using UnityEngine;
using UnityEngine.Events;

public class AntCollector : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private Transform originPoint;
    [SerializeField] private float radius = 1f;
    [SerializeField] private int maxPouchCapacity = 5;

    public UnityEvent<int> OnAntCollected;
    public UnityEvent OnAntsReset;
    public UnityEvent<int> OnMaxCapacitySet;

    private int _currentAntAmount = 0;

    private void Awake()
    {
        OnMaxCapacitySet?.Invoke(maxPouchCapacity);
    }

    private void Update()
    {
        if (_currentAntAmount < maxPouchCapacity)
        {
            SearchForAnts();
        }

        TryFeedAnteater();
    }

    private void TryFeedAnteater()
    {
        if (Physics.SphereCast(originPoint.position, radius, Vector3.forward, out RaycastHit hit, radius, targetMask))
        {
            if (hit.transform.TryGetComponent(out AnteaterHealth anteaterHealth))
            {
                anteaterHealth.Feed(_currentAntAmount);
                _currentAntAmount = 0;
                OnAntsReset?.Invoke();
            }
        }
    }

    private void SearchForAnts()
    {
        RaycastHit[] hits = Physics.SphereCastAll(originPoint.position, radius, Vector3.forward, radius, targetMask);
        if (hits != null && hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent(out Ant ant))
                {
                    _currentAntAmount++;
                    OnAntCollected?.Invoke(1);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (originPoint == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(originPoint.position, radius);
    }
}

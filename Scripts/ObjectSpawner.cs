using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] objectPrefabs;
    [SerializeField] private Transform originPoint;
    [SerializeField] private float spawnRadius;
    [SerializeField] private int amount;
    [SerializeField] private bool createAsChild = true;
    [Space]
    [Header("Layer Mask")]
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            var position = GetRandomSpawnPoint();
            SpawnAntHive(position);
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        var terrainPoint = originPoint.position.GetRandomPointFromTilemap(spawnRadius);

        Ray ray = new Ray(terrainPoint, Vector3.up * spawnRadius);
        Debug.DrawRay(terrainPoint, Vector3.up * spawnRadius, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayer))
        {
            return hit.point;
        }
        else
        {
            return terrainPoint;
        }
    }

    private void SpawnAntHive(Vector3 position)
    {
        int randomIndex = Random.Range(0, objectPrefabs.Length);
        var hive = Instantiate(objectPrefabs[randomIndex], position, Quaternion.identity);

        if (createAsChild)
        {
            hive.SetParent(originPoint);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (originPoint == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(originPoint.position, spawnRadius);
    }
}

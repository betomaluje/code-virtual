using UnityEngine;

public static class Vector3Ext
{
    public static Vector3 GetRandomPointFromTerrain(this Vector3 originPoint, float radius)
    {
        Vector2 randomPoint = (Random.insideUnitCircle * radius);
        Vector3 pointOn3D = new Vector3(randomPoint.x, 0, randomPoint.y);
        pointOn3D.y = Terrain.activeTerrain.SampleHeight(randomPoint);
        return originPoint + pointOn3D;
    }

    public static Vector3 GetRandomPointFromTilemap(this Vector3 originPoint, float radius)
    {
        Vector2 randomPoint = (Random.insideUnitCircle * radius);
        Vector3 pointOn3D = new Vector3(randomPoint.x, 0, randomPoint.y);
        return originPoint + pointOn3D;
    }
}

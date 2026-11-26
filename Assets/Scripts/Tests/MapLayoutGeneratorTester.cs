using UnityEngine;

public class MapLayoutGeneratorTester : MonoBehaviour
{
    public GameObject square;
    public int count;
    public int min;
    public int max;

    [ContextMenu("Generate")]
    public void Generate()
    {
        SpawnLayout(count);
    }

    [ContextMenu("Random Generate")]
    public void RandomGenerate()
    {
        SpawnLayout(Random.Range(min, max + 1));
    }

    private void SpawnLayout(int roomCount)
    {
        var layout = MapLayoutGenerator.GenerateLayout(roomCount);
        foreach (var room in layout)
            Instantiate(square, new Vector3(room.x, room.y), Quaternion.identity);
    }
}
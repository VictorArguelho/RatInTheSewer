using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MapLayoutGenerator
{
    private static readonly Vector2Int[] DIRECTIONS = new Vector2Int[4]
    {
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.up,
        Vector2Int.right
    };

    private static Vector2Int[] _rooms;
    private static int _addedRooms;

    public static Vector2Int[] GenerateLayout(int roomCount)
    {
        _rooms = new Vector2Int[roomCount];
        _rooms[0] = Vector2Int.zero;
        _addedRooms = 1;

        for (int i = 1; i < roomCount; i++)
        {
            if (TryGetRandomAdjacent(GetRandomRoom(), out var room))
            {
                _rooms[i] = room;
                _addedRooms++;
            }
            else
                i--;
        }

        return _rooms;
    }

    private static Vector2Int GetRandomRoom() =>
        _rooms[Random.Range(0, _addedRooms)];

    private static bool TryGetRandomAdjacent(Vector2Int room, out Vector2Int adjacentRoom)
    {
        var triedDirections = new Dictionary<Vector2Int, bool>()
        {
            { Vector2Int.down, false},
            { Vector2Int.up, false},
            { Vector2Int.left, false},
            { Vector2Int.right, false}
        };

        do
        {
            var direction = GetRandomDirection();
            triedDirections[direction] = true;
            adjacentRoom = room + direction;

            if (!_rooms.Contains(adjacentRoom))
                return true;
        } while ((!AllDirectionsTried(triedDirections)));

        return false;
    }

    private static bool AllDirectionsTried(Dictionary<Vector2Int, bool> triedDirections)
    {
        foreach (var direction in triedDirections)
        {
            if (!direction.Value)
                return false;
        }
        return true;
    }

    private static Vector2Int GetRandomDirection() =>
        DIRECTIONS[Random.Range(0, DIRECTIONS.Length)];
}
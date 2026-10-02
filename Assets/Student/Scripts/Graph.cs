using UnityEngine;
using System.Collections.Generic;

public class Graph
{
    int vertices;
    int edges;
    List<int>[] adjacencyList;

    public int Vertices => vertices;

    public Graph(IMapData mapdata)
    {
        vertices = mapdata.Width * mapdata.Height;
        edges = 0;
        adjacencyList = new List<int>[vertices];

        for (int i = 0; i < vertices; i++)
        {
            adjacencyList[i] = new List<int>();
        }

        Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };

        for (int x = 0; x < mapdata.Width; x++)
        {
            for (int y = 0; y < mapdata.Height; y++)
            {
                int current = y * mapdata.Width + x;
                Vector2Int currentPos = new Vector2Int(x, y);

                foreach (Vector2Int direction in directions)
                {
                    Vector2Int neighborPos = currentPos + direction;
                    //  NEIGHBOR OUT OF INDEX CHECKS
                    if (neighborPos.x >= 0 && neighborPos.x < mapdata.Width && neighborPos.y >= 0 && neighborPos.y < mapdata.Height)
                    {
                        if (!PathfindingAlgorithm.IsMovementBlocked(currentPos, neighborPos, mapdata))
                        {
                            int neighbor = neighborPos.y * mapdata.Width + neighborPos.x;
                            AddEdge(current, neighbor);
                        }
                    }
                }
            }
        }
    }
    public int GetVertices()
    {
        return vertices;
    }
    public int GetEdges()
    {
        return edges;
    }

    public void AddEdge(int v, int w)
    {
        adjacencyList[v].Add(w);
        adjacencyList[w].Add(v);
        edges++;
    }

    public IEnumerable<int> GetAdjacent(int v)
    {
        return adjacencyList[v];
    }


}

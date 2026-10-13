using System.Collections.Generic;
using System.Transactions;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EdgeWeightedDigraph
{
    int vertices;
    int edges;
    List<DirectedEdge>[] adjacencyList;

    public EdgeWeightedDigraph(IMapData mapdata)
    {
        vertices = mapdata.Width * mapdata.Height;
        edges = 0;

        adjacencyList = new List<DirectedEdge>[vertices];

        for (int v = 0; v < vertices; v++)
        {
            adjacencyList[v] = new List<DirectedEdge>();
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

                            if (currentPos + directions[0] == neighborPos) // UP
                            {
                                double weight = mapdata.GetHorizontalWallCost(neighborPos.x, neighborPos.y);
                                AddEdge(new DirectedEdge(current, neighbor, weight));
                            }
                            else if (currentPos + directions[1] == neighborPos) // DOWN
                            {
                                double weight = mapdata.GetHorizontalWallCost(currentPos.x, currentPos.y);
                                AddEdge(new DirectedEdge(current, neighbor, weight));
                            }
                            else if (currentPos + directions[2] == neighborPos) // RIGHT
                            {
                                double weight = mapdata.GetVerticalWallCost(neighborPos.x, neighborPos.y);
                                AddEdge(new DirectedEdge(current, neighbor, weight));
                            }
                            else if (currentPos + directions[3] == neighborPos) // LEFT
                            {
                                double weight = mapdata.GetVerticalWallCost(currentPos.x, currentPos.y);
                                AddEdge(new DirectedEdge(current, neighbor, weight));
                            }
                        }
                    }
                }

                if(mapdata.HasJumpFrom(x, y))
                {
                    foreach(JumpConnection jump in mapdata.GetJumpsFrom(currentPos))
                    {
                        int jumpToVertex = jump.To.y * mapdata.Width + jump.To.x;
                        AddEdge(new DirectedEdge(current, jumpToVertex, jump.Cost));
                    }
                }
            }
        }
    }
    public int Vertices => vertices;

    public int GetVertices()
    {
        return vertices;
    }
    public int GetEdges()
    {
        return edges;
    }

    public void AddEdge(DirectedEdge e)
    {
        adjacencyList[e.From()].Add(e);
        edges++;
    }

    public IEnumerable<DirectedEdge> GetAdjacent(int v)
    {
        return adjacencyList[v];
    }

    public IEnumerable<DirectedEdge> Edges()
    {
        List<DirectedEdge> directedEdges = new List<DirectedEdge>();

        for (int v = 0; v < vertices; v++)
        {
            foreach (DirectedEdge e in adjacencyList[v])
            {
                directedEdges.Add(e);
            }
        }

        return directedEdges;
    }
}

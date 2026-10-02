using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Experimental.AI;

public class BreadthFirstSearch
{
    private bool[] marked;
    private int[] edgeTo;
    private int s;

    public int S => s;

    public BreadthFirstSearch(Graph G, int s)
    {
        marked = new bool[G.GetVertices()];
        edgeTo = new int[G.GetVertices()];
        this.s = s;
        bfs(G, s);
    }

    private void bfs(Graph G, int s)
    {
        Queue<int> queue = new Queue<int>();
        marked[s] = true;
        queue.Enqueue(s);
        while (queue.Count > 0)
        {
            int v = queue.Dequeue();
            foreach (int w in G.GetAdjacent(v))
            {
                if (!marked[w])
                {
                    edgeTo[w] = v;
                    marked[w] = true;
                    queue.Enqueue(w);
                }
            }
        }
    }

    public bool hasPathTo(int v)
    {
        return marked[v];
    }

    public IEnumerable<int> PathTo(int v)
    {
        if (!hasPathTo(v))
            return null;

        Stack<int> path = new Stack<int>();
        for (int x = v; x != s; x = edgeTo[x])
        {
            path.Push(x);
        }       

        path.Pop();
        return path;
    }
}

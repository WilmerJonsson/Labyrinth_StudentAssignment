using UnityEngine;

public class BreadthFirstSearch : MonoBehaviour
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
}

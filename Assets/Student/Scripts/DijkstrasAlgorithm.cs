using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DijkstrasAlgorithm
{
    private DirectedEdge[] edgeTo;
    private double[] distTo;
    private IndexMinPQ<double> pq;

    public DijkstrasAlgorithm(EdgeWeightedDigraph G, int s)
    {
        edgeTo = new DirectedEdge[G.GetVertices()];
        distTo = new double[G.GetVertices()];

        pq = new IndexMinPQ<double>(G.GetVertices());

        for(int v = 0; v < G.GetVertices(); v++)
        {
            distTo[v] = double.PositiveInfinity;
        }

        distTo[s] = 0.0;

        pq.Insert(s, 0.0);
        
        while(!pq.IsEmpty())
        {
            Relax(G, pq.DelMin());
        }
    }

    private void Relax(EdgeWeightedDigraph G, int v)
    {
        foreach( DirectedEdge e in G.GetAdjacent(v))
        {
            int w = e.To();

            if (distTo[w] > distTo[v] + e.GetWeight())
            {
                distTo[w] = distTo[v] + e.GetWeight();
                edgeTo[w] = e;

                if(pq.Contains(w))
                {
                    pq.ChangeKey(w, distTo[w]);
                }
                else
                {
                    pq.Insert(w, distTo[w]);
                }
            }
        }
    }

    public double DistTo(int v)
    {
        return distTo[v];
    }

    public bool hasPathTo(int v)
    {
        return !(distTo[v] == double.PositiveInfinity);
    }

    public IEnumerable<DirectedEdge> PathTo(int v)
    {
        if (!hasPathTo(v))
            return null;

        Stack<DirectedEdge> path = new Stack<DirectedEdge>();

        for(DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.From()])
        {
            path.Push(e);
        }

        return path;
    }
}

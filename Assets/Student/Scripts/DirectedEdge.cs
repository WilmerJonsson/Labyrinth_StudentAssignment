using System.Collections.Generic;
using UnityEngine;

public class DirectedEdge
{
    int V;
    int W;
    double Weight;

    public int v => V;
    public int w => W;
    public double weight => Weight;

    public DirectedEdge(int v, int w, double weight)
    {
        V = v;
        W = w;
        Weight = weight;
    }

    public double GetWeight()
    {
        return Weight;
    }

    public int From()
    {
        return V;
    }

    public int To()
    {
        return W;
    }

    public string toString()
    {
        return string.Format("%d->%d % .2f", v, w, weight);
    }


}

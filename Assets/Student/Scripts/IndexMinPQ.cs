using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class IndexMinPQ<T>
{
    private int maxN;
    private int n;
    private int[] pq;
    private int[] qp;
    private T[] keys;


    public IndexMinPQ(int maxN)
    {
        if (maxN < 0)
            throw new ArgumentException("MAX N < 0");

        this.maxN = maxN;
        n = 0;
        keys = new T[maxN + 1];
        pq = new int[maxN + 1];
        qp = new int[maxN + 1];

        for (int i = 0; i <= maxN; i++)
        {
            qp[i] = -1;
        }
    }

    public void Insert(int i, T key)
    {
        validateIndex(i);
        if (Contains(i)) throw new ArgumentException("index is already in the priority queue");
        n++;
        qp[i] = n;
        pq[n] = i;
        keys[i] = key;
        Swim(n);
    }

    public void ChangeKey(int i, T key)
    {
        validateIndex(i);
        if (!Contains(i)) throw new ArgumentException("index is not in the priority queue");
        keys[i] = key;
        Swim(qp[i]);
        Sink(qp[i]);
    }

    public bool Contains(int i)
    {
        validateIndex(i);
        return qp[i] != -1;
    }
    public int DelMin()
    {
        if (n == 0) throw new ArgumentException("Priority queue underflow");
        int min = pq[1];
        Exch(1, n--);
        Sink(1);
        qp[min] = -1;        // delete
        keys[min] = default(T);    // to help with garbage collection
        pq[n + 1] = -1;        // not needed
        return min;
    }

    public bool IsEmpty()
    {
        return n == 0;
    }

    private void Swim(int k)
    {
        while (k > 1 && Less(k / 2, k))
        {
            Exch(k /2, k);
            k = k / 2;
        }
    }

    private void Sink(int k)
    {
        while (2 * k <= n)
        {
            int j = 2 * k;
            if (j < n && Greater(j, j + 1)) j++;
            if (!Greater(k, j)) break;
            Exch(k, j);
            k = j;
        }
    }
    private bool Greater(int i, int j)
    {

        return Comparer.Default.Compare(keys[pq[i]], keys[pq[j]]) > 0;
    }
    private bool Less(int i, int j)
    {
        return Comparer.Default.Compare(keys[pq[i]], keys[pq[j]]) < 0;
    }

    private void Exch(int i, int j)
    {
        int swap = pq[i];
        pq[i] = pq[j];
        pq[j] = swap;
        qp[pq[i]] = i;
        qp[pq[j]] = j;
    }
    private bool validateIndex(int i)
    {
        if (i >= 0 && i < maxN)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

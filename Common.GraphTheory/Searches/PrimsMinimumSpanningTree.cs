﻿using System;
using System.Collections.Generic;

using Common.Core.LinearAlgebra;
using Common.Collections.Queues;
using Common.GraphTheory.Adjacency;
using Common.GraphTheory.Grids;

namespace Common.GraphTheory.Searches
{
    internal static class PrimsMinimumSpanningTree
    {

        internal static void Search<VERTEX, EDGE>(AdjacencyGraph<VERTEX, EDGE> graph, AdjacencySearch search, int root, IComparer<EDGE> comparer)
            where EDGE : class, IAdjacencyEdge, new()
        {

            int count = graph.VertexCount;

            bool[] isVisited = new bool[count];
            isVisited[root] = true;

            search.Parent[root] = root;
            search.Order.Add(root);

            PriorityQueue<EDGE> queue = new PriorityQueue<EDGE>(comparer);

            if(graph.Edges[root] != null)
            {
                foreach (EDGE edge in graph.Edges[root])
                    queue.Push(edge);
            }

            while (queue.Count != 0)
            {
                EDGE edge = queue.Pop();

                int v = edge.To;
                if (isVisited[v]) continue;

                search.Order.Add(v);
                isVisited[v] = true;
                search.Parent[v] = edge.From;

                if (graph.Edges[v] != null)
                {
                    foreach (EDGE e in graph.Edges[v])
                    {
                        if (isVisited[e.To]) continue;
                        queue.Push(e);
                    }
                }

            }

        }

        internal static void Search(GridGraph graph, GridSearch search, int x, int y, float[,] weights, IComparer<GridEdge> comparer)
        {
            int width = graph.Width;
            int height = graph.Height;
            
            bool[,] isVisited = new bool[width, height];
            isVisited[x, y] = true;

            search.Order.Add(new Vector2i(x, y));

            if(search.Parent != null)
                search.Parent[x, y] = new Vector2i(x, y);

            PriorityQueue<GridEdge> queue = new PriorityQueue<GridEdge>(comparer);

            List<GridEdge> edges = new List<GridEdge>(8);
            graph.GetEdges(x, y, edges, weights);

            if (edges.Count != 0)
            {
                foreach (GridEdge edge in edges)
                    queue.Push(edge);

                edges.Clear();
            }

            while (queue.Count != 0)
            {
                GridEdge edge = queue.Pop();

                Vector2i v = edge.To;
                if (isVisited[v.x, v.y]) continue;

                search.Order.Add(v);
                isVisited[v.x, v.y] = true;

                if (search.Parent != null)
                    search.Parent[v.x, v.y] = edge.From;

                if (graph.Edges[v.x, v.y] == 0) continue;

                graph.GetEdges(v.x, v.y, edges, weights);

                foreach (GridEdge e in edges)
                {
                    if (isVisited[e.To.x, e.To.y]) continue;
                    queue.Push(e);
                }

                edges.Clear();

            }

        }
    
    }
}

﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Common.Core.Directions;
using Common.GraphTheory.Grids;

namespace Common.GraphTheory.Test.Grids
{
    [TestClass]
    public class GraphTheory_Grids_GridGraphTest
    {
        [TestMethod]
        public void FoldFulkersonMaxFlow()
        {
            int width = 4;
            int height = 2;
            GridFlowGraph graph = new GridFlowGraph(width, height);

            graph.Label[0, 0] = GridFlowGraph.SOURCE;
            graph.Label[3, 0] = GridFlowGraph.SINK;

            graph.Capacity[0, 0, D8.RIGHT] = 16;
            graph.Capacity[0, 0, D8.RIGHT_TOP] = 13;
            graph.Capacity[1, 0, D8.TOP] = 10;
            graph.Capacity[1, 0, D8.RIGHT] = 12;
            graph.Capacity[1, 1, D8.BOTTOM] = 4;
            graph.Capacity[1, 1, D8.RIGHT] = 14;
            graph.Capacity[2, 0, D8.RIGHT] = 20;
            graph.Capacity[2, 0, D8.LEFT_TOP] = 9;
            graph.Capacity[2, 1, D8.BOTTOM] = 7;
            graph.Capacity[2, 1, D8.RIGHT_BOTTOM] = 4;

            graph.FordFulkersonMaxFlow();

            Assert.AreEqual(23, graph.MaxFlow);

            Assert.AreEqual(GridFlowGraph.SOURCE, graph.Label[0, 0]);
            Assert.AreEqual(GridFlowGraph.SOURCE, graph.Label[1, 0]);
            Assert.AreEqual(GridFlowGraph.SOURCE, graph.Label[1, 1]);
            Assert.AreEqual(GridFlowGraph.SOURCE, graph.Label[2, 1]);

            Assert.AreEqual(GridFlowGraph.SINK, graph.Label[2, 0]);
            Assert.AreEqual(GridFlowGraph.SINK, graph.Label[3, 0]);

            float minCut = 0;

            for(int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (graph.Label[x, y] != GridFlowGraph.SOURCE) continue;

                    for (int i = 0; i < 8; i++)
                    {
                        int xi = x + D8.OFFSETS[i, 0];
                        int yi = y + D8.OFFSETS[i, 1];

                        if (xi < 0 || xi > width - 1) continue;
                        if (yi < 0 || yi > height - 1) continue;

                        if (graph.Label[xi, yi] == GridFlowGraph.SINK)
                            minCut += graph.Flow[x, y, i];
                    }
                }
            }

            Assert.AreEqual(23, minCut);
        }

    }
}

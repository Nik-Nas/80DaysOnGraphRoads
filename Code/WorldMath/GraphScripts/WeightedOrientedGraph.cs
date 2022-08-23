using System;
using System.Collections.Generic;
using System.Linq;

namespace ITCampFinalProject.Code.WorldMath.GraphScripts
{
    public class WeightedOrientedGraph
    { 
        public int NodesCount { get; private set; }
        public int EdgesCount { get; private set; }
        public readonly int[] neighboursCount;
        private Dictionary<int, HashSet<int>> nodes;
        public IReadOnlyList<Vector2> nodesInWorld;


        public WeightedOrientedGraph(int nodesCount, List<Vector2> nodesPositions, params KeyValuePair<int, int>[] nodesConnection)
        {
            if (nodesCount != nodesPositions.Count) throw new ArgumentException("nodes count is not equal to amount of positions");
                NodesCount = nodesCount;
            nodes = new Dictionary<int, HashSet<int>>();
            nodesInWorld = new List<Vector2>(nodesPositions);
            foreach (KeyValuePair<int, int> connection in nodesConnection)
            {
                nodes.Add(connection.Key, new HashSet<int>());
                EdgesCount++;
                nodes[connection.Key].Add(connection.Value);
            }
        }
        
        public WeightedOrientedGraph(int nodesCount, params WeightedEdge[] nodesConnection)
        {
            NodesCount = nodesCount;
            nodes = new Dictionary<int, HashSet<int>>();

            foreach (WeightedEdge connection in nodesConnection)
            {
                nodes.Add(connection.connectedNodes.Key, new HashSet<int>());
                EdgesCount++;
                nodes[connection.connectedNodes.Key].Add(connection.connectedNodes.Value);
            }
        }

        public HashSet<int> GetNeighbours(int nodeIndex)
        {
            return nodes[nodeIndex];
        }

        public class WeightedEdge
        {
            public readonly float cost;
            public KeyValuePair<int, int> connectedNodes;

            public WeightedEdge(float cost, KeyValuePair<int, int> connectedNodes)
            {
                this.cost = cost;
                this.connectedNodes = connectedNodes;
            }
        }
    }
}
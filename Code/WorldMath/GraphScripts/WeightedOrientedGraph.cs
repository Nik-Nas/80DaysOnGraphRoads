using System;
using System.Collections.Generic;
using System.Linq;
using ITCampFinalProject.Code.Utils;

namespace ITCampFinalProject.Code.WorldMath.GraphScripts
{
    public class WeightedOrientedGraph
    { 
        public int NodesCount { get; private set; }
        public int EdgesCount { get; private set; }
        public IReadOnlyList<int> ShortestPath { get; private set; } = new List<int>();
        public int ShortestPathLength { get; private set; }
        private Dictionary<int, HashSet<KeyValuePair<int, int>>> _nodes;

        public WeightedOrientedGraph(int nodesCount, params Triplet<int, int, int>[] nodesConnection)
        {
            NodesCount = nodesCount;
            _nodes = new Dictionary<int, HashSet<KeyValuePair<int, int>>>();
            foreach (Triplet<int, int, int> connection in nodesConnection)
            {
                if (!_nodes.ContainsKey(connection.Key))
                    _nodes.Add(connection.Key, new HashSet<KeyValuePair<int, int>>());
                if (!_nodes.ContainsKey(connection.Value))
                    _nodes.Add(connection.Value, new HashSet<KeyValuePair<int, int>>());
                EdgesCount++;
                _nodes[connection.Key].Add(new KeyValuePair<int, int>(connection.Value, connection.Argument));
                _nodes[connection.Value].Add(new KeyValuePair<int, int>(connection.Key, connection.Argument));
            }
        }
        
        public HashSet<KeyValuePair<int, int>> GetNeighbours(int nodeIndex)
        {
            return _nodes[nodeIndex];
        }

        public bool AreNodesAdjacent(int node1Index, int node2Index)
        {
            return _nodes[node1Index].Count(pair => pair.Key == node2Index) > 0;
        }

        public void WideBillySearch(Action nodeProcessing)
        {
            Queue<int> openSet = new Queue<int>();
            int[] closedSet = new int[_nodes.Count];
            openSet.Enqueue(_nodes.Keys.First());
            while (openSet.Count > 0)
            {
                int x = openSet.Dequeue();
                if (closedSet[x] == 1) continue;
                
                closedSet[x] = 1;
                Console.WriteLine($@"visited node {x + 1}");
                foreach (KeyValuePair<int, int> neighbour in GetNeighbours(x)) openSet.Enqueue(neighbour.Key);
            }
        }

        //private bool[] used;

        /*public void DeepFistingSearch(int index)
        {
            used = new bool[NodesCount];
            DFS(index);
        }

        private void DFS(int index)
        {
            used[index] = true;
            Console.WriteLine("node " + (index + 1) + " visited");
            foreach (int neighbour in nodes[index])
            {
                if (!used[neighbour])
                {
                    DFS(neighbour);
                }
            }

        }*/


        public KeyValuePair<int, List<int>> GetShortestPath(WeightedOrientedGraph graph, int startIndex, int endIndex)
        {
            //creating queue of nodes to use and array of used nodes to execute traversing the graph in width
            Queue<int> openSet = new Queue<int>();
            int[] closedSet = new int[_nodes.Count];
            //placing start index into queue
            openSet.Enqueue(startIndex);
            //creating array to count path lengths through given nodes
            int[] pathLengths = new int[graph.NodesCount];
            for (int i = 0; i < pathLengths.Length; i++)
            {
                //filling array with 99999 value to provide true min path searching
                pathLengths[i] = 99999;
            }

            //assigning element in array to 0 to mark it as entry point
            pathLengths[startIndex] = 0;
            //searching while there are unvisited nodes
            while (openSet.Count > 0)
            {
                //receiving first in queue node index
                int currentNode = openSet.Dequeue();
                //if it was visited continue
                if (closedSet[currentNode] == 1) continue;
                //marking this node as visited
                closedSet[currentNode] = 1;
                //doing some optional actions with node (in current case printing number of visited node)
                Console.WriteLine(@"visited node " + (currentNode + 1));
                //adding to queue all neighbours of chosen node
                foreach (KeyValuePair<int, int> neighbour in graph.GetNeighbours(currentNode))
                {
                    //if number in pathLengths[x] is less than 
                    pathLengths[neighbour.Key] = Math.Min(
                        pathLengths[currentNode] + neighbour.Value,
                        pathLengths[neighbour.Key]);
                    if (!openSet.Contains(neighbour.Key)) openSet.Enqueue(neighbour.Key);
                }
            }

            KeyValuePair<int, List<int>> path = GetPathFromList(graph, pathLengths, startIndex, endIndex);
            ShortestPath = path.Value.AsReadOnly();
            ShortestPathLength = path.Key;
            return path;
        }

        public KeyValuePair<int, List<int>> GetPathFromList(WeightedOrientedGraph graph, int[] pathLengths, int startIndex,
            int endIndex)
        {
            List<int> result = new List<int>();
            int lengthFromEnd = pathLengths[endIndex];
            int currentNodeIndex = endIndex;
            result.Add(endIndex);
            bool pathExists = false;
            while (currentNodeIndex != startIndex)
            {
                foreach (KeyValuePair<int, int> neighbour in 
                         graph.GetNeighbours(currentNodeIndex).Where(neighbour => 
                                 lengthFromEnd - neighbour.Value == pathLengths[neighbour.Key]))
                {
                    pathExists = true;
                    result.Add(neighbour.Key);
                    lengthFromEnd -= neighbour.Value;
                    currentNodeIndex = neighbour.Key;
                    break;
                }

                if (!pathExists)
                {
                    Console.Error.WriteLine("Current path does not exists");
                }
            }

            result.Reverse();
            return new KeyValuePair<int, List<int>>(pathLengths[endIndex], result);
        }

        /*private class PathNode
        {
            public readonly int IndexInGraph;
            public readonly int PathLengthFromStart;
            public readonly PathNode CameFrom;

            public PathNode(int pathLengthFromStart, PathNode cameFrom, int indexInGraph)
            {
                PathLengthFromStart = pathLengthFromStart;
                CameFrom = cameFrom;
                IndexInGraph = indexInGraph;
            }
        }*/
    }
}
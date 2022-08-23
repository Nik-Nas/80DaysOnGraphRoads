using System;
using System.Collections.Generic;
using System.Linq;

namespace ITCampFinalProject.Code.WorldMath.GraphScripts
{
    public static class ShortestPathFinder
    {
        /*public static HashSet<int> GetShortestWayInWeightedAndOrientedGraph(WeightedAndOrientedGraph graph,
            int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex > graph.NodesCount)
                throw new ArgumentException("wrong startIndex or endIndex");
            if (startIndex == endIndex) return new HashSet<int>();
            int pathLength = 0;
            List<int> nodesToCheck = new List<int>();
            nodesToCheck.Add(startIndex);
            List<int> checkedNodes = new List<int>();
            return new HashSet<int>();
        }*/
        
        public static List<Vector2> FindPath(WeightedOrientedGraph graph, Vector2 start, Vector2 goal)
        {
            // Шаг 1.
            HashSet<PathNode> closedSet = new HashSet<PathNode>();
            HashSet<PathNode> openSet = new HashSet<PathNode>();
            // Шаг 2.
            PathNode startNode = new PathNode
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = Vector2.Distance(start, goal),
                IndexInGraph = ;
            };
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                // Шаг 3.
                var currentNode = openSet.OrderBy(node => 
                    node.EstimateFullPathLength).First();
                // Шаг 4.
                if (currentNode.Position == goal)
                    return GetPathForNode(currentNode);
                // Шаг 5.
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                // Шаг 6.
                foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
                {
                    // Шаг 7.
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                        node.Position == neighbourNode.Position);
                    // Шаг 8.
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                    if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        // Шаг 9.
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            // Шаг 10.
            return null;
        }
        
        private static List<PathNode> GetNeighbours(WeightedOrientedGraph graph, PathNode pathNode, Vector2 goal)
        {
            List<PathNode> result = new List<PathNode>();
            HashSet<int> neighbours = graph.GetNeighbours(pathNode.IndexInGraph);
            for (int i = 1; i < neighbours.Count; i++)
            {
                
                var neighbourNode = new PathNode()
                {
                    Position = graph.nodesInWorld[i - 1],
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart + GetDistanceBetweenNeighbours(graph.nodesInWorld[point], ),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                };
                result.Add(neighbourNode);
            }
            return result;
        }
        
        private static List<Vector2> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Vector2>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }

        private static float GetDistanceBetweenNeighbours(Vector2 current, Vector2 other)
        {
            return Vector2.Distance(current, other);
        }
        
        
        public class PathNode
        {
            public int IndexInGraph { get; set; }
            // Координаты точки на карте.
            public Vector2 Position { get; set; }
            // Длина пути от старта (G).
            public float PathLengthFromStart { get; set; }
            // Точка, из которой пришли в эту точку.
            public PathNode CameFrom { get; set; }
            // Примерное расстояние до цели (H).
            public float HeuristicEstimatePathLength { get; set; }
            // Ожидаемое полное расстояние до цели (F).
            public float EstimateFullPathLength => PathLengthFromStart + HeuristicEstimatePathLength;
        }
    }
}
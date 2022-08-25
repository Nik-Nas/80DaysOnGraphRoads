using System;
using System.Collections.Generic;
using static ITCampFinalProject.Code.WorldMath.AdvancedMath;
using System.Linq;
using ITCampFinalProject.Code.Utils;
using System.Drawing;

namespace ITCampFinalProject.Code.WorldMath.GraphScripts
{
    //ReSharper disable InconsistentNaming
    public class GraphGenerator
    {
        public static int MIN_DistanceBetweenNodes
        {
            get => _minDistBetweenNodes;
            set => _minDistBetweenNodes = value > 0 ? value : 10;
        }

        public static int MAX_DistanceBetweenNodes
        {
            get => _maxDistBetweenNodes;
            set => _maxDistBetweenNodes = value <= 1000000 ? value : 1000000;
        }

        private static int _minDistBetweenNodes = 10;
        private static int _maxDistBetweenNodes = 1000;
        private static Random _random;

        /*public static /*List<Vector2>List<int> GenerateRandomGraph(int nodesCount, Size screenSize)
        {
            if (nodesCount <= 0) return new List<int>();
            Console.WriteLine(MAX_DistanceBetweenNodes);
            _random = new Random(DateTime.Now.Millisecond);
            List<int> result = new List<int>();
            float distanceToClosetPoint;
            for (int i = 1; i < nodesCount; i++)
            {
                while (true)
                {
                    distanceToClosetPoint = float.MaxValue;

                    //generating a random vector
                    Vector2 tryVec = new Vector2(_random.Next(30, 30 + screenSize.Width / (nodesCount - i)),
                        _random.Next(30, 30 + screenSize.Height / (nodesCount - i)));

                    /*distanceToClosetPoint = result.Select(generatedPoint => 
                        Vector2.Distance(tryVec, generatedPoint)).Prepend(distanceToClosetPoint).Min();
                    foreach (Vector2 vector in result)
                    {
                        float distanceBetweenNodes = Vector2.Distance(vector, tryVec);
                        if (distanceBetweenNodes < distanceToClosetPoint) distanceToClosetPoint = distanceBetweenNodes;
                    }

                    if (distanceToClosetPoint < MIN_DistanceBetweenNodes ||
                        distanceToClosetPoint > MAX_DistanceBetweenNodes) continue;
                    result.Add(tryVec);
                    break;
                }
            }
            return result;
        }*/

        public static Triplet<int, int, int>[] ConvertPointsListToRandomGraph(List<Vector2> points)
        {
            int pointsCount = points.Count;
            _random = new Random(DateTime.Now.Millisecond);
            int amountOfConnection = _random.Next(pointsCount, pointsCount * 2);
            int maxConnectionToOneNode = _random.Next(pointsCount % 2 + 1, 5);
            int nodeConnectFrom;
            int nodeConnectTo;
            Dictionary<int, int> connections =
                new Dictionary<int, int>();
            List<Triplet<int, int, int>> result = new List<Triplet<int, int, int>>();
            for (int i = 0; i < amountOfConnection; i++)
            {
                while (true)
                {
                    nodeConnectFrom = _random.Next(pointsCount);
                    nodeConnectTo = _random.Next(pointsCount);

                    if (!connections.ContainsKey(nodeConnectFrom))
                        connections.Add(nodeConnectFrom, 0);

                    if (nodeConnectTo == nodeConnectFrom ||
                        connections[nodeConnectFrom] >= maxConnectionToOneNode) continue;

                    connections[nodeConnectFrom]++;
                    result.Add(new Triplet<int, int, int>(nodeConnectFrom, nodeConnectTo,
                        (int)Vector2.Distance(points[nodeConnectFrom], points[nodeConnectTo])));
                    break;
                }
            }

            return result.ToArray();
        }

        public static KeyValuePair<int, Triplet<int, int, int>[]> GenerateRandomGraph(int nodesCount)
        {
            _random = new Random(DateTime.Now.Millisecond);
            int amountOfConnection = _random.Next(nodesCount, nodesCount * 2);
            int maxConnectionToOneNode = _random.Next(nodesCount % 2 + 1, 5);
            int nodeConnectFrom;
            int nodeConnectTo;
            Dictionary<int, int> connections =
                new Dictionary<int, int>();
            List<Triplet<int, int, int>> result = new List<Triplet<int, int, int>>();
            for (int i = 0; i < amountOfConnection; i++)
            {
                while (true)
                {
                    nodeConnectFrom = _random.Next(nodesCount);
                    nodeConnectTo = _random.Next(nodesCount);

                    if (!connections.ContainsKey(nodeConnectFrom))
                        connections.Add(nodeConnectFrom, 0);

                    if (nodeConnectTo == nodeConnectFrom ||
                        connections[nodeConnectFrom] >= maxConnectionToOneNode) continue;

                    connections[nodeConnectFrom]++;
                    result.Add(new Triplet<int, int, int>(nodeConnectFrom, nodeConnectTo,
                        _random.Next(MIN_DistanceBetweenNodes, MAX_DistanceBetweenNodes)));
                    break;
                }
            }

            return new KeyValuePair<int, Triplet<int, int, int>[]>(nodesCount, result.ToArray());
        }
    }
}
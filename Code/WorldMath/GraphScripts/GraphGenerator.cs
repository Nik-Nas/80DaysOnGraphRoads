using System;
using System.Collections.Generic;
using static ITCampFinalProject.Code.WorldMath.AdvancedMath;
using System.Linq;
using ITCampFinalProject.Code.Utils;

namespace ITCampFinalProject.Code.WorldMath.GraphScripts
{
//ReSharper disable InconsistentNaming
    public class GraphGenerator
    {
        public static float MIN_DistanceBetweenNodes
        {
            get => _minDistBetweenNodes;
            set => _minDistBetweenNodes = value > 0 ? value : 10f;
        }

        public static float MAX_DistanceBetweenNodes
        {
            get => _maxDistBetweenNodes;
            set => _maxDistBetweenNodes = value <= 10E6F ? value : 10E6F;
        }

        private static float _minDistBetweenNodes = 10f;
        private static float _maxDistBetweenNodes = 1000f;
        private static Random _random;

        public static List<Vector2> GenerateRandomGraph(int nodesCount)
        {
            if (nodesCount <= 0) return new List<Vector2>();
            _random = new Random(DateTime.Now.Millisecond);
            List<Vector2> result = 
                new List<Vector2>();
            for (int i = 0; i < nodesCount; i++)
            {
                float distanceToClosetPoint = float.MaxValue;
                while (true)
                {
                    //generating a random vector
                    Vector2 tryVec = new Vector2(NextFloat(_random), NextFloat(_random));

                    distanceToClosetPoint = result.Select(generatedPoint => 
                        Vector2.Distance(tryVec, generatedPoint)).Prepend(distanceToClosetPoint).Min();

                    if (distanceToClosetPoint < _minDistBetweenNodes ||
                        distanceToClosetPoint > _maxDistBetweenNodes) continue;
                    result.Add(tryVec);
                    break;
                }
            }
            return result;
        }

        public static Triplet<int, int, int>[] ConvertPointsListToRandomGraph(List<Vector2> points)
        {
            int pointsCount = points.Count;
            _random = new Random(DateTime.Now.Millisecond);
            int amountOfConnection = _random.Next(pointsCount * 2);
            int maxConnectionToOneNode = _random.Next(pointsCount % 2 + 1, amountOfConnection / pointsCount);
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
                    if (nodeConnectTo == nodeConnectFrom ||
                        connections[nodeConnectFrom] >= maxConnectionToOneNode) continue;
                    
                    if(!connections.ContainsKey(nodeConnectFrom)) 
                        connections.Add(nodeConnectFrom, 0);
                    
                    connections[nodeConnectFrom]++;
                        result.Add(new Triplet<int, int, int>(nodeConnectFrom, nodeConnectTo, 
                            (int) Vector2.Distance(points[nodeConnectFrom], points[nodeConnectTo])));
                    break;
                }
            }

            return result.ToArray();
        }
    }
}
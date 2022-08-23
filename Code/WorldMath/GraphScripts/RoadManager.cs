using System;
using System.Collections.Generic;
using System.Drawing;

namespace ITCampFinalProject.Code.WorldMath.GraphScripts
{
    public class RoadManager
    {
        private Random _random;

        public RoadManager()
        {
            GraphGenerator.MAX_DistanceBetweenNodes = 30;
        }

        public Bitmap GetRoad()
        {
            //updating random
            _random = new Random(DateTime.Now.Millisecond);
            //generating set of random points. Count of nodes generates randomly
            List<Vector2> points = GraphGenerator.GenerateRandomGraph( _random.Next(10, 25));
            WeightedOrientedGraph graph = ConvertPointsSetToRandomGraph(points);
            HashSet<int> way = ShortestPathFinder.FindShortestPath(graph, 0, graph.NodesCount - 1);
        }

        private WeightedOrientedGraph ConvertPointsSetToRandomGraph(List<Vector2> set)
        {
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using ITCampFinalProject.Code.Drawing;
using ITCampFinalProject.Code.Utils;

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
            Triplet<int, int, int>[] graph = GraphGenerator.ConvertPointsListToRandomGraph(points);
            WeightedOrientedGraph realGraph = new WeightedOrientedGraph(graph.Length, graph);
            List<int> way = realGraph.GetShortestPath(realGraph, 0, points.Count - 1).Value;
            return VisualizedGraph.VisualizeGraph(new KeyValuePair<List<Vector2>, 
                Triplet<int, int, int>[]>(points, graph), 5);
        }

        /*private WeightedOrientedGraph ConvertPointsSetToRandomGraph(List<Vector2> set)
        {
            
        }*/
    }
}
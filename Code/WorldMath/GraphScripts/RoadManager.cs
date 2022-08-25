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
            GraphGenerator.MAX_DistanceBetweenNodes = 100;
        }

        public Bitmap GetRoad(Size screenSize)
        {
            //updating random
            _random = new Random(DateTime.Now.Millisecond);
            //generating set of random points. Count of nodes generates randomly
            KeyValuePair<int, Triplet<int, int, int>[]> points = GraphGenerator.GenerateRandomGraph(_random.Next(10, 25));
            //Triplet<int, int, int>[] graph = GraphGenerator.ConvertPointsListToRandomGraph(points);
            foreach (Triplet<int, int, int> item in points.Value)
            {
                Console.WriteLine($"{item.Key} {item.Value} {item.Argument}");
            }
            WeightedOrientedGraph realGraph = new WeightedOrientedGraph(points.Key, points.Value);
            Console.WriteLine(points.Value.Length - 1 + " " + points.Value.Length);
            List<int> way = realGraph.GetShortestPath(realGraph, 0, realGraph.NodesCount - 1).Value;
            return VisualizedGraph.VisualizeGraph(points.Value, 1, screenSize);
        }

        /*private WeightedOrientedGraph ConvertPointsSetToRandomGraph(List<Vector2> set)
        {
            
        }*/
    }
}
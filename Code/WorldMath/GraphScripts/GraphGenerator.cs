using System;
using System.Collections.Generic;
using static ITCampFinalProject.Code.WorldMath.AdvancedMath;
using System.Linq;

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
            List<Vector2> result = new List<Vector2>();
            for (int i = 0; i < nodesCount; i++)
            {
                while (true)
                {
                    //generating a random vector
                    Vector2 tryVec = new Vector2(NextFloat(_random), NextFloat(_random));
                    
                    //checking if any of generated
                    if (result.Any(point =>
                        {
                            float distance = Vector2.Distance(point, tryVec);
                            return distance > _maxDistBetweenNodes || distance < _minDistBetweenNodes;
                        })) continue;
                    result.Add(tryVec);
                    break;
                }
            }

            return result;
        }
    }
}
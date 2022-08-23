using ITCampFinalProject.Code.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

//ReSharper disable InconsistentNaming
namespace ITCampFinalProject.Code.WorldMath.GraphScripts
{
    public class WeightedAndOrientedGraph
    {
        private WightedMatrix data;
        private int nodesCount;
        private int edgesCount;

        public int NodesCount => nodesCount;

        public int EdgesCount => edgesCount;

        public WeightedAndOrientedGraph(int nodesCount, params Triplet<int, int, int>[] nodesLengths)
        {
            if (nodesCount < 0) throw new ArgumentException("count matrix element must be more than -1!!!");
            if (nodesLengths.Any(triplet =>
            triplet.aValue >= nodesCount &&
            triplet.bValue >= nodesCount &&
            triplet.cValue < 0)) throw new ArgumentException("forbidden value in nodesLengths");
            data = new WightedMatrix(nodesCount);
            foreach (Triplet<int, int, int> dataSet in nodesLengths)
            {
                data.SetValue(dataSet.aValue, dataSet.bValue, dataSet.cValue);
            }
        }

        public int GetValue(int row, int col) => data.GetValue(row, col);
        public void SetValue(int row, int col, int value) => data.SetValue(row, col, value);




        private class WightedMatrix
        {
            private int[][] matrix;
            public readonly int Dimension;

            public WightedMatrix(int nodesCount)
            {
                if (nodesCount < 0) throw new ArgumentException("count matrix element must be more than zero!!!");
                matrix = new int[nodesCount][];
                for (int i = 0; i < matrix.Length; i++) matrix[i] = new int[nodesCount];
                Dimension = nodesCount;

            }

            public void SetValue(int row, int col, int value)
            {
                if (row < 0 || col < 0 || row >= Dimension || col >= Dimension)
                    throw new ArgumentException("index of matrix element cannot be less than zero!!!");
                matrix[row][col] = value;
            }

            public int GetValue(int row, int col)
            {
                if (row < 0 || col < 0 || row >= Dimension || col >= Dimension)
                    throw new ArgumentException("index of matrix element cannot be less than zero!!!");
                return matrix[row][col];
            }
        }
    }
}

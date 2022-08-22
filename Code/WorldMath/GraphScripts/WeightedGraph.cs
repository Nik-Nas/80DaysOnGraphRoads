using ITCampFinalProject.Code.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCampFinalProject.Code.WorldMath.GraphScripts
{
    class WeightedGraph
    {
        private WightedMatrix data;
        private int nodesCount;
        private int edgesCount;

        public WeightedGraph(int nodesCount, params Triplet<int, int, int>[] nodesLengths)
        {
            if (nodesCount <= 0) throw new ArgumentException("count matrix element must be more than zero!!!");
            if (nodesLengths.Any(triplet =>
            triplet.aValue > nodesCount &&
            triplet.bValue > nodesCount &&
            triplet.cValue <= 0)) throw new ArgumentException("forbidden value in nodesLengths");
            data = new WightedMatrix(nodesCount);
            for (int i = 0; i < nodesLengths.Length; i++)
            {
                data.SetValue(nodesLengths[i].aValue - 1, nodesLengths[i].bValue - 1, nodesLengths[i].cValue);
            }
        }


        private class WightedMatrix
        {
            private int[][] matrix;
            public readonly int Dimension;

            public WightedMatrix(int nodesCount)
            {
                if (nodesCount <= 0) throw new ArgumentException("count matrix element must be more than zero!!!");
                matrix = new int[nodesCount][];
                for (int i = 0; i < matrix.Length; i++) matrix[i] = new int[nodesCount];
                Dimension = nodesCount;

            }

            public void SetValue(int row, int col, int value)
            {
                if (row < 0 || col < 0) throw new ArgumentException("index of matrix element cannot be less than zero!!!");
                matrix[row][col] = value;
                matrix[col][row] = value;
            }

            public int GetValue(int row, int col)
            {
                if (row < 0 || col < 0) throw new ArgumentException("index of matrix element cannot be less than zero!!!");
                return matrix[row][col];
            }
        }
    }
}

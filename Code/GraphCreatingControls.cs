using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using ITCampFinalProject.Code.Drawing;
using ITCampFinalProject.Code.Utils;
using ITCampFinalProject.Code.WorldMath;
using ITCampFinalProject.Code.WorldMath.GraphScripts;

namespace ITCampFinalProject.Code
{
    public class GraphCreatingControls
    {
        public ReadOnlyCollection<Sprite> Nodes => _nodes.AsReadOnly();
        public Bitmap NodeTexture;
        public Renderer TargetRenderer { get; private set; }
        public Size NodeSize;
        public readonly RenderingLayer Layer;
        public NodeSelectingMode mode;

        public Sprite SelectedNode
        {
            get => _selectedNode;
            set => _selectedNode = _nodes.Contains(value) ? value : null;
        }

        private List<Sprite> _nodes = new List<Sprite>();
        private HashSet<Triplet<int, int, int>> _edges = new HashSet<Triplet<int, int, int>>();
        private int[] selectingEdge = {-1, -1};
        private Sprite _selectedNode;

        public void AddNode(Vector2 point)
        {
            Sprite newNode = new Sprite(NodeTexture, NodeSize, point, Layer);
            _nodes.Add(newNode);
            TargetRenderer.AddSpriteToRenderingStack(newNode);
            _selectedNode = newNode;
        }   

        public void RemoveSelectedNode()
        {
            if (!_nodes.Remove(_selectedNode))
            {
                Console.WriteLine(@"Can't remove node");
                return;
            }

            TargetRenderer.RemoveSpriteFromRenderingStack(_selectedNode);
            _selectedNode = null;
        }

        public void RemoveNode(int index)
        {
            try
            {
                TargetRenderer.RemoveSpriteFromRenderingStack(_nodes[index]);
                _nodes.RemoveAt(index);
            }
            catch (Exception)
            {
                Console.WriteLine(@"Can't remove node");
            }
        }

        public bool AddEdge(int nodeFrom, int nodeTo)
        {
            if (nodeFrom == nodeTo) return false;

            _edges.Add(new Triplet<int, int, int>(nodeFrom, nodeTo, (int) Vector2.Distance(
                    _nodes[nodeFrom].transform.centeredPosition, _nodes[nodeTo].transform.centeredPosition)));
            return true;
        }

        public WeightedOrientedGraph ConvertDataToGraph()
        {
            /*List<Triplet<int, int, int>> connections =
                _edges.Select(nodesPair => new Triplet<int, int, int>(nodesPair.Key, nodesPair.Value,
                    (int) Vector2.Distance(_nodes[nodesPair.Key].transform.centeredPosition,
                        _nodes[nodesPair.Value].transform.centeredPosition))).ToList();*/
            WeightedOrientedGraph graph = new WeightedOrientedGraph(_nodes.Count, _edges.ToArray());
            return graph;
        }

        public GraphCreatingControls(Bitmap nodeTexture, Renderer targetRenderer, Size nodeSize, RenderingLayer layer)
        {
            NodeTexture = nodeTexture;
            NodeSize = nodeSize;
            TargetRenderer = targetRenderer;
            Layer = layer;
        }

        public void SelectNode(int index)
        {
            _selectedNode = _nodes[index];
        }

        public void SelectNode(Sprite node)
        {
            _selectedNode = node;
        }

        public void Deselect()
        {
            _selectedNode = null;
        }

        public void ProcessClick(Vector2 clickCoords)
        {
            Triplet<bool, Sprite, int> isAnyNodeClicked = IsClickedToNode(clickCoords);
            if (!isAnyNodeClicked.Key)
            {
                Deselect();
                return;
            }

            switch (mode)
            {
                case NodeSelectingMode.ForMoving:
                    SelectNode(isAnyNodeClicked.Value);
                    break;
                case NodeSelectingMode.ForCreatingEdge:
                    ConnectEdges(isAnyNodeClicked.Argument);
                    break;
            }
        }

        public Triplet<bool, Sprite, int> IsClickedToNode(Vector2 clickCoords)
        {
            /*foreach (var node in _nodes.Where(node => */
            for (int i = 0; i < _nodes.Count; i++)
            {
                if (Vector2.Distance(_nodes[i].transform.centeredPosition, clickCoords) <=
                    _nodes[i].transform.Size.Width << 1)
                    return new Triplet<bool, Sprite, int>(true, _nodes[i], i);
            }

            return new Triplet<bool, Sprite, int>(false, null, -1);
        }

        private bool ConnectEdges(int nodeIndex)
        {
            if (selectingEdge[0] == -1)
            {
                selectingEdge[0] = nodeIndex;
                return false;
            }

            mode = NodeSelectingMode.ForMoving;
            selectingEdge[1] = nodeIndex;
            TargetRenderer.primitives.Add(new Line(_nodes[selectingEdge[0]],
                _nodes[selectingEdge[1]], 30, Color.Orange));
            
            _edges.Add(new Triplet<int, int, int>(selectingEdge[0], selectingEdge[1], (int) Vector2.Distance(
                _nodes[selectingEdge[0]].transform.centeredPosition, _nodes[selectingEdge[1]].transform.centeredPosition)));
            
            selectingEdge = new[] {-1, -1};
            return true;
        }
    }

    public enum NodeSelectingMode
    {
        ForMoving,
        ForCreatingEdge
    }
}
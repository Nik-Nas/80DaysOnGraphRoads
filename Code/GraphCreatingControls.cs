using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ITCampFinalProject.Code.Drawing;
using ITCampFinalProject.Code.Utils;
using ITCampFinalProject.Code.WorldMath;
using ITCampFinalProject.Code.WorldMath.GraphScripts;
using static ITCampFinalProject.Code.Drawing.DrawingUtils;

namespace ITCampFinalProject.Code
{
    public class GraphCreatingControls
    {
        public MultiTextureSprite SelectedNode
        {
            get => _selectedNode;
            set => _selectedNode = _nodes.Contains(value) ? value : null;
        }
        public ReadOnlyCollection<MultiTextureSprite> Nodes => _nodes.AsReadOnly();
        public ReadOnlyCollection<Triplet<int, int, int>> Edges => _edges.AsReadOnly();
        public Bitmap NodeTexture;
        public Bitmap SelectedNodeTexture;
        public Renderer TargetRenderer { get; private set; }
        public Size NodeSize;
        public readonly RenderingLayer Layer;
        public NodeSelectingMode Mode;

        private List<MultiTextureSprite> _nodes = new List<MultiTextureSprite>();
        private List<Triplet<int, int, int>> _edges = new List<Triplet<int, int, int>>();
        private List<Line> _edgesLines = new List<Line>();
        private int[] _selectingEdge = {-1, -1};
        private MultiTextureSprite _selectedNode;
        
        public delegate void OnNodeSelectionChanged(bool isSelected, MultiTextureSprite node, int index);

        public OnNodeSelectionChanged OnNodeSelectionChangedCallback;


        #region NodesOperations

        public void AddNode(Vector2 point)
        {
            MultiTextureSprite newNode = new MultiTextureSprite(new List<Bitmap> 
                {DrawTextOnTexture(NodeTexture, _nodes.Count + 1,
                    TextAlignment.CenterMiddle, Color.White), 
                    DrawTextOnTexture(SelectedNodeTexture, _nodes.Count + 1,
                    TextAlignment.CenterMiddle, Color.White)}, NodeSize, point, Layer);

            _nodes.Add(newNode);
            TargetRenderer.AddSpriteToRenderingStack(newNode);
            Deselect();
            SelectNode(newNode);
        }

        public void RemoveSelectedNode()
        {
            int selectedNodeIndex = _nodes.IndexOf(_selectedNode);

            if (selectedNodeIndex == -1)
            {
                Console.WriteLine(@"Can't remove node");
                return;
            }
            
            TargetRenderer.RemoveSpriteFromRenderingStack(_selectedNode);
            for (int i = 0; i < _edges.Count; i++)
            {
                if (_edges[i].Key != selectedNodeIndex && _edges[i].Value != selectedNodeIndex) continue;
                _edges.Remove(_edges[i]);
                TargetRenderer.primitives.Remove(_edgesLines[i]);
                _edgesLines.RemoveAt(i);
                i--;
            }
            Deselect();
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

        public void SelectNode(int index)
        {
            try
            {
                _selectedNode = _nodes[index];
                _selectedNode.SetTexture(1);
                OnNodeSelectionChangedCallback?.Invoke(true, _selectedNode, index);
            }
            catch (Exception)
            {
                MessageBox.Show(@"can't find element in _nodes[] with index " + index);
            }

        }

        public void SelectNode(MultiTextureSprite node)
        {
            try
            {
                _selectedNode = node;
                _selectedNode.SetTexture(1);
                OnNodeSelectionChangedCallback?.Invoke(true, _selectedNode, _nodes.IndexOf(node));
            }
            catch (Exception)
            {
                MessageBox.Show(@"Can't find argument node in _nodes list");
            }
        }

        public void Deselect()
        {
            _selectedNode?.ResetTexture();
            OnNodeSelectionChangedCallback?.Invoke(false, _selectedNode, _nodes.IndexOf(_selectedNode));
            _selectedNode = null;
        }

        public Triplet<bool, MultiTextureSprite, int> IsClickedToNode(Vector2 clickCoords)
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                if (Vector2.Distance(_nodes[i].transform.centeredPosition, clickCoords) <=
                    _nodes[i].transform.Size.Width)
                    return new Triplet<bool, MultiTextureSprite, int>(true, _nodes[i], i);
            }

            return new Triplet<bool, MultiTextureSprite, int>(false, null, -1);
        }

        #endregion

        #region EdgesOperations

        public bool AddEdge(int nodeFrom, int nodeTo)
        {
            if (nodeFrom == nodeTo) return false;

            _edges.Add(new Triplet<int, int, int>(nodeFrom, nodeTo, (int) Vector2.Distance(
                _nodes[nodeFrom].transform.centeredPosition, _nodes[nodeTo].transform.centeredPosition)));
            return true;
        }

        public WeightedOrientedGraph ConvertDataToGraph()
        {
            WeightedOrientedGraph graph = new WeightedOrientedGraph(_nodes.Count, _edges.ToArray());
            return graph;
        }

        public GraphCreatingControls(Bitmap nodeTexture, Bitmap selectedNodeTexture, Renderer targetRenderer,
            Size nodeSize, RenderingLayer layer)
        {
            NodeTexture = nodeTexture;
            SelectedNodeTexture = selectedNodeTexture;
            NodeSize = nodeSize;
            TargetRenderer = targetRenderer;
            Layer = layer;
        }

        private bool ConnectEdges(int nodeIndex)
        {
            if (_selectingEdge[0] == -1)
            {
                _selectingEdge[0] = nodeIndex;
                return false;
            }

            Mode = NodeSelectingMode.ForMoving;
            _selectingEdge[1] = nodeIndex;
            if (!_edges.Any(edge => edge.Key == _selectingEdge[0] && edge.Value == _selectingEdge[1]))
            {
                _edgesLines.Add(new Line(_nodes[_selectingEdge[0]],
                    _nodes[_selectingEdge[1]], 30, Color.Orange));

                TargetRenderer.primitives.Add(_edgesLines[_edgesLines.Count - 1]);

                _edges.Add(new Triplet<int, int, int>(_selectingEdge[0], _selectingEdge[1], (int) Vector2.Distance(
                    _nodes[_selectingEdge[0]].transform.centeredPosition,
                    _nodes[_selectingEdge[1]].transform.centeredPosition)));
                _selectingEdge = new[] {-1, -1};
                return true;
            }

            _selectingEdge = new[] {-1, -1};
            return false;
        }
        
        #endregion
        
        public void ProcessClick(Vector2 clickCoords)
        {
            Triplet<bool, MultiTextureSprite, int> isAnyNodeClicked = IsClickedToNode(clickCoords);
            if (!isAnyNodeClicked.Key)
            {
                Deselect();
                return;
            }

            switch (Mode)
            {
                case NodeSelectingMode.ForMoving:
                    SelectNode(isAnyNodeClicked.Value);
                    break;
                case NodeSelectingMode.ForCreatingEdge:
                    ConnectEdges(isAnyNodeClicked.Argument);
                    break;
            }
        }

        public void Reset()
        {
            _nodes = null;
            _nodes = new List<MultiTextureSprite>();
            _edges = null;
            _edges = new List<Triplet<int, int, int>>();
            TargetRenderer.ClearLayerInRenderingStack(Layer);
            TargetRenderer.primitives = null;
            TargetRenderer.primitives = new List<Line>();
            _selectedNode = null;
            _edgesLines = null;
            _edgesLines = new List<Line>();
            OnNodeSelectionChangedCallback?.Invoke(false, _selectedNode, _nodes.IndexOf(_selectedNode));
        }
    }

    public enum NodeSelectingMode
    {
        ForMoving,
        ForCreatingEdge
    }
}
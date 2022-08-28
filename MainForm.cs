using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using ITCampFinalProject.Code;
using ITCampFinalProject.Code.Drawing;
using ITCampFinalProject.Code.WorldMath;
using ITCampFinalProject.Code.WorldMath.GraphScripts;

namespace ITCampFinalProject
{
    public partial class MainForm : Form
    {
        private Renderer _renderer;
        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();
        private Sprite _player;
        private GraphCreatingControls _controls;
        private float _rotationSpeed = 2.0f;
        private bool _canMoveNode;

        public MainForm()
        {
            _renderer = new Renderer(CreateGraphics(), Size);
            InitializeComponent();

            _controls = new GraphCreatingControls(Properties.Resources.node_icon_128x128,
                Properties.Resources.node_icon_selected_128x128, _renderer,
                new Size(32, 32), RenderingLayer.Layer3);
            _controls.OnNodeSelectionChangedCallback += NodeSelectionListener;

            _renderer.ResizeRenderingWindow(Size.Width, Size.Height);

            _player = new Sprite(Properties.Resources.car_icon_512x256, new Size(32, 16),
                RenderingLayer.Layer8, 50, 100);
            _renderer.AddSpriteToRenderingStack(_player);

            addEdgeButton.Enabled = false;
            FPSTimer.Enabled = true;
            InputTimer.Enabled = false;
        }

        private void FpsTick(object sender, ElapsedEventArgs e)
        {
            _renderer.RenderStack();
        }

        private void ResizeWindow(object sender, EventArgs e)
        {
            _renderer.ResizeRenderingWindow(Size.Width, Size.Height);
            _renderer.SetScreenGraphics(CreateGraphics());
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_pressedKeys.Add(e.KeyCode)) InputTimer.Enabled = true;
        }

        private void OnKeyUp(object sender, KeyEventArgs args)
        {
            _pressedKeys.Remove(args.KeyCode);
            if (_pressedKeys.Count == 0) InputTimer.Enabled = false;
        }

        private void ProcessMotion()
        {
            float rotationMultiplier = _pressedKeys.Contains(Keys.W) ? 1f :
                _pressedKeys.Contains(Keys.S) ? -1 : 0;
            foreach (Keys keyCode in _pressedKeys)
            {
                switch (keyCode)
                {
                    case Keys.W:
                    {
                        _player.transform.MoveInDirection(_player.transform.forward.Normalized * 2f);
                        break;
                    }

                    case Keys.S:
                    {
                        _player.transform.MoveInDirection(-_player.transform.forward * 2f);
                        break;
                    }

                    case Keys.A:
                    {
                        _player.transform.Rotate(-_rotationSpeed * rotationMultiplier);
                        break;
                    }

                    case Keys.D:
                    {
                        _player.transform.Rotate(_rotationSpeed * rotationMultiplier);
                        break;
                    }
                }
            }
        }

        private void InputTimerTick(object sender, EventArgs e)
        {
            ProcessMotion();
        }

        private void AddNodeButton_Click(object sender, EventArgs e)
        {
            _controls.AddNode(new Vector2(150f, 150f));
            HintLabel.Text = @"Place node wherever you want";
            ResetFocus(sender);

            Activate();
        }

        private void DeleteNodeButton_Click(object sender, EventArgs e)
        {
            _controls.RemoveSelectedNode();
            ResetFocus(sender);

            Activate();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            _canMoveNode = true;
            _controls.ProcessClick(new Vector2(e.X, e.Y));
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            _canMoveNode = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            float x = AdvancedMath.Clamp(e.X, 100, Size.Width - 100);
            float y = AdvancedMath.Clamp(e.Y, 100, Size.Height - 100);
            
            if (_canMoveNode && _controls.Mode == NodeSelectingMode.ForMoving && _controls.SelectedNode != null)
            {
                _controls.SelectedNode.transform.SetPosition(x, y);
            }
        }

        private void AddEdgeButton_Click(object sender, EventArgs e)
        {
            HintLabel.Text = @"Select two nodes to make an edge between them";
            _controls.Mode = NodeSelectingMode.ForCreatingEdge;
            _canMoveNode = false;
            ResetFocus(sender);

            Activate();
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            if (_controls.Nodes.Count > 1)
            {
                WeightedOrientedGraph graph = _controls.Nodes.Count > 1 ? _controls.ConvertDataToGraph() : null;
                graph?.GetShortestPath(graph, 0, graph.NodesCount - 1);
                ShowWay(graph?.ShortestPath);
            }

            ResetFocus(sender);

            Activate();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            _controls.Reset();
            ResetFocus(sender);
        }

        private void ShowWay(IReadOnlyList<int> way)
        {
            for (int i = 1; i < way.Count; i++)
            {
                Line l = new Line(_controls.Nodes[way[i - 1]], _controls.Nodes[way[i]], 5, Color.Black);
                _renderer.primitives.Add(l);
            }

            if (way.Count <= 0) return;

            _player.transform.SetPosition(_controls.Nodes[way[0]].transform.position);
            _player.transform.LookAt(_controls.Nodes[way[1]].transform);
        }

        private void NodeSelectionListener(bool isSelected, MultiTextureSprite node, int index)
        {
            deleteNodeButton.Enabled = isSelected;
            addEdgeButton.Enabled = !isSelected && _controls.Nodes.Count > 1;
        }

        private void ResetFocus(object sender)
        {
            if (!(sender is Button b)) return;
            b.Enabled = false;
            b.Enabled = true;
        }
    }
}
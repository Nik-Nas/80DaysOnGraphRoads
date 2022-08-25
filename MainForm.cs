using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private bool _canMoveNode = false;

        public MainForm()
        {
            _renderer = new Renderer(CreateGraphics(), Size);
            InitializeComponent();

            _controls = new GraphCreatingControls(Properties.Resources.node_icon_128x128, _renderer,
                new Size(32, 32), RenderingLayer.Layer3);

            _renderer.ResizeRenderingWindow(Size.Width, Size.Height);

            _player = new Sprite(Properties.Resources.car_icon_512x256, new Size(32, 16),
                RenderingLayer.Layer8, 50, 50);

            _renderer.AddSpriteToRenderingStack(_player);
            /*RoadManager roadGenerator = new RoadManager();
            Bitmap road = roadGenerator.GetRoad(Size);
            _renderer.AddSpriteToRenderingStack(new Sprite(road, Size, Vector2.zero, RenderingLayer.Layer2));
            _renderer.SetScreenGraphics(CreateGraphics());

            /*Bitmap visualizedGraph = VisualizedGraph.VisualizeGraph(
                new[]
                {
                    new Vector2(15, 15),
                    new Vector2(45, 30),
                    new Vector2(30, 40)
                }, 2f);
            _renderer.AddSpriteToRenderingStack(new Sprite(visualizedGraph, 
                visualizedGraph.Size, new Vector2(150, 200), 1));*/
            Console.WriteLine(_renderer.RenderingMask);
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

                    case Keys.A when _pressedKeys.Contains(Keys.W) || _pressedKeys.Contains(Keys.S):
                    {
                        _player.transform.Rotate(-_rotationSpeed);
                        break;
                    }

                    case Keys.D when _pressedKeys.Contains(Keys.W) || _pressedKeys.Contains(Keys.S):
                    {
                        _player.transform.Rotate(_rotationSpeed);
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
            if (sender is Button b)
            {
                b.Enabled = false;
                b.Enabled = true;
            }

            Activate();
        }

        private void DeleteNodeButton_Click(object sender, EventArgs e)
        {
            _controls.RemoveSelectedNode();
            if (sender is Button b)
            {
                b.Enabled = false;
                b.Enabled = true;
            }

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
            if (_controls.mode == NodeSelectingMode.ForMoving && _controls.SelectedNode != null && _canMoveNode)
            {
                _controls.SelectedNode.transform.ChangePosition(e.X, e.Y);
            }
        }
        private void AddEdgeButton_Click(object sender, EventArgs e)
        {
            HintLabel.Text = @"Select two nodes to make an edge between them";
            _controls.mode = NodeSelectingMode.ForCreatingEdge;
            _canMoveNode = false;
            if (sender is Button b)
            {
                b.Enabled = false;
                b.Enabled = true;
            }

            Activate();

        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            WeightedOrientedGraph graph = _controls.Nodes.Count > 1 ? _controls.ConvertDataToGraph() : null;
            string s = "";
            foreach(int way in graph?.GetShortestPath(graph, 0, graph.NodesCount - 1).Value ?? new List<int>())
                s += way + "\n";
            
            if (sender is Button b)
            {
                b.Enabled = false;
                b.Enabled = true;
            }

            Activate();

            ShowWay(graph?.ShortestPath);
            //if (s.Length > 0) MessageBox.Show(s);
        }

        private void ShowWay(IReadOnlyList<int> way)
        {
            for (int i = 1; i < way.Count; i++)
            {
                Line l = new Line(_controls.Nodes[way[i - 1]], _controls.Nodes[way[i]], 5, Color.Black);
                _renderer.primitives.Add(l);
            }
            _player.transform.ChangePosition(_controls.Nodes[way[0]].transform.position);
        }
    }
}
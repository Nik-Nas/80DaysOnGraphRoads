using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using ITCampFinalProject.Code.Drawing;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject
{
    public partial class MainForm : Form
    {
        private Renderer _renderer;
        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();
        private Sprite _player;
        private float _rotationSpeed = 2.0f;

        public MainForm()
        {
            _renderer = new Renderer(CreateGraphics(), Size);
            InitializeComponent();
            _renderer.ResizeRenderingWindow(Size.Width, Size.Height);
            _renderer.SetScreenGraphics(CreateGraphics());
            _player = new Sprite(Properties.Resources.car_icon_512x256, new Size(64, 32), 1, 50, 50);
            _renderer.AddSpriteToRenderingStack(_player);
            /*Bitmap visualizedGraph = VisualizedGraph.VisualizeGraph(
                new[]
                {
                    new Vector2(15, 15),
                    new Vector2(45, 30),
                    new Vector2(30, 40)
                }, 2f);
            _renderer.AddSpriteToRenderingStack(new Sprite(visualizedGraph, 
                visualizedGraph.Size, new Vector2(150, 200), 1));*/
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
    }
}

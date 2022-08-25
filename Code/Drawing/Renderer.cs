using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject.Code.Drawing
{
    public class Renderer
    {
        public ReadOnlyCollection<List<Sprite>> RenderingStack => _renderingStack.Values.ToList().AsReadOnly();
        public byte RenderingMask;
        private Dictionary<RenderingLayer, List<Sprite>> _renderingStack;
        public List<Line> primitives;
        private Bitmap _buffer;
        private Graphics _bufferGraphics;
        private Graphics _screenGraphics;

        public Renderer(Graphics targetScreenGraphics, Size screenSize)
        {
            _screenGraphics = targetScreenGraphics;
            primitives = new List<Line>();
            _renderingStack = new Dictionary<RenderingLayer, List<Sprite>>();
            for (int i = 1; i < 256; i *= 2)
            {
                _renderingStack.Add((RenderingLayer) i, new List<Sprite>());
            }
            _buffer = new Bitmap(screenSize.Width, screenSize.Height);
            _bufferGraphics = Graphics.FromImage(_buffer);
        }

        public void RenderStack()
        {
            _bufferGraphics.Clear(Color.White);
            foreach (Line line in primitives)
            {
                _bufferGraphics.DrawLine(line.pen, (int) line.start.transform.position.x, (int) line.start.transform.position.y, 
                    (int) line.end.transform.position.x, (int) line.end.transform.position.y);
            }
            foreach(Sprite sprite in _renderingStack.Where(layer => 
                            (byte) ((byte) layer.Key & RenderingMask) != 0).
                        SelectMany(layer => layer.Value))
            {
                _bufferGraphics.DrawImage(sprite.rotatedTexture,
                    sprite.transform.centeredPosition.x,
                    sprite.transform.centeredPosition.y);
            }

            _screenGraphics.DrawImage(_buffer, 0, 0);
        }
        
        public void ResizeRenderingWindow(int newWidth, int newHeight)
        {
            _buffer.Dispose();
            _bufferGraphics.Dispose();
            _buffer = new Bitmap(newWidth, newHeight);
            _bufferGraphics = Graphics.FromImage(_buffer);
        }

        public void AddSpriteToRenderingStack(Sprite sprite)
        {
            if (_renderingStack[sprite.layer].Count == 0) EnableLayer(sprite.layer);
                _renderingStack[sprite.layer].Add(sprite);
        }

        public bool RemoveSpriteFromRenderingStack(Sprite sprite)
        {
            if (_renderingStack[sprite.layer].Count == 0) return false;
            if (!_renderingStack[sprite.layer].Remove(sprite)) return false;
            if (_renderingStack[sprite.layer].Count == 0) DisableLayer(sprite.layer);
            return true;
        }

        public void EnableLayer(RenderingLayer layer)
        {
            byte layerValue = (byte) layer;
            if ((byte) (RenderingMask & layerValue) == 0)
            {
                RenderingMask |= layerValue;
            }
        }

        public void DisableLayer(RenderingLayer layer)
        {
            RenderingMask ^= (byte) layer;
        }

        public void SetScreenGraphics(Graphics graphics)
        {
            _screenGraphics.Dispose();
            _screenGraphics = graphics;
        }
    }

    public enum RenderingLayer
    {
        Layer1 = 0b00000001,
        Layer2 = 0b00000010,
        Layer3 = 0b00000100,
        Layer4 = 0b00001000,
        Layer5 = 0b00010000,
        Layer6 = 0b00100000,
        Layer7 = 0b01000000,
        Layer8 = 0b10000000
    }
}
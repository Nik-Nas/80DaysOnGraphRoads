using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace ITCampFinalProject.Code.Drawing
{
    class Renderer
    {
        public ReadOnlyCollection<List<Sprite>> RenderingStack => _renderingStack.Values.ToList().AsReadOnly();
        public byte RenderingMask;
        private readonly Dictionary<int, List<Sprite>> _renderingStack;
        private Bitmap _buffer;
        private Graphics _bufferGraphics;
        private Graphics _screenGraphics;

        public Renderer(Graphics targetScreenGraphics, Size screenSize)
        {
            _screenGraphics = targetScreenGraphics;
            _renderingStack = new Dictionary<int, List<Sprite>>();
            _buffer = new Bitmap(screenSize.Width, screenSize.Height);
            _bufferGraphics = Graphics.FromImage(_buffer);
        }

        public void RenderStack()
        {
            _bufferGraphics.Clear(Color.White);
            foreach (Sprite sprite in _renderingStack.Where(layeredSprite =>
                         (layeredSprite.Key & RenderingMask) != 0)
                         .SelectMany(layeredSprite => layeredSprite.Value))
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
            if (_renderingStack.ContainsKey(sprite.layer) && _renderingStack.TryGetValue(sprite.layer, out List<Sprite> sprites))
            {
                sprites.Add(sprite);
            }
            else
            {
                _renderingStack.Add(sprite.layer, new List<Sprite>(new[] {sprite}));
                RenderingMask |= sprite.layer;
            }
            //_renderingStack.Add(sprite);
        }

        public bool RemoveSpriteFromRenderingStack(Sprite sprite)
        {
            //return _renderingStack.Remove(sprite);
            if (!_renderingStack.TryGetValue(sprite.layer, out List<Sprite> sprites)) return false;
            if (!sprites.Remove(sprite)) return false;
            RenderingMask ^= sprite.layer;
            return true;

        }

        public void SetScreenGraphics(Graphics graphics)
        {
            _screenGraphics = graphics;
        }
    }
}
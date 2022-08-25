using System;
using System.Drawing;
using ITCampFinalProject.Code.GameObjects;
using ITCampFinalProject.Code.WorldMath;
// ReSharper disable InconsistentNaming

namespace ITCampFinalProject.Code.Drawing
{
    public class Sprite : Component
    {
        public readonly Bitmap texture;
        public Bitmap rotatedTexture;
        public Transform transform;
        public readonly RenderingLayer layer;
        
        private void OnSizeChanged(int width, int height)
        {
            rotatedTexture = DrawingUtils.ResizeImage(rotatedTexture, width, height);
        }

        private void OnRotated(float angle)
        {
            rotatedTexture = DrawingUtils.RotateImage(texture, transform.Angle);
            transform.SetSize(rotatedTexture.Width, rotatedTexture.Height, false);
        }


        #region Constructors

        public Sprite(Bitmap texture, Size size, Vector2 position, RenderingLayer layer, float angle = 0)
        {
            this.texture = DrawingUtils.ResizeImage(texture, size.Width, size.Height);
            rotatedTexture = Math.Abs(angle) > 0.01 ? DrawingUtils.RotateImage(this.texture, angle) : this.texture;
            
            transform = new Transform(position, 0, size);
            //adding methods to transform delegates to synchronize transform changes with texture changes
            transform.OnTransformSizeChangedCallback += OnSizeChanged;
            transform.OnTransformRotatedCallback += OnRotated;
            //assigning layer to get ability use render mask
            this.layer = layer;
        }

        public Sprite(Bitmap texture, Size size, RenderingLayer layer, float x = 0, float y = 0, float angle = 0)
        {
            //creating texture with given configuration
            this.texture = DrawingUtils.ResizeImage(texture, size.Width, size.Height);
            rotatedTexture = Math.Abs(angle) > 0.01 ? DrawingUtils.RotateImage(this.texture, angle) : this.texture;
            //creating transform
            transform = new Transform(x, y, angle, size);
            //adding methods to transform delegates to synchronize transform changes with texture changes
            transform.OnTransformSizeChangedCallback += OnSizeChanged;
            transform.OnTransformRotatedCallback += OnRotated;
            //assigning layer to get ability use render mask
            this.layer = layer;
        }
        
        #endregion
    }
}

using System;
using System.Drawing;
//ReSharper disable InconsistentNaming 

namespace ITCampFinalProject.Code.WorldMath
{
    public class Transform
    {
        public Vector2 position;
        public Vector2 center { get; private set; }
        public Vector2 centeredPosition => _centeredPosition;
        private Vector2 _centeredPosition;
        private Size _size;

        #region Delegates

        public delegate void OnTransformSizeChanged(int newWidth, int newHeight);

        public delegate void OnTransformRotated(float angle);

        public OnTransformSizeChanged OnTransformSizeChangedCallback;
        public OnTransformRotated OnTransformRotatedCallback;
        
        #endregion


        public float Angle
        {
            get => _angle;
            private set => _angle = AdvancedMath.CircularClamp(value, 0f, 359.999f);
        }
        
        public Vector2 forward { get; private set; }
        
        private float _angle;

        public void ChangePosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void ChangePosition(float x, float y)
        {
            position.x = x;
            position.y = y;

            RecalculateCenteredPosition();
        }

        public void MoveInDirection(Vector2 direction)
        {
            position += direction;
            
            RecalculateCenteredPosition();
        }

        public void Rotate(float angleDelta)
        {
            Angle += angleDelta;
            forward = new Vector2((float) Math.Cos(Angle * AdvancedMath.DEG_TO_RAD),
                (float) Math.Sin(Angle * AdvancedMath.DEG_TO_RAD));
            
            OnTransformRotatedCallback?.Invoke(Angle);
        }

        public void Scale(float scaleFactor, bool callEvent = true)
        {
            //changing scale
            _size.Height = (int) (_size.Height * scaleFactor);
            _size.Width = (int) (_size.Width * scaleFactor);
            
            //recalculating center of transform
            center = new Vector2(_size.Width >> 1, _size.Height >> 1);
            
            //recalculating centeredPosition
            RecalculateCenteredPosition();
            
            //if needed, calling event
            if (callEvent) OnTransformSizeChangedCallback?.Invoke(_size.Width, _size.Width);
        }

        public void SetSize(int width, int height, bool callEvent = true)
        {
            //changing size of transform
            _size.Height = height;
            _size.Width = width;
            
            //recalculating center of transform
            center = new Vector2(_size.Width >> 1, _size.Height >> 1);
            
            RecalculateCenteredPosition();
            
            //if needed, calling event
            if (callEvent) OnTransformSizeChangedCallback?.Invoke(_size.Width, _size.Width);
        }

        public Transform(Vector2 position, float angle, Size size)
        {
            this.position = position;
            _angle = angle;
            Angle = _angle;
            _size = size;
            forward = new Vector2((float) Math.Cos(Angle), (float) Math.Sin(Angle));
            center = new Vector2(_size.Width >> 1, _size.Height >> 1);
            RecalculateCenteredPosition();
        }

        public Transform(float x, float y, float angle, Size size)
        {
            position = new Vector2(x, y);
            _angle = angle;
            Angle = _angle;
            _size = size;
            forward = new Vector2((float) Math.Cos(Angle), (float) Math.Sin(Angle));
            center = new Vector2(_size.Width >> 1, _size.Height >> 1);
            RecalculateCenteredPosition();
        }

        public void RecalculateCenteredPosition()
        {
            _centeredPosition.x = position.x - center.x;
            _centeredPosition.y = position.y - center.y;
        }
    }
}
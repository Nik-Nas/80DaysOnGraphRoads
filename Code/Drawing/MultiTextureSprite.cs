using System.Collections.Generic;
using System.Drawing;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject.Code.Drawing
{
    public class MultiTextureSprite : Sprite
    {
        public List<Bitmap> Textures;
        public Bitmap DefaultTexture { get; private set; }

        public MultiTextureSprite(List<Bitmap> textures, Size size, Vector2 position, RenderingLayer layer,
            float angle = 0)
            : base(textures[0], size, position, layer, angle)
        {
            Textures = textures;
            for (int i = 0; i < Textures.Count; i++)
            {
                Textures[i] = DrawingUtils.ResizeImage(Textures[i], size.Width, size.Height);
            }
            SetDefaultTexture(textures[0]);
        }

        public MultiTextureSprite(List<Bitmap> textures, Size size, RenderingLayer layer, float x = 0, float y = 0,
            float angle = 0)
            : base(textures[0], size, layer, x, y, angle)
        {
            Textures = textures;
            for (int i = 0; i < Textures.Count; i++)
            {
                Textures[i] = DrawingUtils.ResizeImage(Textures[i], size.Width, size.Height);
            }
            SetDefaultTexture(textures[0]);
        }

        public void SetTexture(int index)
        {
            texture = Textures[index];
            OnRotated(transform.Angle);
        }

        public void SetDefaultTexture(Bitmap newDefaultTexture)
        {
            DefaultTexture = newDefaultTexture;
            texture = DefaultTexture;
            OnRotated(transform.Angle);
        }

        public void ResetTexture()
        {
            texture = DefaultTexture;
            OnRotated(transform.Angle);
        }
    }
}
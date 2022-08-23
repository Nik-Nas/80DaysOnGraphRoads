using System.Drawing;
using System.Linq;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject.Code.Drawing
{
    public class VisualizedGraph
    {
        public static Bitmap VisualizeGraph(Vector2[] source, float scaleFactor)
        {
            float radiusOfNode = 3.5f * scaleFactor;
            float halfRadius = radiusOfNode / 2f;
            //creating empty Bitmap to draw on it
            Bitmap resultingImage = new Bitmap(
                (int) ((source.Max().x + radiusOfNode * scaleFactor) * scaleFactor),
                (int) ((source.Max().y + radiusOfNode * scaleFactor) * scaleFactor));
            
            //create graphics for this bitmap
            Graphics resultingImageGraphics = Graphics.FromImage(resultingImage);
            {
                //create custom pen to draw thick lines
                Pen customPen = new Pen(Color.Orange, 3 * scaleFactor);
                float x, y, prevX, prevY;
                for (int i = 1; i < source.Length; i++)
                {
                    //assigning temporary variables to avoid multiple calculating the same value
                    x = source[i].x * scaleFactor;
                    y = source[i].y * scaleFactor;
                    prevX = source[i - 1].x * scaleFactor;
                    prevY = source[i - 1].y * scaleFactor;

                    //draw the connecting line
                    resultingImageGraphics.DrawLine(customPen, x, y, prevX, prevY);

                    //draw the current node and previous node
                    resultingImageGraphics.FillEllipse(
                        Brushes.Black, x - halfRadius, y - halfRadius, radiusOfNode, radiusOfNode);
                    resultingImageGraphics.FillEllipse(
                        Brushes.Black, prevX - halfRadius, prevY - halfRadius, radiusOfNode, radiusOfNode);
                }
            }
            resultingImageGraphics.Dispose();
            return resultingImage;
        }
    }
}
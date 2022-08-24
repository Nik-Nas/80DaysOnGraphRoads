using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ITCampFinalProject.Code.Utils;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject.Code.Drawing
{
    public class VisualizedGraph
    {
        public static Bitmap VisualizeGraph(KeyValuePair<List<Vector2>, Triplet<int, int, int>[]> source, float scaleFactor)
        {
            float radiusOfNode = 3.5f * scaleFactor;
            float halfRadius = radiusOfNode / 2f;
            //creating empty Bitmap to draw on it
            Bitmap resultingImage = new Bitmap(
                (int) ((source.Key.Max().x + radiusOfNode * scaleFactor) * scaleFactor),
                (int) ((source.Key.Max().y + radiusOfNode * scaleFactor) * scaleFactor));
            
            //create graphics for this bitmap
            Graphics resultingImageGraphics = Graphics.FromImage(resultingImage);
            {
                //create custom pen to draw thick lines
                Pen customPen = new Pen(Color.Orange, 3 * scaleFactor);
                //float x, y, prevX, prevY;
                foreach (Triplet<int, int, int> node in source.Value)
                {
                    resultingImageGraphics.DrawLine(customPen, source.Key[node.Key].x, source.Key[node.Key].y,
                        source.Key[node.Value].x, source.Key[node.Value].y);

                    resultingImageGraphics.FillEllipse(
                        Brushes.Black, source.Key[node.Key].x - halfRadius, source.Key[node.Key].x - halfRadius,
                        radiusOfNode, radiusOfNode);
                    
                }
                /*for (int i = 1; i < source.Key.Length; i++)
                {
                    //assigning temporary variables to avoid multiple calculating the same value
                    x = source.Key[i].x * scaleFactor;
                    y = source.Key[i].y * scaleFactor;
                    prevX = source.Key[i - 1].x * scaleFactor;
                    prevY = source.Key[i - 1].y * scaleFactor;

                    //draw the connecting line
                    resultingImageGraphics.DrawLine(customPen, x, y, prevX, prevY);

                    //draw the current node and previous node
                    resultingImageGraphics.FillEllipse(
                        Brushes.Black, x - halfRadius, y - halfRadius, radiusOfNode, radiusOfNode);
                    resultingImageGraphics.FillEllipse(
                        Brushes.Black, prevX - halfRadius, prevY - halfRadius, radiusOfNode, radiusOfNode);
                }*/
            }
            resultingImageGraphics.Dispose();
            return resultingImage;
        }

        public static Bitmap DrawHelpingLine(List<Vector2> points, List<int> way)
        {
            return null;
        }
    }
}
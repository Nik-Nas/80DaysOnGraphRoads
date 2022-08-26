using System.Drawing;
using ITCampFinalProject.Code.WorldMath;

namespace ITCampFinalProject.Code.Drawing
{
    public class Line
    {
        public Sprite start;
        public Sprite end;
        public Pen pen;

        public Line(Sprite start, Sprite end, int width, Color color)
        {
            this.start = start;
            this.end = end;
            pen = new Pen(color, width);
        }
    }
}
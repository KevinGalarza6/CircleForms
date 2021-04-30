using System.Drawing;

namespace WinForms
{
    public struct Line : IDrawable
    {
        public PointF Start { get; set; }
        public PointF End { get; set; }
        public Color Color { get; set; }

        public void Draw(Graphics g)
        {
            //todo: implement cache for this 
            using(var pen = new Pen(Color)) 
            { 
                g.DrawLine(pen, Start, End); 
            }
        }
    }
}

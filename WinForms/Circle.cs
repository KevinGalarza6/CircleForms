using System.Drawing;

namespace WinForms
{
    public struct Circle : IDrawable
    {
        public double Radius { get; set; }

        public PointF Position { get; set; }

        public void Draw(Graphics g)
        {
            var rectangle = CaculateRectangle();
            g.DrawEllipse(Pens.Black, rectangle);
        }

        private RectangleF CaculateRectangle()
        {
            var radius = (float)Radius;
            var rectangle = new RectangleF() 
            {
                X = Position.X - radius,
                Y = Position.Y - radius,
                Width = 2 * radius,
                Height = 2 * radius
            };

            return rectangle;
        }
    }
}

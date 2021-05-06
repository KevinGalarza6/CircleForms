using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private Timer _timer;
        private double _radAngle;
        private double _angle;
        private double _cos;
        private double _sin;
        private double _tan;
        private float _zoom;
        private Circle _circle;
        private Line _line;
        private Line _xAxis;
        private Line _yAxis;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _timer = new Timer();
            _timer.Interval = 16;
            _timer.Tick += _timer_Tick;
            _timer.Start();
            _zoom = 1;
            _mousePosition = new PointF { X = 0, Y = 0 };
            _circle = new Circle() { Position = new PointF { X = 0, Y = 0 }, Radius = 200 };
            _line = new Line() { Start = new PointF { X = 0, Y = 0 }, End = new PointF { X = 200, Y = 200 }, Color = Color.Black };
            _xAxis = new Line()
            {
                Start = new PointF { X = -this.ClientRectangle.Width, Y = 0 },
                End = new PointF { X = this.ClientRectangle.Width, Y = 0 },
                Color = Color.Blue
            };
            _yAxis = new Line()
            {
                Start = new PointF { X = 0, Y = -this.ClientRectangle.Height },
                End = new PointF { X = 0, Y = this.ClientRectangle.Height },
                Color = Color.Red
            };
            this.MouseWheel += new MouseEventHandler(this.panel1_MouseWheel);
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _radAngle += 0.01;
            _angle = _radAngle * (180 / Math.PI);

            if (_radAngle >= Math.PI * 2)
                _radAngle -= Math.PI * 2;

            _cos = Math.Cos(_radAngle);
            _sin = Math.Sin(_radAngle);
            _tan = Math.Tan(_radAngle);
            _line.End = new PointF() { X = (float)(_cos * _circle.Radius), Y = (float)(_sin * _circle.Radius) };

            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2);
            e.Graphics.ScaleTransform(_zoom, _zoom);
            _xAxis.Draw(e.Graphics);
            _yAxis.Draw(e.Graphics);
            DrawValues(e.Graphics);
            _circle.Draw(e.Graphics);
            _line.Draw(e.Graphics);
        }

        private void DrawValues(Graphics g)
        {
            string format = $"angle = {_angle:0.###} | cos = {_cos:0.###} | sen = {_sin:0.###} | tan = {_tan:0.###}";

            g.DrawString(format, this.Font, Brushes.Red, _line.End);
        }

        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && _zoom + 1 != 0)
            {
                _zoom *= (float)1.25;
            }
            else if (e.Delta < 0 && _zoom - 1 != 0)
            {
                _zoom /= (float)1.25;
            }
        }
    }
}

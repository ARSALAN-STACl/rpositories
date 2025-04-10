using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace анимация
{
    public partial class Form1 : Form
    {
        private PointF[] duckPositions;
        private PointF[] duckDirections;
        private const float speed = 5f;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Size = new Size(800, 600);

            duckPositions = new PointF[3]
            {
                new PointF(100, 100),
                new PointF(300, 200),
                new PointF(500, 300)
            };

            duckDirections = new PointF[3]
            {
                new PointF(1, 0),
                new PointF(-1, 1),
                new PointF(0, -1)
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            DrawDuck(g, duckPositions[0]); // Обычная утка
            DrawDuckWithJacket(g, duckPositions[1]); // Утка с курточкой
            DrawDuckWithCap(g, duckPositions[2]); // Утка с кепочкой
        }

        private void DrawDuck(Graphics g, PointF position)
        {
            GraphicsPath duckPath = new GraphicsPath();

            duckPath.AddEllipse(position.X - 20, position.Y - 10, 40, 30);

            duckPath.AddLine(position.X - 10, position.Y - 5, position.X - 30, position.Y - 15);
            duckPath.AddLine(position.X + 10, position.Y - 5, position.X + 30, position.Y - 15);

            PointF[] beakPoints =
            {
                new PointF(position.X, position.Y + 10),
                new PointF(position.X + 10, position.Y + 20),
                new PointF(position.X - 10, position.Y + 20)
            };
            duckPath.AddPolygon(beakPoints);

            g.FillPath(Brushes.Yellow, duckPath);
            g.DrawPath(Pens.Black, duckPath);
        }

        private void DrawDuckWithJacket(Graphics g, PointF position)
        {
            DrawDuck(g, position);

            GraphicsPath jacketPath = new GraphicsPath();

            jacketPath.AddRectangle(new RectangleF(position.X - 25, position.Y, 50, 30));

            jacketPath.AddEllipse(position.X - 35, position.Y + 5, 20, 20);
            jacketPath.AddEllipse(position.X + 15, position.Y + 5, 20, 20);

            g.FillPath(Brushes.Red, jacketPath);
            g.DrawPath(Pens.Black, jacketPath);
        }
        private void DrawDuckWithCap(Graphics g, PointF position)
        {
          
            DrawDuck(g, position);

            GraphicsPath capPath = new GraphicsPath();

            capPath.AddEllipse(position.X - 15, position.Y - 25, 30, 20);

            capPath.AddRectangle(new RectangleF(position.X - 20, position.Y - 15, 40, 10));

            g.FillPath(Brushes.Blue, capPath);
            g.DrawPath(Pens.Black, capPath);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                duckPositions[i].X += duckDirections[i].X * speed;
                duckPositions[i].Y += duckDirections[i].Y * speed;

                if (duckPositions[i].X < 0 || duckPositions[i].X > this.ClientSize.Width)
                    duckDirections[i].X = -duckDirections[i].X;

                if (duckPositions[i].Y < 0 || duckPositions[i].Y > this.ClientSize.Height)
                    duckDirections[i].Y = -duckDirections[i].Y;
            }

            this.Invalidate();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 30;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
    }
}
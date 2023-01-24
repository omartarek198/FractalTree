using System.Drawing;
using System.Reflection;

namespace Fractal_Tree
{
    public partial class Form1 : Form
    {

        float changeAngleInRad = 0.5f;
        int depth = 13;
        bool SimulateRandomChange = false;
        bool InstructionsVisible = true;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;

        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            DrawScene(e.Graphics);
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Maximized;

            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Left:
                    changeAngleInRad -= 0.05f;
                    DrawScene(CreateGraphics());

                    break;
                case Keys.Right:
                    changeAngleInRad += 0.05f;
                    DrawScene(CreateGraphics());

                    break;
                case Keys.Up:
                    depth += 1;
                    DrawScene(CreateGraphics());

                    break;
                case Keys.Down:
                    depth -= 1;
                    DrawScene(CreateGraphics());
                    break;
                case Keys.U:
                   InstructionsVisible = InstructionsVisible ? false : true;
                    DrawScene(CreateGraphics());
                        break;
            }

        }

        void branch(Graphics g, PointF point, float angle ,float length)

        {

            Pen p = new Pen(Brushes.White);


            if (length <= 0) return;
            //length = length *0.67f;
            //double dx = length * Math.Cos(angle);
            //double dy = length * Math.Sin(angle);
            //g.DrawLine(Pens.White, point.X, point.Y, point.X + (float)dx, point.Y - (float)dy);
            ////g.DrawLine(Pens.White, point.X, point.Y, point.X - (float)dx, point.Y - (float)dy);

            //float dA = ((float)Math.PI * 1   / 180);
            //branch(g, new PointF(point.X + (float)dx, point.Y - (float)dy), length, ((float)Math.PI * 180 / 180) - angle + dA);


            //branch(g, new PointF(point.X + (float)dx, point.Y - (float)dy), length, angle + dA);

            ////branch(g, new PointF(point.X - (float)dx, point.Y - (float)dy), length, angle +dA );

            //////branch(g, new pointF(point.x + (float)dx, point.y - (float)dy), length, angle);

            if (length-4 <= 0) p = new Pen(Brushes.Purple); 


            int x2 = (int)point.X + (int)(Math.Cos(angle) * length * 10.0);
            int y2 = (int)point.Y + (int)(Math.Sin(angle) * length * 10.0);
            g.DrawLine(p, new Point(x2, y2), point ) ;

            branch(g, new Point(x2, y2), angle - changeAngleInRad, (float)Math.Ceiling( (float) length -1));
            branch(g, new Point(x2, y2), angle + changeAngleInRad, (float)Math.Ceiling( (float) length -1));



        }

        void DrawInstructions(Graphics g)
        {

            SolidBrush brush = new SolidBrush(Color.White);
            g.DrawString("Angle change ≈ " + Math.Round(changeAngleInRad * (180) / (Math.PI)), new Font("Arial", 16), brush, new PointF(10, 10));
            g.DrawString("Depth = " + depth.ToString() , new Font("Arial", 16), brush, new PointF(10, 50));
            g.DrawString("Left/Right to control angle"  , new Font("Arial", 16), brush, new PointF(10, 100));
            g.DrawString("Up/Down to control Depth", new Font("Arial", 16), brush, new PointF(10, 150));
            g.DrawString("Press U to show/hide controls", new Font("Arial", 16), brush, new PointF(10, 200));


        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);


            if (InstructionsVisible)
                DrawInstructions(g);



            //g.DrawLine(Pens.White, this.Width / 2, this.Height, this.Width / 2, 650);

            PointF point = new PointF(this.Width /2, 650);



            branch(g, new PointF( ClientSize.Width / 2, ClientSize.Height - 10) , (float)-Math.PI/ 2, depth);



        }

     
    }
}
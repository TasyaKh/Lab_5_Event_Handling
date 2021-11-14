using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Lab_5_Event_Handling.Objects;
using System.Windows.Forms;

namespace Lab_5_Event_Handling
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        //Dots[] dots;
        Player player;
        Marker marker;
        MovingArea area;
        public Form1()
        {
            InitializeComponent();
            player = new Player(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);
            area = new MovingArea(pictureBox1.Width / 2, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height);

            player.onOverlap += (p, obj) =>
            {
                if(obj is Dots)
                {
                    Dots d = (Dots)obj;
                    //richTextBox1.Text = $" x: {obj.getX()}\ny:  {obj.getY()}\n" + richTextBox1.Text;//595 399
                }
                    
            };
            player.OnOverlap += (obj) =>
           {
               if (obj is Marker) {
                   objects.Remove(marker);
                   marker = null; // и обнуляю маркер
                }
               else if (obj is Dots)
               {
                   ((Dots)obj).Update();
                   Counter.Text = "Очки: " + player.getCountHit();
               }
               else if(obj is MovingArea)
               {
                   richTextBox1.Text = $"peresecl x: {((MovingArea)obj).getX()}\ny:  {((MovingArea)obj).getY()}\n";
               }
           };
            marker = new Marker(pictureBox1.Width / 2 + 50, pictureBox1.Height / 2 + 50, 0);
            objects.Add(area);

            objects.Add(new Dots(pictureBox1.Width, pictureBox1.Height, 0));
            objects.Add(new Dots(pictureBox1.Width, pictureBox1.Height, 0));
            objects.Add(player);
            objects.Add(marker);

            //objects. Add(new MyCircle(100, 100, 0));

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.Black);

            if(marker != null)
                player.Update(marker.getX(),marker.getY());

            area.Update(pictureBox1.Width);
            foreach (var obj in objects.ToList())
            {
                // проверяю было ли пересечение с игроком
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj); // то есть игрок пересекся с объектом
                }

                if (obj != area)
                {
                    bool itersect = false;
                    if (area.Overlaps(obj, g))
                        itersect = true;

                    area.intersectUn(obj, itersect); // то есть area intersect с объектом
                }
                g.Transform = obj.getMatrix();
                obj.Render(g);
            }
           // g.Transform = area.getMatrix();
           // area.Render(g);
        }
        //public static int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {  
            pictureBox1.Invalidate();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // и главное не забыть пололжить в objects
            }
            marker.Update(e.X,e.Y);
        }
    }
}

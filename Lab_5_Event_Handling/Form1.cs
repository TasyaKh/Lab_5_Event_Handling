using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        public Form1()
        {
            InitializeComponent();
            player = new Player(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);

            player.onOverlap += (p, obj) =>
            {
                // richTextBox1.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + richTextBox1.Text;
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
               }
           };
            marker = new Marker(pictureBox1.Width / 2 + 50, pictureBox1.Height / 2 + 50, 0);
            objects.Add(player);
            objects.Add(marker);

            objects.Add(new Dots(pictureBox1.Width, pictureBox1.Height, 0));
            objects.Add(new Dots(pictureBox1.Width, pictureBox1.Height, 0));
           
            //objects. Add(new MyCircle(100, 100, 0));
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.Black);

            if(marker != null)
                player.Update(marker.getX(),marker.getY());

            foreach (var obj in objects.ToList())
            {
                // проверяю было ли пересечение с игроком
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj); // то есть игрок пересекся с объектом
                }

                g.Transform = obj.getMatrix();
                obj.Render(g);
            }
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

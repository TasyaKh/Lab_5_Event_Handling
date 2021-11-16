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
        Player player; //игрок
        Marker marker; //маркер
        MovingArea area; //черная область
        CircleEnemy circleEnemy; //враг
        public Form1()
        {
            InitializeComponent();
            player = new Player(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);
            area = new MovingArea(pictureBox1.Width / 2, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height);
            circleEnemy = new CircleEnemy(pictureBox1.Width / 2, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height);
            //player.onOverlap += (p, obj) =>
            //{
            //    if(obj is Dots)
            //    {
            //        Dots d = (Dots)obj;
            //        //richTextBox1.Text = $" x: {obj.getX()}\ny:  {obj.getY()}\n" + richTextBox1.Text;//595 399
            //    }
                    
            //};
            player.OnOverlap += (obj) =>
           { //Если игрок пересекся с объектом
               if (obj is Marker) {        //Если маркер
                   objects.Remove(marker); //Удалить маркер
                   marker = null;          //и обнуляю маркер
                }
               else if (obj is CircleAlien)//Если враг
               {
                   ((CircleAlien)obj).Update(true); //Обновляем его размер или позицию
                   Counter.Text = "Очки: " + player.getCountHit(); //Обновляем очки
               }
               else if(obj is MovingArea) //Если двигающаяся область
               {
                   richTextBox1.Text = $"peresecl x: {((MovingArea)obj).getX()}\ny:  {((MovingArea)obj).getY()}\n";
               } //Получаем пересечение с объектами
           };

            circleEnemy.onOverlapPlayer += (obj) =>
            { //На пересечение игрока с врагом
                Counter.Text = "Очки: " + player.getCountHit();
            };

            marker = new Marker(pictureBox1.Width / 2 + 50, pictureBox1.Height / 2 + 50, 0);
            objects.Add(area);

            objects.Add(new CircleAlien(pictureBox1.Width, pictureBox1.Height, 0)); //добавить союзный шарик
            objects.Add(new CircleAlien(pictureBox1.Width, pictureBox1.Height, 0)); //добавить союзный шарик
            objects.Add(circleEnemy); //добавить вражеский шарик
            objects.Add(player);      //добавить игрока 
            objects.Add(marker);      ////добавить маркер нажатия

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.FromArgb(61, 48, 103)); //Установитть цвет фона

            if(marker != null)                    //Если маркер существует
                player.Update(marker.getX(),marker.getY()); //Обновить движение игрока до маркера

            area.Update(pictureBox1.Width);    //Обновить движение черной области
            circleEnemy.Update(false);         //Обновить расширение врага

            foreach (var obj in objects.ToList())
            {
                if (obj is CircleAlien){              //Если это точка
                    ((CircleAlien)obj).Update(false); //Обновить расшиерние точки
                }
               
                if(obj != player && player.Overlaps(obj, g)) //Если это не игрок а объект, который пересекся с игроком,то
                {
                    player.Overlap(obj); // Изменяем ифнормацию об объекте с которым пересеклись

                    if (obj is CircleEnemy)
                    { //Если мы пересеклись с врагом
                        circleEnemy.Overlap(player); // Изменяем ифнормацию об объекте с которым пересеклись
                        circleEnemy.Update(true);    //Изменить локацию и размер врага
                    }
                }

                if (obj != area)
                {//Если обект не черная область
                    bool itersect = false;
                    if (area.Overlaps(obj, g)) //Проверяем пересечение с объектом
                        itersect = true;

                    area.intersectUn(obj, itersect); // то есть area intersect с объектом
                }
                g.Transform = obj.getMatrix();      //получить трансформирматор объектов
                obj.Render(g);                      //Создать трансформированный объект
            }
        }
        //public static int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {  
            pictureBox1.Invalidate(); //Перерисовать
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            { //Содать новый маркер
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // и главное не забыть пололжить в objects
            }
            marker.Update(e.X,e.Y);
        }
    }
}

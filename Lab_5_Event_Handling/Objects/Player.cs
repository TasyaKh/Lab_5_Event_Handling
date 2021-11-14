using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System;

namespace Lab_5_Event_Handling.Objects
{
    class Player : BaseObject,IAction
    {
        public Action<BaseObject> OnOverlap;
        public float vX, vY;
        public Player(float x, float y, float angle) : base(x, y, angle)
        {
            wH = 30;
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Black), -wH / 2, -wH / 2, wH, wH);
            g.DrawEllipse(new Pen(Color.White, 2), -wH / 2, -wH / 2, wH, wH);
            g.DrawLine(new Pen(Color.White, 2), 0, 0, 25, 0);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-wH / 2, -wH / 2, wH, wH);
            return path;
        }
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            OnOverlap(obj);
        }
        public void Update(float markerX, float markerY)
        {
            float angle = 0;
            
                float dx = markerX - X;
                float dy = markerY - Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                // по сути мы теперь используем вектор dx, dy
                // как вектор ускорения, точнее даже вектор притяжения
                // который притягивает игрока к маркеру
                // 0.5 просто коэффициент который подобрал на глаз
                // и который дает естественное ощущение движения
                vX += dx * 0.5f;
                vY += dy * 0.5f;

                // расчитываем угол поворота игрока 
                angle = 90 - MathF.Atan2(vX, vY) * 180 / MathF.PI;
               // тормозящий момент,
               // нужен чтобы, когда игрок достигнет маркера произошло постепенное замедление
                vX += -vX * 0.1f;
                vY += -vY * 0.1f;

                setCoords(X + vX, Y + vY, angle);
            // пересчет позиция игрока с помощью вектора скорости
        }
        // richTextBox1.Text += "angle: " + angle + " player.vX" + player.vX + "  player.vY" + player.vY;
    }
}

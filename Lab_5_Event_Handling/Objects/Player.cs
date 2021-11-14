using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace Lab_5_Event_Handling.Objects
{
    class Player : BaseObject,IAction
    {
        public Action<BaseObject> OnOverlap;
        private float vX, vY;
        private int countHit;
        public Player(float x, float y, float angle) : base(x, y, angle)
        {
            wObj = hObject = 30;
            countHit = 0;

            colorObj = Color.DarkOrange;
            typeColor = Colors.DEFAULT;
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(colorObj), -wObj / 2, -hObject / 2, wObj, hObject);
            g.DrawEllipse(new Pen(Color.White, 2), -wObj / 2, -hObject / 2, wObj, hObject);
            g.DrawLine(new Pen(Color.White, 2), 0, 0, 25, 0);
        }
        public override void reverseColor()
        { 
           if (typeColor == Colors.DEFAULT)
            {
                colorObj = Color.DarkOrange; 
            }
           else
            {
                base.reverseColor();
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-wObj / 2, -hObject / 2, wObj, hObject);
            return path;
        }
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            if (obj is Dots)
            {
                countHit++;
            }
                OnOverlap(obj);
        }
        public int getCountHit()
        {
            return countHit;
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
                vX += dx * 0.8f;
                vY += dy * 0.8f;

                // расчитываем угол поворота игрока 
                angle = 90 - MathF.Atan2(vX, vY) * 180 / MathF.PI;
               // тормозящий момент,
               // нужен чтобы, когда игрок достигнет маркера произошло постепенное замедление
                vX += -vX * 0.1f;
                vY += -vY * 0.1f;

                setCoords(X + vX, Y + vY, angle);
            // пересчет позиция игрока с помощью вектора скорости
        }
       
    }
}

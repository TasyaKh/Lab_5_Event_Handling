using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace Lab_5_Event_Handling.Objects
{
    class Player : BaseObject,IAction
    {
        public Action<BaseObject> OnOverlap;
        private float vX, vY;
        protected int countHit; //Счетчик количества попаданий на точки
                                //public Action<BaseObject, BaseObject> onOverlap;
        public Player(float x, float y, float angle) : base(x, y, angle)
        {
            wObj = hObj = 30;  //Присваиваем начальную ширину и высоту объукта

            countHit = 0;      //Счетчик попаданий

            colorObj = Color.DarkOrange; //Нач. цвет
            typeColor = Colors.DEFAULT;  //Нач. тип цвета
        }
        public override void Render(Graphics g)
        {   //Рисуем игрока
            g.FillEllipse(new SolidBrush(colorObj), -wObj / 2, -hObj / 2, wObj, hObj);
            g.DrawEllipse(new Pen(Color.White, 2), -wObj / 2, -hObj / 2, wObj, hObj);
            g.DrawLine(new Pen(Color.White, 2), 0, 0, 25, 0);
        }
        public override void reverseColor()
        { //Именяем цвет игрока
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
            var path = base.GetGraphicsPath(); //Получаем графику для игрока
            path.AddEllipse(-wObj / 2, -hObj / 2, wObj, hObj); //Добавляем фигуру для пересечения
            return path;
        }
        public override void Overlap(BaseObject obj)
        { //Если произршло пересечение
            //base.Overlap(obj);

            if (obj is CircleAlien)
            { //Если это точка союзник, то увеличмваю ссчетчик попаданий
                countHit++;
            }
            else if(obj is CircleEnemy && countHit > 0)
            {//Если это точка враг, то уменьшаю счетчик попаданий
                countHit--;
            }
                OnOverlap(obj);
        }
        public int getCountHit()
        { //Выводим кол-во попаданий
            return countHit;
        }
        public void Update(float markerX, float markerY)
        { //Обновляем позицию и поворот игрока
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

using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace Lab_5_Event_Handling.Objects
{
    class Dots : BaseObject
    {
        protected Random rand = new Random();
        protected float maxX; //Ширина сцены
        protected float maxY; //Высота сцены

        protected Dots(float maxX, float maxY, float maxAngle) : base(maxX, maxY, maxAngle)
        {
            this.maxX = maxX;   //макс Ширина сцены
            this.maxY = maxY;   //макс Высота сцены
        }
        public override void Render(Graphics g)
        { //Создаем круг
            g.FillEllipse(new SolidBrush(colorObj), -wObj / 2, -hObj / 2, wObj, hObj);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-wObj / 2, -hObj / 2, wObj, hObj);
            return path;
        }
        public virtual void Update(bool reverseLocation)
        { //Будет выполеяться, когда нужно изменить положение или размер шарика
          
        }
    }
}

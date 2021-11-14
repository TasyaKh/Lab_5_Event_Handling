using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace Lab_5_Event_Handling.Objects
{
    class Dots : BaseObject, IAction
    {
        Random randPosit = new Random();
        private float maxX;
        private float maxY;
        public Dots(float maxX, float maxY, float maxAngle) : base(maxX, maxY, maxAngle)
        {
            wObj=hObject = 20;
            this.maxX = maxX;
            this.maxY = maxY;
            colorObj = Color.Yellow;
            Update();
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(colorObj), -wObj / 2, -hObject / 2, wObj, hObject);
        }
        public override void reverseColor()
        {
            if (typeColor == Colors.DEFAULT)
            {
                colorObj = Color.Yellow;
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
        public void Update(float playerX = -1, float playerY = -1)
        {

          if(playerX == -1 && playerY == -1)
            {
                base.Overlap(this);
                X = randPosit.Next((int)(maxX - wObj)) + wObj/2;
                Y = randPosit.Next((int)(maxY - hObject)) + hObject / 2;
            }  
        }
    }
}

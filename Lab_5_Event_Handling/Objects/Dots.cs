using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System;

namespace Lab_5_Event_Handling.Objects
{
    class Dots : BaseObject, IAction
    {
        Random randPosit = new Random();
        float maxX;
        float maxY;
        public Dots(float maxX, float maxY, float maxAngle) : base(maxX, maxY, maxAngle)
        {
            wH = 20;
            this.maxX = maxX;
            this.maxY = maxY;
            Update();
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Yellow), -wH / 2, -wH / 2, wH, wH);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-wH / 2, -wH / 2, wH, wH);
            return path;
        }
        public void Update(float playerX = -1, float playerY = -1)
        {

          if(playerX == -1 && playerY == -1)
            {
                X = randPosit.Next((int)(maxX - wH) + wH / 2);
                Y = randPosit.Next((int)(maxY - wH) + wH / 2);
            }  
        }
    }
}

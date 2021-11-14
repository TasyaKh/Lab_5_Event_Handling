using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5_Event_Handling.Objects
{
    class MovingArea:BaseObject,IAction
    {
        private readonly float speed;
        public MovingArea(float x, float y, int widthScreen,int heightScreen) : base(x, y, 0)
        {
            //this.widthScreen = widthScreen;
            wObj = widthScreen / 2;
            hObject = heightScreen;
            colorObj = Color.White;
            speed = 70;
        }
        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(colorObj), -wObj / 2, -hObject/ 2, wObj, hObject);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(-wObj / 2, -hObject / 2, wObj, hObject));
            return path;
        }

        public void intersectUn(BaseObject obj, bool intersect) {
            BaseObject ob = null;
           
            switch (obj)
            {
                case Player:
                    ob = (Player)obj; 
                    break;
                case Marker:
                    ob = (Marker)obj;
                    break;
                case Dots:
                    ob = (Dots)obj;
                    break;
            }
 
            if (ob != null)
            {
                if (intersect)
                    ob.typeColor = Colors.COLORLESS;
                else
                    ob.typeColor = Colors.DEFAULT;
                ob.reverseColor();
            }
                
        }
        public void Update(float widthScreen = 0,float y = 0)
        {
            if(X - wObj/2 >= widthScreen)
            {
                X = -wObj / 2;
            }
            X += widthScreen / speed;
        }
    }
}

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lab_5_Event_Handling.Objects
{
    class Marker:BaseObject,IAction
    {
       
        public Marker(float x, float y, float angle) : base(x, y, angle)
        {
            wObj = hObject = 6;
            colorObj = Color.Red;
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(colorObj), -wObj/ 2, -hObject / 2, wObj, hObject);
            g.DrawEllipse(new Pen(colorObj, 2), -wObj, -hObject, wObj*2, hObject * 2);
            g.DrawEllipse(new Pen(colorObj, 2), -10, -10, 20, 20);
        }
        public override void reverseColor()
        {
            if (typeColor == Colors.DEFAULT)
            {
                colorObj = Color.Red;
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

        public void Update(float x, float y)
        {
            setCoords(x, y, 0);
        }
    }
}

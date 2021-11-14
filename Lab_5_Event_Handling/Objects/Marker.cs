using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Lab_5_Event_Handling.Objects
{
    class Marker:BaseObject,IAction
    {
       
        public Marker(float x, float y, float angle) : base(x, y, angle)
        {
            wH = 6;
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.White), -wH / 2, -wH / 2, wH, wH);
            g.DrawEllipse(new Pen(Color.Red, 2), -wH, -wH, wH*2, wH*2);
            g.DrawEllipse(new Pen(Color.Red, 2), -10, -10, 20, 20);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-wH / 2, -wH / 2, wH, wH);
            return path;
        }

        public void Update(float x, float y)
        {
            setCoords(x, y, 0);
        }
    }
}

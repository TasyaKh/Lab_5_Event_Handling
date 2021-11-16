using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lab_5_Event_Handling.Objects
{
    class MovingArea:BaseObject,IAction
    {
        private readonly float speed; //Скорость большой прямоугольной области
        public MovingArea(float x, float y, int widthScreen,int heightScreen) : base(x, y, 0)
        {
            //this.widthScreen = widthScreen;
            wObj = widthScreen / 2; //ширина объекта
            hObj = heightScreen;    //высота объекта
            colorObj = Color.White; //цвет объекта
            speed = 70;             //скорость объекта
        }
        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(colorObj), -wObj / 2, -hObj/ 2, wObj, hObj);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle((int)-wObj / 2, (int)-hObj / 2, (int)wObj, (int)hObj));
            return path;
        }

        public void intersectUn(BaseObject obj, bool intersect) {
            //Смотрим пересеклись ли мы с этой одластью или нет и взависимоси от этого меняем цвет фигур
            BaseObject ob = null;
           
            switch (obj)
            {
                case Player: //Если игрок
                    ob = (Player)obj; 
                    break;
                case Marker://Если маркер
                    ob = (Marker)obj;
                    break; 
                case CircleAlien: //Если союзник
                    ob = (CircleAlien)obj;
                    break;
            }
 
            if (ob != null && !(ob is CircleEnemy)) //Цвет врага менять не будем
            {        //Если пересеклись то
                if (intersect)
                    ob.typeColor = Colors.COLORLESS; //Обесцвечиваем
                else //Если нет
                    ob.typeColor = Colors.DEFAULT;   //Ставим по умолчанию(собственный цвет объекта)
                ob.reverseColor();
            }
                
        }
        public void Update(float widthScreen = 0,float y = 0)
        { //Обновить позицию
            if(X - wObj/2 >= widthScreen)
            { //Если вышли за рамки зоны видимости то
                X = -wObj / 2;
            } 
            X += widthScreen / speed; //Увеличиваем позицию по оси х
        }
    }
}

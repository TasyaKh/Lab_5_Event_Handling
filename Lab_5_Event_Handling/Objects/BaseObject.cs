using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lab_5_Event_Handling.Objects
{
    public interface IAction
    {
        void Update(float x, float y);
    }
    class BaseObject
    {
        public enum Colors
        {
            DEFAULT, COLORLESS,
        }

        protected float X;     //Позиция х
        protected float Y;     //Позиция у
        protected float Angle; //Угол наклона

        protected float wObj;  //Ширина объекта
        protected float hObj;  //Высота объекта

        protected Color colorObj; //Цветобъекта
        public Colors typeColor;  //Тип цвета объекта

        public BaseObject(float x,float y,float angle)
        {
            X = x;
            Y = y;
            Angle = angle;    
        }

        public virtual void Render(Graphics g)
        {
        }
        public Matrix getMatrix()
        { //Полусить позицию объекта и его угол
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);

            return matrix;
        }
        protected void setCoords(float x, float y, float angle)
        { //Задаем координаты фигуры
            X = x;
            Y = y;
            Angle = angle;
        }
        public float getX()
        {
            return X;
        }
        public float getY()
        {
            return Y;
        }
        public float getAngle()
        {
            return Angle;
        }
        public virtual GraphicsPath GetGraphicsPath()
        {
            // пока возвращаем пустую форму
            return new GraphicsPath();
        }
        public virtual void reverseColor()
        { //Изменить цвет на черный
             colorObj = Color.Black;
        }
        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            // берем информацию о форме
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            // применяем к объектам матрицы трансформации
            path1.Transform(this.getMatrix());
            path2.Transform(obj.getMatrix());

            // используем класс Region, который позволяет определить 
            // пересечение объектов в данном графическом контексте
            var region = new Region(path1);
            region.Intersect(path2); // пересекаем формы
            return !region.IsEmpty(g); // если полученная форма не пуста то значит было пересечение
        }
        public virtual void Overlap(BaseObject obj)
        {
            //if(this.onOverlap != null)
            //{
            //    this.onOverlap(this, obj);
            //}
        }
    }
    
}

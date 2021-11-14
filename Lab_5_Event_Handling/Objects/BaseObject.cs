using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace Lab_5_Event_Handling.Objects
{
    public interface IAction
    {
        void Update(float x, float y);
    }
    class BaseObject
    {
        protected float X;
        protected float Y;
        protected float Angle;
        protected int wH;

        public Action<BaseObject, BaseObject> onOverlap;
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
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);

            return matrix;
        }
        protected void setCoords(float x, float y, float angle)
        {
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
            if(this.onOverlap != null)
            {
                this.onOverlap(this, obj);
            }
        }
    }
    
}

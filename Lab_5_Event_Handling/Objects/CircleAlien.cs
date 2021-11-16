using System.Drawing;

namespace Lab_5_Event_Handling.Objects
{
    class CircleAlien:Dots
    {
        public CircleAlien(float maxX, float maxY, float maxAngle) : base(maxX, maxY, maxAngle)
        {
           // wObj = hObj = 40;
            colorObj = Color.Yellow;
            Update(true);
        }
        public override void reverseColor()
        { //Если нужно изменить цвет, для черной области
            if (typeColor == Colors.DEFAULT)
            { //Если цвет нужен стандартный
                colorObj = Color.Yellow;
            }
            else
            { //Если нужен бесцветный
                base.reverseColor(); //обесцвечиваем
            }
        }
        public override void Update(bool reverseLocation)
        {
            wObj -= 40 * 0.01f; //изменить ширину по оси х на 1%
            hObj -= 40 * 0.01f; //изменить высоту по оси х на 1%

            if (wObj <= 0 || reverseLocation)
            { //Если нам нужно изменить локацию
                wObj = hObj = (float)rand.Next(40) + 25;      //Задаем начальные значения ширины и высоты(рандомные)

                X = rand.Next((int)(maxX - wObj)) + wObj / 2; //Начальное положение по оси х
                Y = rand.Next((int)(maxY - hObj)) + hObj / 2; //Начальное положение по оси у
            }
         }
     
     }
}

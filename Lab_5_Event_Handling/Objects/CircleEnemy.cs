using System;
using System.Drawing;

namespace Lab_5_Event_Handling.Objects
{
    class CircleEnemy:Dots//Класс для большого вражескорго круга, который постепенно увеличивается и при 
    {                            //прикосновении к нему теряем 1 очко
        public Action<BaseObject> onOverlapPlayer; //Если игрок пересекся с врагом

        public CircleEnemy(float x, float y, float maxX, float maxY) : base(x, y, 0)
        {

           // wObj = hObj = 40;
            colorObj = Color.FromArgb(100, 0, 150, 255);
            Update(true); //Обновляем положение объекта
        }
        public override void Overlap(BaseObject obj)
        {
            if(obj is Player)
            { //Если игрок, то
                onOverlapPlayer(obj); //действие
            }

        }
        public override void Update(bool reverseLocation)
        {
            wObj += maxY/3 * 0.01f; //изменить ширину по оси х на 1%
            hObj += maxY/3 * 0.01f; //изменить высоту по оси х на 1%

            if (reverseLocation)
            { //Если нам нужно изменить локацию
                wObj = hObj = (float)rand.Next(40) + 25;      //Задаем начальные значения ширины и высоты(рандомные)

                X = rand.Next((int)(maxX - wObj)) + wObj / 2; //Начальное положение по оси х
                Y = rand.Next((int)(maxY - hObj)) + hObj / 2; //Начальное положение по оси у
            }
        }
    }
}

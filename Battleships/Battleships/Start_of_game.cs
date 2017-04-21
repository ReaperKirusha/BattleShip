using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Start_of_game
    {
        int[] type = new int[4];

        public Start_of_game() {
            type[3] = 1;
            type[2] = 2;
            type[1] = 3;
            type[0] = 4;
        }
       
        public void InitializeField(Player Main) {
            Console.Clear();
            Console.WriteLine("Игрок 1 подтвердите размещение кораблей");
            Console.Read();
            Console.Clear();

            
            SetPoints_Type(Main);




        }
        private void SetPoints_Type(Player Main) {
            Console.WriteLine("Выберите тип корабля");
            int b;
            while (!int.TryParse(Console.ReadLine(), out b)) { Console.WriteLine("Введите число"); }
            Point First_Point, Second_Point;

            Console.WriteLine("Введите координаты первой точки");
            while (!int.TryParse(Console.ReadLine(), out First_Point.a)) { Console.WriteLine("Введите число"); } 
            while (!int.TryParse(Console.ReadLine(), out First_Point.b)) { Console.WriteLine("Введите число"); }

            Console.WriteLine("введите координаты второй точки");
            while (!int.TryParse(Console.ReadLine(), out Second_Point.a)) { Console.WriteLine("Введите число"); }
            while (!int.TryParse(Console.ReadLine(), out Second_Point.b)) { Console.WriteLine("Введите число"); }

            while (!Main.GetShipsOfPlayer().SetShip(First_Point, Second_Point, b)) {
                Console.Clear();
                Console.WriteLine("Возникла ошибка, ваш тип = " + b +" , повторите ввод");

                Console.WriteLine("Введите координаты первой точки");
                First_Point.a = int.Parse(Console.ReadLine());
                First_Point.b = int.Parse(Console.ReadLine());

                Console.WriteLine("введите координаты второй точки");
                Second_Point.a = int.Parse(Console.ReadLine());
                Second_Point.b = int.Parse(Console.ReadLine());

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Battleships
{
    public class Ship {
        private bool IsShipDestroyed; // уничтожен ли корабль
        public bool horizantal; //горизонтальный или нет
        private int type; //количство точек корабля
        private Point[] ShipsPoints; // точки корабля в виде поинтов
        private bool[] DamagedPoints;// true - стоит, false - точка уничтожена

        //ищет корабль с такой точкой, если не найден, возвращает null
        public Ship FindShip(Point Attack)
        {
            for (int i = 0; i < type; i++)
            {
                if (ShipsPoints[i].Equals(Attack))
                {
                    return this;
                }
            }
            return null;
        }

        //Записыват точки в массив точек для корабля, если первая точка не выше или не левее, возвращает ошибку
        //так же если не соотвествует тип
        public bool SetPoints(Point First, Point Second, int CertainType) {

            //проверка положения
            if (First.a > Second.a || First.b < Second.b) { return false; }

            //проверка типа
            type = Math.Max(Math.Abs(First.a - Second.a), Math.Abs(First.b - Second.b));
            if (CertainType != type) { return false; }

            //горизонтален или нет
            if ((First.a - Second.a) == 0)
            {
                horizantal = false;
            }
            else { horizantal = true; }

            for (int i = 0; i < type; i++) {
                ShipsPoints[i].a = First.a + i * Math.Sign(First.a - Second.a);
                ShipsPoints[i].b = First.b + i * Math.Sign(First.b - Second.b);
                DamagedPoints[i] = true;
            }
            return true;
        }
        public Point[] GetPoints() { return ShipsPoints; }

        //Ищет есть ли такая точка у корабля, если да то записывает на нее урон, возвращает попал ли
        public bool SetDamage(Point Attack) {
            for (int i = 0; i < type; i++)
            {
                if (ShipsPoints[i].Equals(Attack)) {
                    DamagedPoints[i] = false;
                    break;
                }
            }
            return CheckDestroy();
        }

        //Вовзращает информацию о уничтожении корабля, а так же меняет переменную уничтожения на уничтожен
        public bool CheckDestroy()
        {
            int Temp = 0;
            for (int i = 0; i < type; i++)
            {
                if (!DamagedPoints[i])
                    Temp++;
            }

            IsShipDestroyed = (Temp == type);
            return IsShipDestroyed;
        }

        //возвращает массив уничтоженых точек, нужен для отображения
        public bool[] GetShipDamage() { return DamagedPoints; }
    }

    public class Ships {
        Ship[] ShipsArray;
        bool[] DestroyedShips;// true - стоит, false - уничтожен
        int NumberOfShips;
        int NumberOfLeftShips;
        
        //ищет корабль по точку, если не нашел возвразает null, в ином случае корабль
        public Ship FindShip(Point Attack)
        {
            for (int i = 0; i < NumberOfShips; i++)
            {
                if (ShipsArray[i].FindShip(Attack) != null)
                {
                    return ShipsArray[i].FindShip(Attack);
                }
            }
            return null;
        }

        //возвращает попал ли
        public bool SetDamage(Point Attack)
        {
            bool Temp = false;
            for (int i = 0; i < NumberOfShips; i++)
            {
                if (ShipsArray[i].SetDamage(Attack)) {
                    Temp = true;                
                }
            }
            return Temp;
        }
    }

    public struct Point {
        public int a, b;
    }

   
    class Player
    {
        //для случая когда имя не задается
        private static int NumberOfPLayers = 1;
        private static int ReturnNumberOfPlayers() { NumberOfPLayers++; return (NumberOfPLayers - 1); }

        //массив с точками для кораблей игрока
        private char[,] Ships;//пусто(O), подбит(D), стоит(S), пустое попадание(M)
        
        // массив с точками для выстрелов игрока
        private char[,] Shots;//попал(D), непопал(M), ореол(A), Пусто(O)
        
        //структура  показывающая какие корабли остались, будет выводится для другого игрока, чтоб он знал что по чем
        private Ships Ships_of_player;

        // просто чилсо сколько корабелй осталось, тоже для другого игрока
        private int Ships_left;

        //имя игрока
        private string Name;

        Player() {
            Ships = new char[10,10];
            Shots = new char[10,10];
            Name = "Player " + ReturnNumberOfPlayers(); //
        }

        //возвращает значене в опред точке
        public char GetPointInShips(Point a) {
            return Ships[a.a, a.b];
        }

        //назначает значение в точке
        public void SetPointInShips(char b, Point a)
        {
           Ships[a.a, a.b] = b;
        }

        public Ships GetShipsOfPlayer() {
            return Ships_of_player;
        }

        //Установить имя игрока, которое будет выводится впоследствии
        public bool SetPlayerName(string NewName) {
            if (NewName.Length > 20) {
                return false;
            }else{
                Name = NewName;
                return true;
            }
        }

        //доступ к имени можно было бы сделать через свойство, но обе эти функции используются в совершенно
        //разных частях программы, так что я решил что это не нужно
        public string GetPlayerName(){ return Name; }

        //проверка выстрела, проверяется массив Shots игрока, который делает выстрел, если поле == O,
        //то выстрел можно произвести или точка не за пределами массива
        public bool ShootCheking(Point a) {
            if (a.a > 10 || a.b > 10) {
                return false;
            }

            if (Shots[a.a, a.b] == 'O')
            {
                return true;
            }else { return false; }
        }

        //Метод для выстрела, получает на вход точку и другого игрока, чтоб спросить о попадании у него
        // возвращает попал\не попал
        public int Shot(Point Attack, Player Enemy) {
            if (Enemy.GetPointInShips(Attack) == 'S')
            {
                Shots[Attack.a, Attack.b] = 'D';
                if (Enemy.GetShipsOfPlayer().SetDamage(Attack))
                {
                    Point[] Temp = Ships_of_player.FindShip(Attack).GetPoints();
                    bool horiantal = Ships_of_player.FindShip(Attack).horizantal;


                    return 2;
                }
                
                //поменять значение у Enemy
                return 1;
            }
            else
            {
                Shots[Attack.a, Attack.b] = 'M';
                return 0;
            }
        }

        //заменяет значение в поле для выстрелов и ставит ореолы
        public void ChangeFieldsAfterShot(Point a, Player Enemy) {
            

        }

        
    }
}

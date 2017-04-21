using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{


    class Program
    {

        static public int[] game(int[] a) {
            a[0] = 7;
            return a; }


        static void Main(string[] args)
        {
            Player FirstPlayer = new Player(), SecondPlayer = new Player();

            Start_of_game Start = new Start_of_game();
            Start.InitializeField(FirstPlayer);


    }
}
}

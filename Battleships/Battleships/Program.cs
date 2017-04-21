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
            int[] name = { 4, 5, 6};
            int[] gub = {1, 2, 3};
            
            gub = game(name);

            Console.WriteLine(name[0]);
            Console.WriteLine(gub[1]);
            Console.WriteLine(gub[2]);

        }
    }
}

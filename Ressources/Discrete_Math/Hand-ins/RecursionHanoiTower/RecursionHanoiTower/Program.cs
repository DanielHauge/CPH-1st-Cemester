using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RecursionHanoiTower
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Indtast Højde på tårn!");
            
            Hanoi tower = new Hanoi(int.Parse(Console.ReadLine()));
            tower.WriteTowerToConsole();

            tower.Move(tower.discs, 0, 2, 1);
            Console.Clear();
            tower.WriteTowerToConsole();
            Console.WriteLine(" ");
            tower.allsteps.ForEach(Console.WriteLine);
            Console.WriteLine(" ");
            Console.WriteLine("COOL! Den klarede den. Liste over alle trin. Slukker om 10 seconder.");
            Thread.Sleep(10000);
            Console.WriteLine("Tast for at slukke");

        }
    }
}

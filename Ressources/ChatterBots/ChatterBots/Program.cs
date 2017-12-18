using System;
using ChatterBotAPI;

namespace ChatterBots
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatterBotFactory factory = new ChatterBotFactory();

            ChatterBot bot1 = factory.Create(ChatterBotType.CLEVERBOT);
            ChatterBotSession bot1session = bot1.CreateSession();



            string s = "Hi";
            while (true)
            {


                Console.WriteLine("bot2> " + s);

                s = bot1session.Think(s);
            }

        }
    }
}

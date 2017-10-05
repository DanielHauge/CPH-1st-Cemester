using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RegularExpressionsMath
{
    class Program
    {
        static void Main(string[] args)
        {
            var AllStates = new List<string> { "s0", "s1", "s2", "s3", "s4", "s5", "s6", "s6" }; ///// 
            var Alpha = new List<char> { 'a', 'b', 'c', 'd', 'l', 'o', 'g', 'i', 'n' }; //////////Alphabet || Sigma
            var AllStateChanges = new List<StateManager>()
            {
                new StateManager("s0", 'l', "s1"),
                new StateManager("s1", 'o', "s2"),
                new StateManager("s2", 'g', "s3"),
                new StateManager("s3", 'i', "s4"),
                new StateManager("s4", 'n', "s5")
            };
            var FinalStates = new List<string> { "s5" };


            var DFA = new RegExpress(AllStates, Alpha, AllStateChanges, "s0", FinalStates);

            DFA.Accepts("login");

            Console.ReadLine();
        }
    }
}

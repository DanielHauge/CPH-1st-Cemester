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
        private static List<string> AllStates;
        private static List<char> Alpha;
        private static List<StateManager> AllStateChanges;
        private static List<string> FinalStates;

        static void Main(string[] args)
        {

            /// Hvis dette program skal vurderes, foreslår jeg at der kigged på metoden SetupAssigment1 først, for at forstå hvordan jeg har sat en Deterministic Finite State machine op! OPS: RegExpress klassen skulle nok have hede Finite State Machine og StateManager skulle nok have hedet StateChange
            /// og derefter kigger på assigment2 metoden til at se hvordan den genereres udfra en regular expressioN! NOTE: Generation af FSA er ikke 100% perfekt.


            ////// Task 1
            //RegExpress DFA = SetupAssigment1();
            ///// Task 2
            RegExpress DFA = SetupAssigment2("l(p|e|i)*o");

            string[] users = new string[4];
            
            if (DFA.Accepts(users[0] + 'l')) { users[0] = DoAction(users[0], 'l'); }
            if (DFA.Accepts(users[0] + 'p')) { users[0] = DoAction(users[0], 'p'); }
            if (DFA.Accepts(users[0] + 'p')) { users[0] = DoAction(users[0], 'p'); }
            if (DFA.Accepts(users[0] + 'e')) { users[0] = DoAction(users[0], 'p'); }
            if (DFA.Accepts(users[0] + 'o')) { users[0] = DoAction(users[0], 'o'); }
            if (DFA.Accepts(users[1] + 'p')) { users[1] = DoAction(users[1], 'p'); }
            Console.WriteLine("You haven't logged in yet.!\n\n");
            if (DFA.Accepts(users[1] + 'l')) { users[1] = DoAction(users[1], 'l'); }
            if (DFA.Accepts(users[1] + 'p')) { users[1] = DoAction(users[1], 'p'); }
            if (DFA.Accepts(users[1] + 'o')) { users[1] = DoAction(users[1], 'o'); }
            if (DFA.Accepts(users[2] + 'l')) { users[2] = DoAction(users[2], 'l'); }
            if (DFA.Accepts(users[2] + 'l')) { users[2] = DoAction(users[2], 'l'); }
            Console.WriteLine("When you are logged in, you cannot login again!\n\n"); 
            if (DFA.Accepts(users[2] + 'e')) { users[2] = DoAction(users[2], 'e'); }
            if (DFA.Accepts(users[2] + 'e')) { users[2] = DoAction(users[2], 'e'); }
            if (DFA.Accepts(users[2] + 'e')) { users[2] = DoAction(users[2], 'e'); }
            if (DFA.Accepts(users[2] + 'e')) { users[2] = DoAction(users[2], 'e'); }
            if (DFA.Accepts(users[2] + 'e')) { users[2] = DoAction(users[2], 'e'); }
            if (DFA.Accepts(users[2] + 'e')) { users[2] = DoAction(users[2], 'e'); }
            if (DFA.Accepts(users[2] + 'e')) { users[2] = DoAction(users[2], 'e'); }
            if (DFA.Accepts(users[2] + 'e')) { users[2] = DoAction(users[2], 'e'); }
            if (DFA.Accepts(users[2] + 'e')) { users[2] = DoAction(users[2], 'e'); }
            if (DFA.Accepts(users[2] + 'i')) { users[2] = DoAction(users[2], 'i'); }
            if (DFA.Accepts(users[2] + 'o')) { users[2] = DoAction(users[2], 'o'); }
            if (DFA.Accepts(users[3] + 'p')) { users[3] = DoAction(users[3], 'p'); }
            Console.WriteLine("FAIL! You haven't logged in yet!!\n\n");  
            if (DFA.Accepts(users[3] + 'l')) { users[3] = DoAction(users[3], 'l'); }
            if (DFA.Accepts(users[3] + 'p')) { users[3] = DoAction(users[3], 'e'); }
            if (DFA.Accepts(users[3] + 'o')) { users[3] = DoAction(users[3], 'o'); }
            if (DFA.Accepts(users[3] + 'o')) { users[3] = DoAction(users[3], 'o'); }
            Console.WriteLine("Ilegal action!, you are not logged in, so you cannot logout\n\n");

    
            ///// Task 2
            //RegExpress DFA2 = SetupAssigment2("A(B|C)*D");



            Console.ReadLine();
        }

        private static RegExpress SetupAssigment1()
        {
            AllStates = new List<string> { "s0", "s1", "s2" }; ///// Alle states der findes.
            Alpha = new List<char> { 'l', 'i', 'e', 'p', 'o' }; //////////Alphabet || Sigma
            // l = Login
            // i = List Items
            // e = Edit Items
            // p = post item
            // o = logout
            AllStateChanges = new List<StateManager>() //// Laver alle statechanges (Hvad sker der når man går fra en state til en anden)
            {
                new StateManager("s0", 'l', "s1"), //// Manually making the states
                new StateManager("s1", 'i', "s1"),
                new StateManager("s1", 'e', "s1"),
                new StateManager("s1", 'p', "s1"),
                new StateManager("s1", 'o', "s2"),
            };
            FinalStates = new List<string> { "s2" }; /// Opretter final state, de states som et udtryk kan slutte på.

            RegExpress DFA = new RegExpress(AllStates, Alpha, AllStateChanges, "s0", FinalStates); /// Opretter Finite State Automaton
            return DFA;
        }

        private static string DoAction(string user, char action)
        {
            return user = user + action;
        }

        private static RegExpress SetupAssigment2(string v)
        {
            AllStates = new List<string>();
            Alpha = new List<char>(); 
            AllStateChanges = new List<StateManager>();
            FinalStates = new List<string>();

            GenerateFSRules(v, AllStates, Alpha, AllStateChanges, FinalStates);
            Console.WriteLine("\n\n\n\n");
            Console.WriteLine("ALLSTATES:" );
            foreach (var state in AllStates)
            {
                Console.WriteLine(state);
            }
            Console.WriteLine("\nALLFINALSTATES");
            foreach (var final in FinalStates)
            {
                Console.WriteLine(final);
            }
            Console.WriteLine("\nALLSTATECHANGES");
            foreach (var change in AllStateChanges)
            {
                Console.WriteLine(change);
            }

            RegExpress DFA = new RegExpress(AllStates, Alpha, AllStateChanges, "s0", FinalStates); 
            return DFA;
        }

        private static void GenerateFSRules(string v, List<string> allStates, List<char> alpha, List<StateManager> allStateChanges, List<string> finalStates)
        {
            GenerateAlphabet(v, alpha);
            Console.WriteLine("Alphabet was generated!");
            foreach (var cha in Alpha)
            {
                Console.WriteLine(cha);
            }
            Console.WriteLine("\n\n");

            List<char> expression = v.ToList();
            int statecount = 0;
            bool inparan = false;
            bool asterixparan = false;
            for (int i = 0; i < expression.Count; i++)
            {
                if (i == expression.Count-1)
                {
                    Console.WriteLine("We made it to the final");

                    if (expression[i] != '*')
                    {
                        Console.WriteLine("Fianl was not asterix");
                        allStateChanges.Add(new StateManager("s" + statecount, expression[i], "s" + (statecount + 1)));
                        statecount++;
                        allStates.Add("s" + statecount);
                    }



                    finalStates.Add("s" + statecount);
                }
                else if (alpha.Contains(expression[i]) && expression[i + 1] != '*' && !inparan)
                {
                    Console.WriteLine("Was a regular add + next step!");
                    allStateChanges.Add(new StateManager("s"+statecount, expression[i], "s"+(statecount+1)));
                    allStates.Add("s"+statecount);
                    statecount++;
                }
                else if (alpha.Contains(expression[i]) && expression[i + 1] == '*' && !inparan)
                {
                    Console.WriteLine("Was a regular add, but was asterixed, so will not get to new step!");
                    allStateChanges.Add(new StateManager("s" + statecount, expression[i], "s" +statecount));
                }
                else if (expression[i] == '(')
                {
                    inparan = true;
                    Console.WriteLine("Oh! we found OR = (");
                    for (int j = i; j < expression.Count-1; j++)
                    {
                        if (expression[j] == ')')
                        {
                            if (expression[j + 1] == '*')
                            {
                                asterixparan = true;
                                Console.WriteLine("We are in asterix or expression!");
                            }
                            break;
                        }
                    }
                }
                else if (expression[i] == ')')
                {
                    Console.WriteLine("We are out of or expression!");
                    inparan = false;
                    asterixparan = false;
                    allStates.Add("s" + statecount);
                }
                else if (alpha.Contains(expression[i]) && inparan)
                {
                    Console.WriteLine("Found char inside OR, therefore it will not be going to next step! untill out of parameter");
                    if (!asterixparan)
                    {
                        allStateChanges.Add(new StateManager("s" + statecount, expression[i], "s" + (statecount + 1)));
                        
                    }
                    else
                    {
                        allStateChanges.Add(new StateManager("s" + statecount, expression[i], "s" + statecount));
                    }
                    

                }
                else if (expression[i] == '|' && !inparan)
                {
                    Console.WriteLine("ohh we found a final state!");
                    finalStates.Add("s"+statecount);
                }
                

                
                


            }
        }

        private static void GenerateAlphabet(string v, List<char> alpha)
        {
            List<char> expression = v.ToList();
            foreach (var bokstav in expression)
            {
                switch (bokstav)
                {
                    case 'l':
                        alpha.Add('l');
                        Console.WriteLine("Added l");
                        break;
                    case 'o':
                        alpha.Add('o');
                        Console.WriteLine("Added o");
                        break;
                    case 'e':
                        alpha.Add('e');
                        Console.WriteLine("Added e");
                        break;
                    case 'i':
                        alpha.Add('i');
                        Console.WriteLine("Added i");
                        break;
                    case 'k':
                        alpha.Add('k');
                        Console.WriteLine("Added k");
                        break;
                    case 'p':
                        alpha.Add('p');
                        Console.WriteLine("Added p");
                        break;
                    case 'A':
                        alpha.Add('A');
                        Console.WriteLine("Added A");
                        break;
                    case 'B':
                        alpha.Add('B');
                        Console.WriteLine("Added B");
                        break;
                    case 'C':
                        alpha.Add('C');
                        Console.WriteLine("Added C");
                        break;
                    case 'D':
                        alpha.Add('D');
                        Console.WriteLine("Added D");
                        break;
                    case 'E':
                        alpha.Add('E');
                        Console.WriteLine("Added E");
                        break;
                    case 'F':
                        alpha.Add('F');
                        Console.WriteLine("Added F");
                        break;
                    case 'G':
                        alpha.Add('G');
                        Console.WriteLine("Added G");
                        break;
                    case 'H':
                        alpha.Add('H');
                        Console.WriteLine("Added H");
                        break;
                    case 'I':
                        alpha.Add('I');
                        Console.WriteLine("Added I");
                        break;
                    case 'J':
                        alpha.Add('J');
                        Console.WriteLine("Added J");
                        break;
                    case 'K':
                        alpha.Add('K');
                        Console.WriteLine("Added K");
                        break;
                    case 'L':
                        alpha.Add('L');
                        Console.WriteLine("Added L");
                        break;
                    case 'M':
                        alpha.Add('M');
                        Console.WriteLine("Added M");
                        break;
                    case 'N':
                        alpha.Add('N');
                        Console.WriteLine("Added N");
                        break;
                    case 'O':
                        alpha.Add('O');
                        Console.WriteLine("Added O");
                        break;
                    case 'P':
                        alpha.Add('P');
                        Console.WriteLine("Added P");
                        break;
                    case 'Q':
                        alpha.Add('Q');
                        Console.WriteLine("Added Q");
                        break;
                    case 'R':
                        alpha.Add('R');
                        Console.WriteLine("Added R");
                        break;
                    case 'S':
                        alpha.Add('S');
                        Console.WriteLine("Added S");
                        break;
                    case 'T':
                        alpha.Add('T');
                        Console.WriteLine("Added T");
                        break;
                    case 'U':
                        alpha.Add('U');
                        Console.WriteLine("Added U");
                        break;
                    case 'V':
                        alpha.Add('V');
                        Console.WriteLine("Added V");
                        break;
                    case 'X':
                        alpha.Add('X');
                        Console.WriteLine("Added X");
                        break;
                    case 'Y':
                        alpha.Add('Y');
                        Console.WriteLine("Added Y");
                        break;
                    case 'Z':
                        alpha.Add('Z');
                        Console.WriteLine("Added Z");
                        break;
                    default:
                        Console.WriteLine("Not a valid char");
                        break;
                }
            }

        }

        
    }


}

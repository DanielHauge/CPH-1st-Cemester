using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegularExpressionsMath
{
    public class RegExpress
    {
        private string StartState;
        public List<string> FSM = new List<string>();
        public List<char> Alphabet = new List<char>();
        public List<StateManager> AllStateChanges = new List<StateManager>();
        public List<string> FinalStates = new List<string>();


        public RegExpress(IEnumerable<string> fsm, IEnumerable<char> alphabet, IEnumerable<StateManager> changes, string start, IEnumerable<string> final)
        {

            FSM = fsm.ToList();
            Alphabet = alphabet.ToList();
            AddChange(changes);
            AddStartState(start);
            AddFinalStates(final);

        }

        public void Accepts(string Case)
        {
            var WorkingState = StartState;
            var trin = new StringBuilder();
            foreach (var Char in Case.ToCharArray())
            {
                var change = AllStateChanges.Find(c => c.FromState == WorkingState && c.Symbol == Char); // Gets the StateChange for every char in the case

                if (change == null)
                {
                    Console.WriteLine("Der findes ingen Statechanges der matcher, Dette er ikke et lovligt change.");
                    Console.WriteLine(trin);
                    return; //// Stops method if change is not existing.
                }
                
                    WorkingState = change.NextState;
                    trin.Append(change + "\n");
                
            }
            if (FinalStates.Contains(WorkingState))
            {
                Console.WriteLine("Acceptere dette input med trin: "+trin);
                return; //// Stops method if everything worked out fine.
            }

            Console.WriteLine("Fejl ved state: "+WorkingState+" - Dette er ikke det sidste trin til at udføre om casen er accepteret, Dette funger ikke");
            Console.WriteLine(trin);
        }



        private void AddChange(IEnumerable<StateManager> changes)
        {
            foreach (var Change in changes) /// Only adds change if it is a valid (Godkendt) change
            {
                if (GodkendtChange(Change))
                {
                    AllStateChanges.Add(Change); //// Adds change to allstateChanges
                   
                }
                else
                {
                    Console.WriteLine("Change ikke godkendt: "+ Change + " States eller symboler findes ikke i Sigma eller FSM");
                }
                 
            }
        }

        private bool GodkendtChange(StateManager arg)
        {
            return FSM.Contains(arg.FromState) && FSM.Contains(arg.NextState) && Alphabet.Contains(arg.Symbol) && !ChangeErIkkeAlleredeDefineret(arg);  /// Checks if it is OKay to add this change, based on what the Finite state machine has. (Alphabet symbols, and statechanges)
        }

        private bool ChangeErIkkeAlleredeDefineret(StateManager arg)
        {
            return AllStateChanges.Any(change => change.FromState == arg.FromState && change.Symbol == arg.Symbol); ///// Checks if the state is allready defined. If the current start state with the same symbol is allready defined.
        }

        private void AddFinalStates(IEnumerable<string> final)
        {
            foreach (var FState in final.Where(FState => FSM.Contains(FState)))
            {
                FinalStates.Add(FState);
            }
        }

        private void AddStartState(string start)
        {
            if (FSM.Contains(start))
            {
                StartState = start;
            }
        }
    }
}

﻿using System;
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
            /// Assigner værdier.
            FSM = fsm.ToList();
            Alphabet = alphabet.ToList();
            AddChange(changes); //// Tilføjer alle changes (Fra state til state)
            AddStartState(start); //// Tilføjer start state
            AddFinalStates(final); //// Tilføjer alle final sates (De states som er lovlige at slutte på.

        }

        public bool Accepts(string Case)
        {
            bool legalaction = true;
            var WorkingState = StartState;
            var trin = new StringBuilder();
            foreach (var Char in Case.ToCharArray())
            {
                var change = AllStateChanges.Find(c => c.FromState == WorkingState && c.Symbol == Char); // Gets the StateChange for every char in the case (Dette vil give alle mulige statechanges som kan give de specifikke bokstaver som er i casen)

                if (change == null)
                {
                    Console.WriteLine("Der findes ingen Statechanges der matcher, Dette er ikke et lovligt change. Der findes ingen statechange til et eller flere bokstaver der er i udtrykket");
                    Console.WriteLine(trin); //// Disse trin blev dog fundet, dette betyder at der er nogle bokstaver som har nogle statechanges som kan give disse bokstaver.
                    return false; //// Stopper metoden og foreach loppet. Da der ikke behøver mere information til at konkludere udtrykket ikke kan lade sig gøre. Da der ikke kan findes en vej dertil.
                }
                
                
                    WorkingState = change.NextState;
                    trin.Append(change + "\n");
                
            }
            if (FinalStates.Contains(WorkingState)) //// Hvis den sidste state er en finalstate, betyder det at udtrykket kan lade sig gøre.
            {
                Console.WriteLine("Acceptere dette input med trin: "+trin);
                return true; //// stopper metoden da alt er OK.
            }
            else
            {
                Console.WriteLine("Lovligt: " + WorkingState + " - Dette er ikke det sidste trin til at udføre om casen er accepteret, kan ikke slutte her");
                Console.WriteLine(trin);
                return true;
            }

            
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
            return AllStateChanges.Any(change => change.FromState == arg.FromState && change.Symbol == arg.Symbol); ///// Checks if the state is alllready defined. If the current start state with the same symbol is allready defined.
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

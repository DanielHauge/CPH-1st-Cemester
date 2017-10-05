using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegularExpressionsMath
{
    public class StateManager   //// Shoulda been called StateChange perhaps, but i am to lazy to rename it now. :P
    {
        public string FromState { get; private set; }
        public char Symbol { get; private set; }
        public string NextState { get; private set; }

        public StateManager(string Fromstate, char symb, string next)
        {
            FromState = Fromstate;
            Symbol = symb;
            NextState = next;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}) -> {2}", FromState, Symbol, NextState);
        }
    }
}

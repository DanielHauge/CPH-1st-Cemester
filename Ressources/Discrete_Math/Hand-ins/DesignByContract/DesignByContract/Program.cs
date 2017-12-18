

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignByContract
{
    class Program
    {
        static void Main(string[] args)
        {

            Account Tine = new Account("Tine", 100);
            Account Mathias = new Account("Mathias", 500);
            
            Tine.Deposit(50);
            Tine.Deposit(100);
            Tine.Deposit(-1);
            Mathias.Withdraw(100);
            Mathias.Withdraw(300);
            Mathias.Withdraw(-50);
            Mathias.Withdraw(200);
            Console.WriteLine("Cannot get Contracts to throw exceptions, see Account.cs");
            Console.Read();
        }
    }
}

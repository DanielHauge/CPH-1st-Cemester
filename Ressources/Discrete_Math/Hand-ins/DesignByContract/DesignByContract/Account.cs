

using System;
using System.Diagnostics.Contracts;

namespace DesignByContract
{
    public class Account : AccountActions
    {

        public string AccountHolderName { get; set; }
        public double balance { get; set; }


        public Account(string name, double cash)
        {
            balance = cash;
            AccountHolderName = name;
            Console.WriteLine("Account created with Name: " + name + " - Starting balance: " + cash);
        }

        public void Deposit(double amount)
        {
            //Contract.Requires<NotPossitiveException>(0 < amount);
            balance = balance + amount;
            Console.WriteLine("Depositing: " + amount + " To " + AccountHolderName + " - Current standing after deposit: " + balance);
        }

        public void Withdraw(double amount)
        {
            balance = balance - amount;
            Console.WriteLine("Withdrawing: " + amount + " From " + AccountHolderName + " - Current standing after withdraw: "+balance);
        }

    }


    [ContractClass(typeof(AccountActionsContract))]
    public interface AccountActions
    {

        double balance { get; set; }

        void Deposit(double amount);

        void Withdraw(double amount);

    }


    [ContractClassFor(typeof(AccountActions))]
    internal abstract class AccountActionsContract : AccountActions
    {

        public double balance
        {
            get { return balance; }
            set
            {
                Contract.Requires<NotPossitiveException>( value > 0 );
                
            }
        }

        void AccountActions.Deposit(double amount)
        {
            Contract.Requires<NotPossitiveException>( 0 < amount);
            Contract.Ensures(balance>0);
        }

        void AccountActions.Withdraw(double amount)
        {
            Contract.Requires<NotPossitiveException>(0 < amount);
            Contract.Requires<NotEnoughDough>( ((AccountActions)this).balance >= amount );
            Contract.Ensures(balance>0);
        }

    }

    public class NotPossitiveException : Exception
    {

        public NotPossitiveException(){}

        public NotPossitiveException(string message) : base(message) { }

        public NotPossitiveException(string message, Exception inner) : base(message, inner) { }

    }


    public class NotEnoughDough : Exception
    {

        public NotEnoughDough() { }

        public NotEnoughDough(string message) : base(message) { }

        public NotEnoughDough(string message, Exception inner) : base(message, inner) { }

    }
}

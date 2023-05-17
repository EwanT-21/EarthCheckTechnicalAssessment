namespace EarthCheckAssessment.Persistence
{
    public class Account
    {
        private double balance;
        private int accountNumber;

        public Account(int accountNumber, double balance)
        {
            this.balance = balance;
            this.accountNumber = accountNumber;
        }

        public double AccountNumber
        {
            get { return accountNumber; }
        }

        public double Balance
        {
            get { return balance; }
        }

        public void Withdraw(double amount)
        {
            balance -= amount;
        }

        public void Deposit(double amount)
        {
            balance += amount;
        }
    }
}
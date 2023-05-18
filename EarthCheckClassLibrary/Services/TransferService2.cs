using EarthCheckAssessment.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EarthCheckAssessment.Services
{
    public static class TransferService2
    {
        /// <summary>
        /// This method calculates the withdrawal from account1 by amount if possible, and deposits the amount into account 2.
        /// If an overdrawal occurs, a fee is calculated and charged to the account withdrawing.
        /// </summary>
        /// <param name="account1">The withdrawal accounut</param>
        /// <param name="account2">The deposit account</param>
        /// <param name="amount">The amount to withdraw and deposit</param>
        /// <returns>A boolean representing a successful transaction, i.e. amount is non-negative.</returns>
        public static bool TransferBetweenAccounts(Account account1, Account account2, double amount)
        {
            if (amount < 0 || account1.Balance < 0 || account1.AccountNumber == account2.AccountNumber)
                return false;

            var overdrawFee = 0d;
            if (account1.Balance.CompareTo(amount) < 0)
            {
                overdrawFee = CalculateOverdrawFee(account1, amount);
            }

            account1.Withdraw(amount + overdrawFee);
            account2.Deposit(amount);
            return true;
        }

        /// <summary>
        /// If a withdrawal occurs, we charge a fee of 2% of the overdraw, this method will calculate and return this 
        /// accordingly.
        /// </summary>
        /// <param name="withdrawalAccount"></param>
        /// <param name="amount"></param>
        /// <returns>The withdrawal fee cost.</returns>
        private static double CalculateOverdrawFee(Account withdrawalAccount, double amount) => 
            (double)Math.Round((amount - withdrawalAccount.Balance) * 0.02, 2);

    }
}

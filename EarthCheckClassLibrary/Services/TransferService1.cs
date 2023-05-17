using EarthCheckAssessment.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthCheckAssessment.Services
{
    public static class TransferService1
    {
        /// <summary>
        /// This method calculates the withdrawal from account1 by amount if possible, and deposits the amount into account 2.
        /// </summary>
        /// <param name="account1">The withdrawal accounut</param>
        /// <param name="account2">The deposit account</param>
        /// <param name="amount">The amount to withdraw and deposit</param>
        /// <returns>A boolean representing a successful transaction, i.e. overdrawal will not occur and amount is non-negative.</returns>
        public static bool TransferBetweenAccounts(Account account1, Account account2, double amount)
        {
            if (amount < 0 || account1.Balance.CompareTo(amount) < 0 || account1.AccountNumber == account2.AccountNumber) 
                return false;

            account1.Withdraw(amount);
            account2.Deposit(amount);
            return true;
        }
    }
}

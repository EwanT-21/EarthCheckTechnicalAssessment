using EarthCheckAssessment.Persistence;
using EarthCheckAssessment.Services;
using NSubstitute;

namespace TestProject1
{
    public class TransferService2Test
    {
        [Fact]
        public void AnyAccount1AndAccount2WithEqualAccountNumberInvalidForAnyAmount()
        {
            var account1 = new Account(2345, 20);
            var account2 = new Account(2345, 220);
            Assert.False(TransferService1.TransferBetweenAccounts(account1, account2, 10));
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-3)]
        [InlineData(-1.1)]
        public void AnyValidAccount1AndAccount2InvalidForAnyNegativeAmount(double amount)
        {
            var account1 = new Account(234, 20);
            var account2 = new Account(2345, 30);
            Assert.False(TransferService1.TransferBetweenAccounts(account1, account2, amount));
        }

        [Theory]
        [InlineData(0, 2, 0.04)]
        [InlineData(3, 1, 0)]
        [InlineData(1, 3, 0.04)]
        [InlineData(2, 3.5, 0.03)]
        [InlineData(3.5, 2, 0)]
        public void PositiveAccount1BalanceValidForAnyPositiveAmountAndAnyAccount2AndBalanceCorrect(double balance, 
            double amount, double withdrawalFee)
        {
            var account1 = new Account(234, balance);
            var account2 = new Account(2345, 30);
            Assert.True(TransferService2.TransferBetweenAccounts(account1, account2, amount));
            // To ensure our withdrawal was correctly
            Assert.True((account1.Balance + (amount + withdrawalFee)).CompareTo(balance) == 0);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(50.134, 10.988)]
        public void PositiveAccount1BalanceValidAnyAmountAndAnyAccount2(double balance, double amount)
        {
            var account1 = new Account(234, balance);
            var account2 = new Account(2345, 30);
            Assert.True(TransferService2.TransferBetweenAccounts(account1, account2, amount));
            Assert.True(account1.Balance + amount == balance);
        }

        [Theory]
        [InlineData(-3, 2, 0)]
        [InlineData(1, 2, 0)]
        [InlineData(5, 3, 0)]
        [InlineData(99, 99.9, 0)]
        [InlineData(5, 101.25, 0.02)]
        [InlineData(99, 103, 0.06)]
        public void Account2AndAccount1BalanceChangedByAmountValidForValidAccount1AndAmount(double balance, double amount,
            double withdrawalFee)
        {
            var account1 = new Account(234, 100);
            var account2 = new Account(2345, balance);
            Assert.True(TransferService2.TransferBetweenAccounts(account1, account2, amount));
            Assert.True((account1.Balance + (amount + withdrawalFee)).CompareTo(100) == 0);
            Assert.True(account2.Balance - amount == balance);
        }
    }
}
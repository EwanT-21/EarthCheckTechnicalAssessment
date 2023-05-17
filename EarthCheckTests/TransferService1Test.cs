using EarthCheckAssessment.Persistence;
using EarthCheckAssessment.Services;

namespace TestProject1
{
    public class TransferService1Test
    {
        [Fact]
        public void AnyAccount1AndAccount2WithEqualAccountNumberInvalidForAnyAmount()
        {
            var account1 = new Account(1234, 10);
            var account2 = new Account(1234, 110);
            Assert.False(TransferService1.TransferBetweenAccounts(account1, account2, 0));
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-3)]
        [InlineData(-1.1)]
        public void AnyAccount1AndAccount2InvalidForAnyNegativeAmount(double amount)
        {
            var account1 = new Account(1234, 5);
            var account2 = new Account(123, 5);
            Assert.False(TransferService1.TransferBetweenAccounts(account1, account2, amount));
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 3)]
        [InlineData(2, 3.5)]
        public void PositiveAccount1BalanceInvalidForAmountGreaterAndAnyAccount2(double balance, double amount)
        {
            var account1 = new Account(123, balance);
            var account2 = new Account(1234, -3);
            Assert.False(TransferService1.TransferBetweenAccounts(account1, account2, amount));
            Assert.True(account1.Balance == balance);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(50.134, 10.988)]
        public void PositiveAccount1BalanceValidForAmountLessThanOrEqualAndAnyAccount2(double balance, double amount)
        {
            var account1 = new Account(123, balance);
            var account2 = new Account(1234, 3);
            Assert.True(TransferService1.TransferBetweenAccounts(account1, account2, amount));
            Assert.True(account1.Balance + amount == balance);
        }

        [Theory]
        [InlineData(-3, 2)]
        [InlineData(1, 2)]
        [InlineData(5, 3)]
        [InlineData(99, 99.9)]
        public void Account2BalanceChangedByAmountValidForValidAccount1AndAmount(double balance, double amount)
        {
            var account1 = new Account(123, 100);
            var account2 = new Account(1234, balance);
            Assert.True(TransferService1.TransferBetweenAccounts(account1, account2, amount));
            Assert.True(account1.Balance + amount == 100 && account2.Balance - amount == balance);
        }
    }
}
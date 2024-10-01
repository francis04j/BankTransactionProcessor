using TransactionProcesor;

namespace TransactionProcessor.UnitTests;

using System.Collections.Generic;


public class AccountServiceTests
{
    [Fact]
    public void Deposit_IncreasesAccountBalance()
    {
        // Arrange
        var account = new Account(1, 1000);
        var accountService = new AccountService<Account>(account);
        
      // Act
      accountService.Deposit(1, 500);

        // Assert
        Assert.Equal(1500, account.Balance);
    }

    [Fact]
    public void Withdraw_DecreasesAccountBalance()
    {
        // Arrange
        
        var account = new Account(1, 1000);
        var accountService = new AccountService<Account>(account);

        // Act
        accountService.Withdraw(1, 300);

        // Assert
       
        Assert.Equal(700, account.Balance);
    }

    [Fact]
    public void Transfer_UpdatesBothAccountBalances()
    {
        // Arrange
        
        var account1 = new Account(1, 1000);
        var account2 = new Account(2, 500);
        var accountService = new AccountService<Account>(account1, account2);

        // Act
        accountService.TransferTo(1, 2, 300);

        // Assert
       
        Assert.Equal(700, account1.Balance);
        Assert.Equal(800, account2.Balance);
    }

    [Fact]
    public void Withdraw_FailsWhenInsufficientFunds()
    {
        // Arrange
        var account = new Account(1, 100);
        var accountService = new AccountService<Account>(account);

        // Act
        try
        {
            accountService.Withdraw(1, 200);
        }
        catch (Exception e)
        {
            // Assert
            Assert.Contains("Insufficient funds", e.Message);
            Assert.Equal(100, account.Balance); // Balance should remain unchanged
        }
    }
}

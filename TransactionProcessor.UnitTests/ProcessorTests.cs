using TransactionProcesor;

namespace TransactionProcessor.UnitTests;

public class TransactionProcessorTests
{
    [Fact]
    public void SingleThread_Deposit_ShouldIncreaseBalance()
    {
        //given
        var account = new Account(1, 1000);
        var amount = 300;
        var expectedBalance = 1300;
        var transaction = new Transaction(TransactionType.Deposit, account, amount);
        var accountService = new AccountService<Account>(account);
        var processor = new Processor(accountService);
        
        processor.Process(new List<Transaction> { transaction });
        
        Assert.Equal(expectedBalance, account.Balance);
    }
    
    [Fact]
    public async Task MultiThread_MultipleDeposits_ShouldUpdateBalanceCorrectly()
    {
        // Arrange
        var account = new Account(1, 1000);
        var transactions = new List<Transaction>
        {
            new Transaction(TransactionType.Deposit, account, amount: 200),
            new Transaction(TransactionType.Deposit, account, amount: 300),
            new Transaction(TransactionType.Deposit, account, amount: 500)
        };
        var accountService = new AccountService<Account>(account);
        var processor = new Processor(accountService);

        // Act
        
        await processor.ProcessTransactionsConcurrently(transactions); // Use multiple threads

        // Assert
        Assert.Equal(2000, account.Balance);
    }
    
    [Fact]
    public void MultiThread_MulitpleWithdraws_ShouldUpdateBalanceCorrectly()
    {
        // Arrange
        var account = new Account(1, 500);
        var transactions = new List<Transaction>
        {
            new Transaction(TransactionType.Withdraw, account, amount: 300),
            new Transaction(TransactionType.Withdraw, account, amount: 100)
        };
        var accountService = new AccountService<Account>(account);
        var processor = new Processor(accountService);

        // Act
        processor.ProcessTransactionsConcurrently(transactions);

        // Assert
        Assert.Equal(account.Balance, 100); // Should not allow overdrawing below 0
    }
    
    [Fact]
    public void MultiThread_ConcurrentWithdraws_ShouldNotOverdrawBelowZero()
    {
        // Arrange
        var account = new Account(1, 500);
        var transactions = new List<Transaction>
        {
            new Transaction(TransactionType.Withdraw, account, amount: 300),
            new Transaction(TransactionType.Withdraw, account, amount: 400)
        };
        var accountService = new AccountService<Account>(account);
        var processor = new Processor(accountService);

        // Act
        processor.ProcessTransactionsConcurrently(transactions);

        // Assert
        Assert.Equal(account.Balance, 200); // Should not allow overdrawing below 0
    }
    
    [Fact]
    public void MultiThread_TransfersBetweenAccounts_ShouldUpdateBothBalances()
    {
        // Arrange
        var account1 = new Account(1, 1000);
        var account2 = new Account(2, 500);
        var transaction = new Transaction(account1, account2, amount: 300);
        var accountService = new AccountService<Account>(account1, account2);
        var processor = new Processor(accountService);

        // Act
        processor.ProcessTransactionsConcurrently(new List<Transaction> { transaction });

        // Assert
        Assert.Equal(700, account1.Balance);
        Assert.Equal(800, account2.Balance);
    }
    
    // Test case for deadlock detection and prevention
    [Fact]
    public async Task MultiThread_DeadlockAvoidance_ShouldPreventDeadlock()
    {
        // Arrange
        var account1 = new Account(1, 1000);
        var account2 = new Account(2, 1000);
        var transactions = new List<Transaction>
        {
            new Transaction(account1, account2, amount: 300),
            new Transaction(account2, account1, amount: 300)
        };
        var accountService = new AccountService<Account>(account1, account2);
        var processor = new Processor(accountService);
        
        //act
        var exception = Record.ExceptionAsync(() => processor.ProcessTransactionsConcurrently(transactions));
        
        //assert
        Assert.Null(exception.Exception);
        Assert.Equal(1000, account1.Balance);
        Assert.Equal(1000, account2.Balance);
    }
}